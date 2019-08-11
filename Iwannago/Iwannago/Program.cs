using CommandLine;
using CsvHelper;
using ShellProgressBar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Threading;

namespace Iwannago
{
    class Program
    {
        [Verb("import", HelpText = "Import Taxi data for searching.")]
        class ImportOptions
        {
            //commit options here
        }
        [Verb("go", HelpText = "Runs the go command for a start/end borough combination.")]
        class GoOptions
        {
            [Option('f', "From", Required = true, HelpText = "Name of Borough to start from.")]
            public bool From { get; set; }

            [Option('t', "To", Required = true, HelpText = "Name of Borough to end at.")]
            public bool To { get; set; }

            [Option('i', "In", Required = true, HelpText = "Type of taxi. 'YellowTaxi', 'GreenTaxi', 'ForHireTaxi'")]
            public bool TaxiType { get; set; }
        }


        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<ImportOptions, GoOptions>(args)
               .MapResult(
                 (ImportOptions opts) => RunImportCommand(opts),
                 (GoOptions opts) => RunGoCommand(opts),
                 (IEnumerable<Error> errs) => 1);
        }

        static int RunImportCommand(ImportOptions opts)
        {
            var result = 0;

            //fhv_tripdata_2018-01.csv
            using (var reader = new StreamReader("DataFiles\\fhv_tripdata_2018-01.csv"))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<TripRecord>();
                while (records.GetEnumerator().MoveNext())
                    result++;

                Console.WriteLine($"Total number of records={result}");
            }

            Console.WriteLine("Import was entered");
            const int totalTicks = 10;
            var options = new ProgressBarOptions
            {
                ProgressCharacter = '*',
                ProgressBarOnBottom = true
            };
            using (var pbar = new ProgressBar(result, "Running through all records", options))
            {
                for (var i = 0; i < result; i++)
                {
                    pbar.Tick();
                }
            }

            return 0;
        }

        static int RunGoCommand(GoOptions opts)
        {
            Console.WriteLine("Go was entered");
            return 0;
        }
    }
}
