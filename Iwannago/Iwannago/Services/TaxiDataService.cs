using Iwannago.Data.Core.Interfaces;
using Iwannago.Data.EntityFrameworkCore.Models;
using Iwannago.Enums;
using Iwannago.Models;
using Iwannago.Options;
using Iwannago.Specifications;
using Microsoft.Extensions.Logging;
using System;

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
            var results = _repo.GetList(new TaxiDataSpecification(options.TripDate, options.TaxiType));
            _logger.LogInformation($"results returned = {results.Count}");
            return null;

        }
    }
}
