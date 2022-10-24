﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using WeatherAPI.DTOs;
using WeatherAPI.Helper;

namespace WeatherAPI.Services
{
    public class AirQualityParticulateMatterService : IAirQualityParticulateMatterService
    {
        private readonly HttpClient _httpClient;
        private readonly IGeoService _geoService;
        private readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
        };
        
        public AirQualityParticulateMatterService()
        {
            _httpClient = new HttpClient();
            _geoService = new GeoService();
        }
        
        public async Task<GetAirQualityParticulateMatterResponseDTO> GetAirQualityParticulateMatterByCityName(string cityName)
        {
            var GeoCoordOfCity = _geoService.GetGeoCoordinatesByCityName(cityName);
            double lat = GeoCoordOfCity.Result.Results.ToList()[0].Latitude;
            double lon = GeoCoordOfCity.Result.Results.ToList()[0].Longitude;

            var result = await _httpClient.GetFromJsonAsync<GetAirQualityParticulateMatterResponseDTO>(ConstantsHelper.AQ_BASE + $"?latitude={lat.ToString().Trim()}&longitude={lon.ToString().Trim()}&hourly=pm10,pm2_5", options);
            return result;
        }
    }
}
