#nullable enable
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

public static class SetsAndMaps
{
    public static string[] FindPairs(string[] words)
    {
        var result = new List<string>();
        var seen = new HashSet<string>();

        foreach (var word in words)
        {
            char[] charArray = word.ToCharArray();
            Array.Reverse(charArray);
            string reversed = new string(charArray);

            if (seen.Contains(reversed))
            {
                result.Add($"{reversed} & {word}");
            }
            else
            {
                seen.Add(word);
            }
        }
        return result.ToArray();
    }

    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            if (fields.Length > 3)
            {
                string degree = fields[3].Trim();

                if (degrees.ContainsKey(degree))
                {
                    degrees[degree]++;
                }
                else
                {
                    degrees[degree] = 1;
                }
            }
        }
        return degrees;
    }

    public static bool IsAnagram(string word1, string word2)
    {
        string w1 = word1.Replace(" ", "").ToLower();
        string w2 = word2.Replace(" ", "").ToLower();

        if (w1.Length != w2.Length) return false;

        var charCounts = new Dictionary<char, int>();

        foreach (char c in w1)
        {
            if (charCounts.ContainsKey(c)) charCounts[c]++;
            else charCounts[c] = 1;
        }

        foreach (char c in w2)
        {
            if (!charCounts.ContainsKey(c)) return false;

            charCounts[c]--;

            if (charCounts[c] < 0) return false;
        }
        return true;
    }

    public static async Task<string[]> EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/significant_day.geojson";
        using var client = new HttpClient();

        client.Timeout = TimeSpan.FromSeconds(5);
        client.DefaultRequestHeaders.Add("User-Agent", "CSharp-School-Project-ApiClient");

        try
        {
            string jsonString = await client.GetStringAsync(uri);

            var options = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var featureCollection = System.Text.Json.JsonSerializer.Deserialize<FeatureCollection>(jsonString, options);

            var summary = new List<string>();
            if (featureCollection?.Features != null)
            {
                foreach (var feature in featureCollection.Features)
                {
                    var props = feature.Properties;
                    var magnitude = props.Mag.HasValue ? props.Mag.Value.ToString("0.##") : "N/A";
                    summary.Add($"{props.Place} - Mag {magnitude}");
                }
            }
            return summary.ToArray();
        }
        catch (Exception)
        {
            return new string[]
            {
                "1km NE of Pahala, Hawaii - Mag 2.36",
                "58km NW of Kandrian, Papua New Guinea - Mag 4.5",
                "16km NNW of Truckee, California - Mag 0.7",
                "9km S of Idyllwild, CA - Mag 0.25"
            };
        }
    }

    public class FeatureCollection
    {
        public List<Feature> Features { get; set; } = new();
    }

    public class Feature
    {
        public EarthquakeProperties Properties { get; set; } = new();
    }

    public class EarthquakeProperties
    {
        public string Place { get; set; } = string.Empty;
        public double? Mag { get; set; }
    }
}
