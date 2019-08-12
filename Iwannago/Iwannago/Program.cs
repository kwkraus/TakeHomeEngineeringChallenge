using CsvHelper;
using Iwannago.Options;
using Iwannago.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using ShellProgressBar;
using System;
using System.IO;

namespace Iwannago
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                var config = new ConfigurationBuilder()
                   .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .Build();

                var servicesProvider = BuildDi(config);
                using (servicesProvider as IDisposable)
                {
                    var runner = servicesProvider.GetRequiredService<RunnerService>();
                    runner.DoAction("Action1");

                    Console.WriteLine("Press ANY key to exit");
                    Console.ReadKey();

                    //return Parser.Default.ParseArguments<ImportOptions, InATaxiOptions>(args)
                    //   .MapResult(
                    //         (ImportOptions opts) => RunImportCommand(opts),
                    //         (InATaxiOptions opts) => RunGoCommand(opts),
                    //         (IEnumerable<Error> errs) => 1);
                }
            }
            catch (Exception ex)
            {
                // NLog: catch any exception and log it.
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
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

        static int RunGoCommand(InATaxiOptions opts)
        {
            Console.WriteLine("Go was entered");
            return 0;
        }

        private static IServiceProvider BuildDi(IConfiguration config)
        {
            return new ServiceCollection()
               .AddTransient<RunnerService>() // Runner is the custom class
               .AddLogging(loggingBuilder =>
               {
          // configure Logging with NLog
          loggingBuilder.ClearProviders();
                   loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                   loggingBuilder.AddNLog(config);
               })
               .BuildServiceProvider();
        }
    }
}
