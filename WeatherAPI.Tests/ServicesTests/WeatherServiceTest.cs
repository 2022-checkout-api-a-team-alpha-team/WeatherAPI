﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using WeatherAPI.DTOs;
using WeatherAPI.Models;
using WeatherAPI.Services;

namespace WeatherAPI.Tests.ServicesTests
{
    public class WeatherServiceTest
    {
        private WeatherService _weatherService;

        [SetUp]
        public void Setup()
        {
            _weatherService = new WeatherService();
        }

        [Test]
        public void Get_Hourly_Temperature_By_Latitude_And_Longitude_Return_Type_Is_GetHourlyTemperatureResponseDTO()
        {
            var result = _weatherService.GetHourlyTemperatureByLatitudeAndLongitude(51.5, -0.1262).Result;
            result.Should().BeOfType(typeof(GetHourlyTemperatureResponseDTO));
        }

        [Test]
        public void Get_Hourly_Temperature_By_CityName_Should_Return_NO_OF_RECORDS_AS_7()
        {
            var result = _weatherService.GetHourlyTemperatureByCity("London").Result;
            result.Count.Should().Be(7);
        }

        [Test]
        public void Get_Hourly_Temperature_By_CityName_Should_Return_Type_List_Of_HourlyTempForeCastAndSuggestions()
        {
            var result = _weatherService.GetHourlyTemperatureByCity("London").Result;
            result.Should().BeOfType(typeof(List<HourlyTempForeCastAndSuggestions>));
        }

        [Test]
        public void Get_Hourly_Temperature_By_CityName_Should_Give_Suggestions_Based_On_Hourly_Temperature()
        {
            var result = _weatherService.GetHourlyTemperatureByCity("London").Result;
            foreach (var record in result)
            {
                if (record.AverargeTemperature > 30)
                {
                    record.Suggestion.Should().Be("Its too Hot. Please plan your days/trip with proper Accessories and Appropriate Activities.");
                }
                else if (record.AverargeTemperature < 16)
                {
                    record.Suggestion.Should().Be("Its too Cold. Please plan your days/trip with proper Accessories and Appropriate Activities.");
                }
                else
                    record.Suggestion.Should().Be("The days are Pleasant. Enjoy your days!!!");
            }
        }

        [Test]
        public void Get_Hourly_Feels_Like_Temperature_By_CityName_Response_Should_Return_Expected_No_Of_Records()
        {
            var result = _weatherService.GetHourlyFeelsLikeTemperatureByCity("London").Result;
            result.Count.Should().Be(24);
        }

        [Test]
        public void Get_Hourly_Feels_Like_Temperature_By_CityName_Should_Give_Dressing_Suggestion_Based_On_Feels_Like_Temperature()
        {
            var result = _weatherService.GetHourlyFeelsLikeTemperatureByCity("London").Result;
            foreach(var record in result)
            {
                if (record.Temperature < record.FeelsLikeTemperature)
                {
                    record.Suggestion.Should().Be("You'll feel hotter than outside - Better to wear light cotton clothes.");
                }
                else if (record.Temperature > record.FeelsLikeTemperature)
                {
                    record.Suggestion.Should().Be("You'll feel colder than outside - Better to wear a jumper/ a jacket to avoid any chills.");
                }
                else
                    record.Suggestion.Should().Be("You'll feel just the right temperature as in air when you go out. Wear as you like.");
            }
        }
    }
}
