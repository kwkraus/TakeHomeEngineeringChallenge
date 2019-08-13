using CommandLine;
using CsvHelper;
using Iwannago.Data.Core.Interfaces;
using Iwannago.Data.EntityFrameworkCore.Contexts;
using Iwannago.Data.EntityFrameworkCore.Models;
using Iwannago.Data.EntityFrameworkCore.Repositories;
using Iwannago.Models;
using Iwannago.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Iwannago
{
    class Program
    {
        static int Main(string[] args)
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
                    var repo = servicesProvider.GetRequiredService<IRepository<TaxiCabTrip>>();

                    return Parser.Default.ParseArguments<ImportOptions, InATaxiOptions>(args)
                       .MapResult(
                             (ImportOptions opts) => RunImportCommand(opts, repo, logger),
                             (InATaxiOptions opts) => RunGoCommand(opts, repo, logger),
                             (IEnumerable<Error> errs) => 1);
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

        static int RunImportCommand(ImportOptions opts, IRepository<TaxiCabTrip> repo, Logger logger)
        {
            var result = 0;
            logger.Trace("Import was entered");

            using (var reader = new StreamReader($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\DataFiles\\fhv_tripdata_2018-01.csv"))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<FHVTripRecord>();

                foreach(var row in records.Take(1000))
                {
                    var id = Guid.NewGuid();

                    var trip = new TaxiCabTrip
                    {
                        Id = id,
                        TaxiType = "FHV",
                        VendorID = row.Dispatching_base_num,
                        pickup_datetime = row.Pickup_DateTime,
                        dropoff_datetime = row.DropOff_datetime,
                        DOLocationID = int.Parse(row.DOlocationID),
                        PULocationID = int.Parse(row.PUlocationID),
                        ehail_fee = default(string),
                        fare_amount = default(decimal),
                        tolls_amount = default(decimal),
                        tip_amount = default(decimal),
                        total_amount = default(decimal),
                        trip_distance = default(int),
                        mta_tax = default(decimal),
                        store_and_fwd_flag = row.SR_Flag,
                        extra = default(decimal),
                        improvement_surcharge = default(decimal),
                        passenger_count = default(int),
                        payment_type = default(int),
                        RatecodeID = default(int),
                        trip_type = default(int)
                    };

                    repo.Insert(trip);

                    var entity = repo.Get(id);

                    result++;
                    logger.Trace($"{result} - {entity.Id}-{entity.pickup_datetime}-{entity.dropoff_datetime}");

                }

                logger.Trace($"Total number of records={result}");
            }

            return 0;
        }

        static int RunGoCommand(InATaxiOptions opts, IRepository<TaxiCabTrip> repof, Logger logger)
        {
            logger.Info("Go was entered");
            return 0;
        }

        private static IServiceProvider BuildDi(IConfiguration config)
        {
            var connStr = config.GetConnectionString("DefaultConnection");
            Console.WriteLine(connStr);

            return new ServiceCollection()
                .AddDbContext<DbContext, TaxiCabContext>(options =>
                    options.UseSqlServer(connStr))

                .AddTransient(typeof(IRepository<>), typeof(EFRepository<>))
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
