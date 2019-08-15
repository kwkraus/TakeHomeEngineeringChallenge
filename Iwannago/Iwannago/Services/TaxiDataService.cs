using Iwannago.Data.Core.Interfaces;
using Iwannago.Data.Core.Specifications;
using Iwannago.Data.EntityFrameworkCore.Models;
using Iwannago.Models;
using Iwannago.Options;
using Iwannago.Specifications;
using Microsoft.Extensions.Logging;

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

        public TaxiTripStats CalculateTaxiDailyStats(InATaxiOptions options)
        {
            var taxiType = new TaxiTypeSpecification(options.TaxiType);
            var tripDate = new TripDateSpecification(options.TripDate);
            Specification<TaxiCabTrip> specification = taxiType.And(tripDate);

            var spec = new TaxiDataSpecification(options.TripDate, options.TaxiType);

            var results = _repo.GetList(spec);
            _logger.LogInformation($"results returned = {results.Count}");
            return null;

        }
    }
}
