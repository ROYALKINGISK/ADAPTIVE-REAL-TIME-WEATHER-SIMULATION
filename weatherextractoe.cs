using System;
using System.Collections.Generic;
using System.IO;

class WeatherDataGenerator
{
    // Constants for temperature and humidity ranges
    static (int, int) TEMP_LOW = (10, 20);
    static (int, int) TEMP_MEDIUM = (20, 30);
    static (int, int) TEMP_HIGH = (30, 40);

    static (int, int) HUMIDITY_LOW = (5, 30);
    static (int, int) HUMIDITY_MEDIUM = (30, 60);
    static (int, int) HUMIDITY_HIGH = (60, 100);

    // Intensity range
    static (int, int) INTENSITY_RANGE = (40, 60);

    // Weather Types
    static string[] WEATHER_TYPES = { "Snow", "Clear", "Rain", "Hails", "Overcast", "Storm" };

    // Function to generate ranges for each weather type
    static (int, int) GetTemperatureRange(string weather)
    {
        if (weather == "Storm") return TEMP_LOW;
        else if (weather == "Overcast") return TEMP_MEDIUM;
        else if (weather == "Clear") return TEMP_LOW;
        else if (weather == "Snow" || weather == "Hails") return TEMP_LOW;
        else if (weather == "Rain") return TEMP_MEDIUM;
        else return TEMP_LOW;
    }

    static (int, int) GetHumidityRange(string weather)
    {
        if (weather == "Storm") return HUMIDITY_LOW;
        else if (weather == "Overcast") return HUMIDITY_LOW;
        else if (weather == "Clear") return HUMIDITY_HIGH;
        else if (weather == "Snow" || weather == "Hails") return HUMIDITY_LOW;
        else if (weather == "Rain") return HUMIDITY_MEDIUM;
        else return HUMIDITY_LOW;
    }

    // Main function to generate weather data and save to CSV
    static void Main()
    {
        var random = new Random();
        List<string[]> weatherData = new List<string[]>();
        int totalRows = 10000;  // Set the desired number of rows

        // Add CSV header
        weatherData.Add(new string[] { "Weather", "Temperature (°C)", "Humidity (%)", "Intensity" });

        for (int i = 0; i < totalRows; i++)
        {
            string weather = WEATHER_TYPES[random.Next(WEATHER_TYPES.Length)];
            var tempRange = GetTemperatureRange(weather);
            var humidityRange = GetHumidityRange(weather);

            int temperature = random.Next(tempRange.Item1, tempRange.Item2 + 1);
            int humidity = random.Next(humidityRange.Item1, humidityRange.Item2 + 1);

            // Adjust temperature and humidity based on weather conditions
            if (weather == "Snow" || weather == "Rain")
            {
                temperature -= 1;  // Reduce temperature by 1
                humidity -= 5;     // Reduce humidity by 5

                // Ensure that the values don't go below their respective minimum ranges
                temperature = Math.Max(temperature, tempRange.Item1);
                humidity = Math.Max(humidity, humidityRange.Item1);
            }
            else if (weather == "Clear")
            {
                temperature += 1;  // Increase temperature by 1
                humidity += random.Next(2, 4);  // Increase humidity by 2 or 3

                // Ensure that humidity doesn't exceed its maximum range
                humidity = Math.Min(humidity, humidityRange.Item2);
            }

            int intensity = random.Next(INTENSITY_RANGE.Item1, INTENSITY_RANGE.Item2 + 1);

            // Add the row to the data list
            weatherData.Add(new string[] { weather, temperature.ToString(), humidity.ToString(), intensity.ToString() });
        }

        // Write data to CSV file
        string filePath = "weather_data_10000_rows.csv";
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var row in weatherData)
            {
                writer.WriteLine(string.Join(",", row));
            }
        }

        Console.WriteLine($"Weather data has been successfully written to {filePath}.");
    }
}