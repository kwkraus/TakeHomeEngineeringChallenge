using Iwannago.Models;
using Iwannago.Options;
using System.Threading.Tasks;

namespace Iwannago.Services
{
    public interface ITaxiDataService
    {
        Task<TaxiTripStats> CalculateTaxiDailyStatsAsync(InATaxiOptions options);
    }
}