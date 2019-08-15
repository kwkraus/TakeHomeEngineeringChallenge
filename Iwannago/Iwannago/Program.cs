using CommandLine;
using Iwannago.Data.Core.Interfaces;
using Iwannago.Data.EntityFrameworkCore.Contexts;
using Iwannago.Data.EntityFrameworkCore.Models;
using Iwannago.Data.EntityFrameworkCore.Repositories;
using Iwannago.Options;
using Iwannago.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;

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
                    var importSvc = servicesProvider.GetRequiredService<ITaxiDataImportService>();
                    var taxiDataSvc = servicesProvider.GetRequiredService<ITaxiDataService>();

                    return Parser.Default.ParseArguments<ImportOptions, InATaxiOptions>(args)
                       .MapResult(
                             (ImportOptions opts) => RunImportCommand(opts, importSvc),
                             (InATaxiOptions opts) => RunGoCommand(opts, taxiDataSvc),
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

        static int RunImportCommand(ImportOptions options, ITaxiDataImportService importSvc)
        {
            //validate input
            if (options is null)
                throw new ArgumentNullException(nameof(options));
            
            if (importSvc is null)
                throw new ArgumentNullException(nameof(importSvc));

            //check what user entered for TaxiType and load appropriate dataset
            switch(options.TaxiType.ToLower())
            {
                case "fhv":
                    importSvc.LoadForHireVehicle(options.Count);
                    break;

                case "yellow":
                    importSvc.LoadYellowTaxi(options.Count);
                    break;

                case "green":
                    importSvc.LoadGreenTaxi(options.Count);
                    break;

                default:
                    throw new ArgumentException($"Argument '{nameof(options.TaxiType)}'={options.TaxiType} is not a valid option.  Please refer to --help");
            }

            return 0;
        }

        static int RunGoCommand(InATaxiOptions options, ITaxiDataService svc)
        {
            var results = svc.CalculateTaxiDailyStats(options);
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
                .AddTransient(typeof(ITaxiDataImportService), typeof(CsvTaxiDataImportService))
                .AddTransient(typeof(ITaxiDataService), typeof(TaxiDataService))
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
