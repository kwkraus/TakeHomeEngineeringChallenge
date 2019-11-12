using Iwannago.Data.Core.Interfaces;
using Iwannago.Data.Core.Models;
using Iwannago.Data.Core.Specifications;
using Iwannago.Models;
using Iwannago.Options;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Iwannago.Services
{
    public class TaxiDataService : ITaxiDataService
    {
        private readonly IRepository<TaxiCabTrip> _repo;
        private readonly ILogger<TaxiDataService> _logger;

        public TaxiDataService(IRepository<TaxiCabTrip> repo, ILogger<TaxiDataService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<TaxiTripStats> CalculateTaxiDailyStatsAsync(InATaxiOptions options)
        {
            var taxiType = new TaxiTypeSpecification(options.TaxiType);
            var tripDate = new TripDateSpecification(options.TripDate);
            var fromLocation = new FromSpecification(options.From);
            var toLocation = new ToSpecification(options.To);
            var spec = taxiType.And(tripDate.And(fromLocation).And(toLocation));

            var results = await _repo.GetListAsync(spec);
            _logger.LogInformation($"results returned = {results.Count}");

            //TODO: calculate statistics for queried results and map to TaxiTripStats to display
            TaxiTripStats stats = new TaxiTripStats();

            return stats;
        }
    }
}
