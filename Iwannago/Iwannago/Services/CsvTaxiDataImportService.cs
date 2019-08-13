using CsvHelper;
using Iwannago.Data.Core.Interfaces;
using Iwannago.Data.EntityFrameworkCore.Models;
using Iwannago.Models;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

namespace Iwannago.Services
{
    public class CsvTaxiDataImportService : ITaxiDataImportService
    {
        private readonly IRepository<TaxiCabTrip> _repo;
        private readonly ILogger<CsvTaxiDataImportService> _logger;

        public CsvTaxiDataImportService(
            IRepository<TaxiCabTrip> repo,
            ILogger<CsvTaxiDataImportService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public void LoadForHireVehicle(int numberOfRows = 0)
        {
            if (numberOfRows == 0)
                throw new ArgumentException("Must enter a value that is greater than zero.", nameof(numberOfRows));

            var result = 0;

            using (var reader = new StreamReader(
                $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\DataFiles\\fhv_tripdata_2018-01.csv"))
            {
                using (var csv = new CsvReader(reader))
                {
                    var records = csv.GetRecords<FHVTripRecord>();

                    foreach (var row in records.Take(numberOfRows))
                    {
                        var trip = new TaxiCabTrip
                        {
                            Id = Guid.NewGuid(),
                            TaxiType = "FHV",
                            VendorID = row.Dispatching_base_num,
                            pickup_datetime = row.Pickup_DateTime,
                            dropoff_datetime = row.DropOff_datetime,
                            DOLocationID = int.Parse(row.DOlocationID),
                            PULocationID = int.Parse(row.PUlocationID),
                            ehail_fee = default,
                            fare_amount = default,
                            tolls_amount = default,
                            tip_amount = default,
                            total_amount = default,
                            trip_distance = default(int),
                            mta_tax = default,
                            store_and_fwd_flag = row.SR_Flag,
                            extra = default,
                            improvement_surcharge = default,
                            passenger_count = default,
                            payment_type = default,
                            RatecodeID = default,
                            trip_type = default
                        };

                        _repo.Insert(trip);

                        result++;
                        _logger.LogInformation($"Inserted: {result}");
                    }

                    _logger.LogInformation($"Total number of records={result}");
                }
            }
        }

        public void LoadGreenTaxi(int numberOfRows)
        {
            throw new System.NotImplementedException();
        }

        public void LoadYellowTaxi(int numberOfRows)
        {
            throw new System.NotImplementedException();
        }
    }
}
