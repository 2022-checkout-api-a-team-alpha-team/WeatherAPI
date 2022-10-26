﻿namespace WeatherAPI.Helper
{
    public static class WeatherSuggestionsHelper
    {
        public const string WEAR_JACKET = "Better to wear a jumper/ a jacket to avoid any chills.";
        public const string WEAR_LIGHT_CLOTHES = "Better to wear light cotton clothes.";
        public const string WEAR_AS_YOU_LIKE = "Wear as you like.";
    }

    public static class FeelsLikeTemperatureSuggestions
    {
        public const string FEELS_LIKE_TEMP_COLD = "You'll feel colder than outside - " + WeatherSuggestionsHelper.WEAR_JACKET;
        public const string FEELS_LIKE_TEMP_HOT = "You'll feel hotter than outside - " + WeatherSuggestionsHelper.WEAR_LIGHT_CLOTHES;
        public const string FEELS_LIKE_TEMP_JUST_RIGHT = "You'll feel just the right temperature as in air when you go out. " + WeatherSuggestionsHelper.WEAR_AS_YOU_LIKE;
    }

    public static class HourlyTemperatureSuggestions
    {
        public const string FEELS_HOT = "It is a Hot day. Please plan your days/trip with proper Accessories and Appropriate Activities.";
        public const string FEELS_COLD = "Its is a Cold day. Please plan your days/trip with proper Accessories and Appropriate Activities.";
        public const string FEELS_PLEASANT = "It is a Pleasant day. Enjoy your day!!!";
    }
}
