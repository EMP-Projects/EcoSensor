using System.Globalization;
using EcoSensorApi.AirQuality;
using EcoSensorApi.AirQuality.Properties;
using EcoSensorApi.AirQuality.Vector;
using Microsoft.AspNetCore.Http.Features;
using NetTopologySuite.Geometries;
using TeamSviluppo.Gis;
using TeamSviluppo.Gis.NetCoreFw.OsmPg.Vector;
using TeamSviluppo.OpenMeteo.AirQuality;
namespace EcoSensorApi.MeasurementPoints;

/// <summary>
/// Servizio per calcolare i punti di misura per la qualità dell'aria
/// </summary>
public class MeasurementPointsService : IMeasurementPointsService 
{
    private readonly OsmVectorService _osmVectorService;
    private readonly AirQualityVectorService _airQualityVectorService;
    private readonly AirQualityPropertiesService _airQualityPropertiesService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<MeasurementPointsService> _logger;
    private readonly IAirQualityService _airQualityService;

    public MeasurementPointsService(
        OsmVectorService osmVectorService, 
        AirQualityVectorService airQualityVectorService, 
        IConfiguration configuration, 
        ILogger<MeasurementPointsService> logger, 
        IAirQualityService airQualityService, 
        AirQualityPropertiesService airQualityPropertiesService)
    {
        _osmVectorService = osmVectorService;
        _airQualityVectorService = airQualityVectorService;
        _configuration = configuration;
        _logger = logger;
        _airQualityService = airQualityService;
        _airQualityPropertiesService = airQualityPropertiesService;
    }

    /// <summary>
    /// Legge la geometria per applicare filtro sulle sorgenti di features di dati
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private Task<Geometry> ReadGeomQuery()
    {
        if (_configuration["Gis:SrCode"] is null)
        {
            const string msg = "Non riesco a leggere il sistema di riferimento delle coordinate dalle configurazioni Gis:SrCode";
            _logger.LogError(msg);
            throw new Exception(msg);
        }
        
        // leggo il file GeoJson delle regioni italiane, oppure de singolo comune
        // https://github.com/openpolis/geojson-italy/blob/master/geojson/limits_R_2_municipalities.geojson
        // TODO: Ideale sarebbe leggere il geojson direttamente da un GeoServer
        var featuresCollection = GisUtility.GetFeatureCollectionByGeoJson(_configuration["Gis:Istat:Layer"]!);

        // filtro il GeoJson per il comune e regione
        var fColl = featuresCollection.FirstOrDefault(x => (long)x.Attributes.GetOptionalValue(_configuration["Gis:Istat:RegioneCampo"]!) == int.Parse(_configuration["Gis:Istat:RegioneNumero"]!) 
            && (long)x.Attributes.GetOptionalValue(_configuration["Gis:Istat:ComuneCampo"]!) == int.Parse(_configuration["Gis:Istat:ComuneNumero"]!));
        
        if (fColl is null)
        {
            const string msg = "Non riesco a leggere il GeoJson delle geometrie";
            _logger.LogError(msg);
            throw new Exception(msg);
        }

        // creo la geometria per eseguire il filtro sulle feature di OSM
        var geom = GisUtility.CreateGeometryFactory(int.Parse(_configuration["Gis:SrCode"]!)).CreateGeometry(fColl.Geometry);
        
        // con geoserver si possono leggere direttamente i dati geojson con lo stesso sistema di riferimento di Osm EPSG:3857
        // creo un bbox dalla geometria nelle coordinate EPSG:3857
        var bbox = geom.Envelope;
        var pointMinToWebMercator = CoordinateConverter.ConvertWgs84ToWebMercator(bbox.EnvelopeInternal.MinX, bbox.EnvelopeInternal.MinY);
        var pointMaxToWebMercator = CoordinateConverter.ConvertWgs84ToWebMercator(bbox.EnvelopeInternal.MaxX, bbox.EnvelopeInternal.MaxY);
        var bboxConverted = GisUtility.CreateEnvelopeFromBBox(pointMinToWebMercator.Y, pointMinToWebMercator.X, pointMaxToWebMercator.Y, pointMaxToWebMercator.X);
        return Task.FromResult(GisUtility.CreateGeometryFromBBox(int.Parse(_configuration["Gis:SrCode"]!), bboxConverted));
    } 

    /// <summary>
    /// Crea le geometrie dei punti di misurazione 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<int> MeasurementPoints()
    {
        // leggo le nuove features
        var featuresOsm = await _osmVectorService.FeatureCollection();
        
        if (featuresOsm is null || featuresOsm.Count == 0)
        {
            const string msg = "Non riesco a leggere le geometrie da OpenStreet Map";
            _logger.LogError(msg);
            return 0;
        }
        
        var matrixDistancePoints = _configuration["Gis:MatrixDistancePoints"] is not null 
            ? double.Parse(_configuration["Gis:MatrixDistancePoints"]!) 
            : 2500;

        // leggo la collection di features di tutti i punti di misurazione per controllare eventuali duplicati
        var listMeasurementPoints = await _airQualityVectorService.FeatureCollection();
        var measurementPoints = new List<Point>();
        var measurementPointsMatrix = new List<Point>();
        var measurementPointsToInsert = new List<Point>();
        var index = listMeasurementPoints?.Count + 1 ;
        
        // leggo tutti i punti creati dalle coordinate delle geometrie
        foreach (var featureOsm in featuresOsm)
        {
            // leggo tutte le coordinate
            var coordinates = featureOsm.Geometry.Coordinates;
            // creo una collection di punti geografici dalle coordinate
            measurementPoints.AddRange(coordinates.Select(coordinate => GisUtility.CreatePoint(int.Parse(_configuration["Gis:SrCode"]!), coordinate)).ToList());
        }
        
        // selezionare solo i punti distanti matrixDistancePoints
        foreach (var point in from point in measurementPoints let isExistPointWithinDistance = measurementPointsMatrix?.Any(x => x.Distance(point) <= matrixDistancePoints) where isExistPointWithinDistance is null or false select point)
            measurementPointsMatrix?.Add(point);
        
        if (measurementPointsMatrix is not null) 
            if (listMeasurementPoints is null || listMeasurementPoints.Count == 0)
                measurementPointsToInsert = measurementPointsMatrix;
            else
            {
                // controllo se esiste un nuovo punto con una distanza
                // maggiore o uguale a matrixDistancePoints
                foreach (var point in measurementPointsMatrix)
                {
                    var nearestPoint = listMeasurementPoints.MinBy(p => p.Geometry.Distance(point));
                    if (nearestPoint?.Geometry.Distance(point) >= matrixDistancePoints)
                        measurementPointsToInsert.Add(point);
                }
            }
        
        // per ogni coordinate creo un punto geometrico 
        foreach (var newMeasurementPoint in measurementPointsToInsert.Select(point => new AirQualityVectorDto
                 {
                     SourceData = ESourceData.Osm,
                     Geom = point,
                     TimeStamp = DateTime.UtcNow,
                     Guid = Guid.NewGuid(),
                     Key = $"OSM:{index++}",
                     Lat = point.Y,
                     Lng = point.X
                 }))
        {
            await _airQualityVectorService.Insert(newMeasurementPoint);
        }

        return await _airQualityVectorService.SaveContext();
    }
    
    /// <summary>
    /// Crea le feature geografiche dalla sorgente dati da visualizzare sulla mappa
    /// e per il calcolo della matrice di punti di misurazione
    /// </summary>
    /// <returns></returns>
    public async Task<int> SeedFeatures()
    {
        var geomFilter = await ReadGeomQuery();
        // salvare le nuove features di OpenStreetMap
        return await _osmVectorService.SeedGeometries(geomFilter, int.Parse(_configuration["Gis:SrCode"]!));
    }

    private async Task<List<AirQualityPropertiesDto>> CreateListAQValues(
        EPollution pollution,
        double lat,
        double lng,
        double elevation,
        long gisId,
        string? unit,
        List<string?>? times,
        List<double?>? values,
        List<long?>? indexAq)
    {
        var result = new List<AirQualityPropertiesDto>();
        if (times is null) return result;
        
        foreach (var time in times.Select((value, i) => new { i, value}))
        {
            if (time.value is null) continue;
            if (values?[time.i] is null) continue;
            if (unit is null) continue;
            
            var airQualityProps = await _airQualityPropertiesService.List(new AirQualityPropertiesQuery
            {
                Begin = DateTime.Parse(time.value).ToUniversalTime(),
                Pollution = pollution,
                GisId = gisId,
                Source = EAirQualitySource.OpenMeteo,
                Longitude = lng,
                Latitude = lat
            });
            if (airQualityProps.Count != 0) continue;
            
            result.Add(new AirQualityPropertiesDto
            {
                Date = DateTime.Parse(time.value).ToUniversalTime(),
                PollutionText = Pollution.GetPollutionDescription(pollution),
                Pollution = pollution,
                Unit = unit,
                Value = values[time.i]!.Value,
                EuropeanAqi = indexAq?[time.i],
                Lat = lat,
                Lng = lng,
                GisId = gisId,
                SourceText = Pollution.GetPollutionSource(EAirQualitySource.OpenMeteo),
                Source = EAirQualitySource.OpenMeteo,
                Elevation = elevation
            });
        }

        return result;
    }
    
    public async Task<int> AirQuality()
    {
        // leggere i records delle coordinate geografiche dei punti di misurazione
        var pointsFeatures = await _airQualityVectorService.FeatureCollection();

        if (pointsFeatures is null)
        {
            _logger.LogWarning("Non sono riuscito a leggere le features dei punti di misurazione");
            return 0;
        }

        // estrarre le coordinate ed effettuare la chiamata API a OpenMeteo eliminando quelle null
        var coordinates = pointsFeatures
            .Select(features => features.Attributes.GetOptionalValue(_airQualityVectorService.NameProperties) as AirQualityLatLng)
            .Where(x => x is not null)
            .ToArray();

        if (coordinates is null || coordinates.Length == 0)
        {
            const string msg = "Non sono riuscito a leggere le coordinate dei punti di misura";
            _logger.LogError(msg);
            return 0;
        }

        const int step = 10;
        var listAllNewAirQualityDto = new List<AirQualityPropertiesDto>();

        for (var c = 0; c < coordinates.Length; c += step)
        {
            var nextItem = c + step - 1;
            var chunkCoordinates = coordinates[c..(nextItem < coordinates.Length ? nextItem : coordinates.Length - 1)]
                .Select(coords =>
                {
                    var p = CoordinateConverter.ConvertWebMercatorToWgs84(coords!.Lng, coords.Lat);
                    return new AirQualityLatLng(p.Y, p.X);
                }).ToArray();
            var openMeteoOptions = new AirQualityOptions(chunkCoordinates);
            var resultAq = await _airQualityService.AirQuality(openMeteoOptions);
            if (resultAq is null)
            {
                const string msg = "Non sono riuscito a leggere dalla API di OpenMeteo";
                _logger.LogError(msg);
                return 0;
            }

            foreach (var r in resultAq)
            {

                if (r.Longitude is null && r.Latitude is null) continue;
                var pointWebMercator = CoordinateConverter.ConvertWgs84ToWebMercator(r.Longitude!.Value, r.Latitude!.Value);
                
                // prendo la feature più vicina alle coordinate della risposta dalle API di OpenMeteo
                var nearestPoint = pointsFeatures?.MinBy(x => x.Geometry.Distance(pointWebMercator));
                
                // leggo Id della feature
                var propId = nearestPoint?.Attributes.GetOptionalValue("Id").ToString();
                if (propId is null)
                {
                    const string msg = "Non riesco a leggere l'Id del punto di misura";
                    _logger.LogError(msg);
                    return 0;
                }
                
                listAllNewAirQualityDto.AddRange(await CreateListAQValues(
                        EPollution.Pm10,
                        pointWebMercator.Y,
                        pointWebMercator.X,
                        r.Elevation!.Value,
                        long.Parse(propId),
                        r.HourlyUnits?.Pm10,
                        r.Hourly?.Time,
                        r.Hourly?.Pm10,
                        r.Hourly?.EuropeanAqiPm10
                    ));
                
                listAllNewAirQualityDto.AddRange(await CreateListAQValues(
                    EPollution.Pm25,
                    pointWebMercator.Y,
                    pointWebMercator.X,
                    r.Elevation!.Value,
                    long.Parse(propId),
                    r.HourlyUnits?.Pm25,
                    r.Hourly?.Time,
                    r.Hourly?.Pm25,
                    r.Hourly?.EuropeanAqiPm25
                ));
                
                listAllNewAirQualityDto.AddRange(await CreateListAQValues(
                    EPollution.CarbonMonoxide,
                    pointWebMercator.Y,
                    pointWebMercator.X,
                    r.Elevation!.Value,
                    long.Parse(propId),
                    r.HourlyUnits?.CarbonMonoxide,
                    r.Hourly?.Time,
                    r.Hourly?.CarbonMonoxide,
                    r.Hourly?.EuropeanAqi
                ));
                
                listAllNewAirQualityDto.AddRange(await CreateListAQValues(
                    EPollution.Ozone,
                    pointWebMercator.Y,
                    pointWebMercator.X,
                    r.Elevation!.Value,
                    long.Parse(propId),
                    r.HourlyUnits?.Ozone,
                    r.Hourly?.Time,
                    r.Hourly?.Ozone,
                    r.Hourly?.EuropeanAqiOzone
                ));
                
                listAllNewAirQualityDto.AddRange(await CreateListAQValues(
                    EPollution.SulphurDioxide,
                    pointWebMercator.Y,
                    pointWebMercator.X,
                    r.Elevation!.Value,
                    long.Parse(propId),
                    r.HourlyUnits?.SulphurDioxide,
                    r.Hourly?.Time,
                    r.Hourly?.SulphurDioxide,
                    r.Hourly?.EuropeanAqiSulphurDioxide
                ));
                
                listAllNewAirQualityDto.AddRange(await CreateListAQValues(
                    EPollution.NitrogenDioxide,
                    pointWebMercator.Y,
                    pointWebMercator.X,
                    r.Elevation!.Value,
                    long.Parse(propId),
                    r.HourlyUnits?.NitrogenDioxide,
                    r.Hourly?.Time,
                    r.Hourly?.NitrogenDioxide,
                    r.Hourly?.EuropeanAqiNitrogenDioxide
                ));
            }
        }
        
        // aggiungere e salvare i dto nel database
        foreach (var dto in listAllNewAirQualityDto)
            await _airQualityPropertiesService.Insert(dto);
        
        return await _airQualityPropertiesService.SaveContext();
    }
}