using CsvHelper;
using Iwannago.Data.Core.Enums;
using Iwannago.Data.Core.Interfaces;
using Iwannago.Data.Core.Models;
using Iwannago.Models;
using Iwannago.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly TaxiDataOptions _options;

        public CsvTaxiDataImportService(
            IRepository<TaxiCabTrip> repo,
            ILogger<CsvTaxiDataImportService> logger,
            IOptions<TaxiDataOptions> options)
        {
            _repo = repo;
            _logger = logger;
            _options = options.Value;
        }

        public void LoadForHireVehicle(int numberOfRows = 0)
        {
            //validate input
            if (numberOfRows == 0)
                throw new ArgumentException("Must enter a value that is greater than zero.", nameof(numberOfRows));

            var result = 0;

            using (var reader = new StreamReader(
                $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\DataFiles\\{_options.FHV}"))
            {
                using (var csv = new CsvReader(reader))
                {
                    var records = csv.GetRecords<FHVTripRecord>();

                    foreach (var row in records.Take(numberOfRows))
                    {
                        var trip = new TaxiCabTrip
                        {
                            Id = Guid.NewGuid(),
                            TaxiType = Enum.GetName(typeof(TaxiType), TaxiType.ForHireVehicle),
                            VendorID = row.Dispatching_base_num,
                            pickup_datetime = row.Pickup_DateTime,
                            dropoff_datetime = row.DropOff_datetime,
                            DOLocationID = row.DOlocationID == string.Empty ? default : int.Parse(row.DOlocationID),
                            PULocationID = row.PUlocationID == string.Empty ? default :int.Parse(row.PUlocationID),
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
                $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\DataFiles\\{_options.Green}"))
            {
                using (var csv = new CsvReader(reader))
                {
                    var records = csv.GetRecords<GreenTripRecord>();

                    foreach (var row in records.Take(numberOfRows))
                    {
                        var trip = new TaxiCabTrip
                        {
                            Id = Guid.NewGuid(),
                            TaxiType = Enum.GetName(typeof(TaxiType), TaxiType.Green),
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
                $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\DataFiles\\{_options.Yellow}"))
            {
                using (var csv = new CsvReader(reader))
                {
                    var records = csv.GetRecords<YellowTripRecord>();

                    foreach (var row in records.Take(numberOfRows))
                    {
                        var trip = new TaxiCabTrip
                        {
                            Id = Guid.NewGuid(),
                            TaxiType = Enum.GetName(typeof(TaxiType), TaxiType.Yellow),
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
