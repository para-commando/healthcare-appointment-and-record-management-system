// add the dependencies here and make sure dotnet-script is installed
// "dotnet tool install --global dotnet-script"
// and then run "dotnet script generateDiseasesData.csx" to see the result of this file execution
#r "nuget: CsvHelper, 33.0.1"

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Linq;
public static string RemovePhrase(string input, string phraseToRemove)
{
    if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(phraseToRemove))
    {
        return input; // Return original string if input or phrase is null/empty
    }

    var parts = input.Split(phraseToRemove, StringSplitOptions.None)
                    .Select(part => part.Trim())
                    .Where(part => !string.IsNullOrEmpty(part));

    return string.Join(", \n", parts);
}

var inputFilePath = "database/data/clinical_grid_data-Medicine_Details.csv";

try
{
    using (var reader = new StreamReader(inputFilePath))
    using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
    {
        // Read the CSV into a list of dynamic objects
        var records = csvReader.GetRecords<dynamic>();
        var uniqueUses = new HashSet<string>();
        var treatmentOf = new HashSet<string>();
        var preventionOf = new HashSet<string>();

        foreach (var record in records)
        {
            var uses = record.uses?.ToString();
            if (!string.IsNullOrEmpty(uses))
            {
                if (uses.Contains("Treatment of"))
                {
                    uses = RemovePhrase(uses, "Treatment of");
                }
                if (uses.Contains("Treatment and prevention of"))
                {
                    uses = RemovePhrase(uses, "Treatment and prevention of");
                }
                if (uses.Contains("Prevention of"))
                {
                    uses = RemovePhrase(uses, "Prevention of");
                }
                uniqueUses.Add(uses);
            }
        }

        // Display unique values
        Console.WriteLine("Unique values in 'uses' column:");
        Console.WriteLine("{");
        foreach (var use in uniqueUses)
        {
            Console.WriteLine($"{use},");
        }
        Console.WriteLine("}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
