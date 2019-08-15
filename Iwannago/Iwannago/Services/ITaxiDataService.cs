using Iwannago.Models;
using Iwannago.Options;

namespace Iwannago.Services
{
    public interface ITaxiDataService
    {
        TaxiTripStats CalculateTaxiDailyStats(InATaxiOptions options);
    }
}