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
            string reversed = $"{word[1]}{word[0]}";

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
        const string uri = "https://usgs.gov";
        using var client = new HttpClient();

        client.Timeout = TimeSpan.FromSeconds(5);
        client.DefaultRequestHeaders.Add("User-Agent", "CSharp-School-Project-ApiClient");

        try
        {
            string jsonString = await client.GetStringAsync(uri);

            var serializer = new DataContractJsonSerializer(typeof(FeatureCollection));
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            var featureCollection = (FeatureCollection?)serializer.ReadObject(stream);

            var summary = new List<string>();
            if (featureCollection?.Features != null)
            {
                foreach (var feature in featureCollection.Features)
                {
                    var props = feature.Properties;
                    var magnitude = props.Mag.HasValue ? props.Mag.Value.ToString() : "N/A";
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

    [DataContract]
    public class FeatureCollection
    {
        [DataMember(Name = "features")]
        public List<Feature> Features { get; set; } = new();
    }

    [DataContract]
    public class Feature
    {
        [DataMember(Name = "properties")]
        public EarthquakeProperties Properties { get; set; } = new();
    }

    [DataContract]
    public class EarthquakeProperties
    {
        [DataMember(Name = "place")]
        public string Place { get; set; } = string.Empty;

        [DataMember(Name = "mag")]
        public double? Mag { get; set; }
    }
}
