using CsvHelper;
using Iwannago.Data.Core.Interfaces;
using Iwannago.Data.EntityFrameworkCore.Models;
using Iwannago.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
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
            //validate input
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
                            trip_distance = default,
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
            //validate input
            if (numberOfRows == 0)
                throw new ArgumentException("Must enter a value that is greater than zero.", nameof(numberOfRows));

            var result = 0;

            using (var reader = new StreamReader(
                $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\DataFiles\\green_tripdata_2018-01.csv"))
            {
                using (var csv = new CsvReader(reader))
                {
                    var records = csv.GetRecords<GreenTripRecord>();

                    foreach (var row in records.Take(numberOfRows))
                    {
                        var trip = new TaxiCabTrip
                        {
                            Id = Guid.NewGuid(),
                            TaxiType = "Green",
                            VendorID = row.VendorID,
                            pickup_datetime = DateTime.TryParseExact(row.lpep_pickup_datetime, "M/d/yyyy H:mm", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime puDateTime) ? puDateTime : default,
                            dropoff_datetime = DateTime.TryParseExact(row.lpep_dropoff_datetime, "M/d/yyyy H:mm", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime doDateTime) ? doDateTime : default,
                            DOLocationID = row.DOLocationID,
                            PULocationID = row.PULocationID,
                            ehail_fee = row.ehail_fee,
                            fare_amount = row.fare_amount,
                            tolls_amount = row.tolls_amount,
                            tip_amount = row.tip_amount,
                            total_amount = row.total_amount,
                            trip_distance = row.trip_distance,
                            mta_tax = row.mta_tax,
                            store_and_fwd_flag = row.store_and_fwd_flag,
                            extra = row.extra,
                            improvement_surcharge = row.improvement_surcharge,
                            passenger_count = row.passenger_count,
                            payment_type = row.payment_type,
                            RatecodeID = row.RatecodeID,
                            trip_type = row.trip_type
                        };

                        _repo.Insert(trip);

                        result++;
                        _logger.LogInformation($"Inserted: {result}");
                    }

                    _logger.LogInformation($"Total number of records={result}");
                }
            }
        }

        public void LoadYellowTaxi(int numberOfRows)
        {
            //validate input
            if (numberOfRows == 0)
                throw new ArgumentException("Must enter a value that is greater than zero.", nameof(numberOfRows));

            var result = 0;

            using (var reader = new StreamReader(
                $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\DataFiles\\yellow_tripdata_2018-01.csv"))
            {
                using (var csv = new CsvReader(reader))
                {
                    var records = csv.GetRecords<YellowTripRecord>();

                    foreach (var row in records.Take(numberOfRows))
                    {
                        var trip = new TaxiCabTrip
                        {
                            Id = Guid.NewGuid(),
                            TaxiType = "Yellow",
                            VendorID = row.VendorID,
                            pickup_datetime = DateTime.TryParseExact(row.tpep_pickup_datetime, "M/d/yyyy H:mm", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime puDateTime) ? puDateTime : default,
                            dropoff_datetime = DateTime.TryParseExact(row.tpep_dropoff_datetime, "M/d/yyyy H:mm", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime doDateTime) ? doDateTime : default,
                            DOLocationID = row.DOLocationID,
                            PULocationID = row.PULocationID,
                            ehail_fee = default,
                            fare_amount = row.fare_amount,
                            tolls_amount = row.tolls_amount,
                            tip_amount = row.tip_amount,
                            total_amount = row.total_amount,
                            trip_distance = row.trip_distance,
                            mta_tax = row.mta_tax,
                            store_and_fwd_flag = row.store_and_fwd_flag,
                            extra = row.extra,
                            improvement_surcharge = row.improvement_surcharge,
                            passenger_count = row.passenger_count,
                            payment_type = row.payment_type,
                            RatecodeID = row.RatecodeID,
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
    }
}
