/*
 * CSE 212 Lesson 6C 
 * 
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 * 
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 * 
 * Each row represents the player's stats for a single season with a single team.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

public class Basketball
{
    public static void Run()
    {
        var players = new Dictionary<string, int>();

        using var reader = new TextFieldParser("basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields();

        // ignore header row
        while (!reader.EndOfData)
        {
            var fields = reader.ReadFields()!;
            var playerId = fields[0];
            var points = int.Parse(fields[8]);

            if (players.ContainsKey(playerId))
            {
                players[playerId] += points;
            }
            else
            {
                players[playerId] = points;
            }
        }

        var top10Players = players
            .OrderByDescending(pair => pair.Value)
            .Take(10)
            .ToList();

        Console.WriteLine("\n=== TOP 10 PLAYERS WITH HIGHEST TOTAL POINTS ===");
        Console.WriteLine($"{"Rank",-6} | {"Player ID",-15} | {"Total Points",-12}");
        Console.WriteLine(new string('-', 40));

        int rank = 1;
        foreach (var player in top10Players)
        {
            Console.WriteLine($"{rank,-6} | {player.Key,-15} | {player.Value,-12:N0}");
            rank++;
        }
    }
}