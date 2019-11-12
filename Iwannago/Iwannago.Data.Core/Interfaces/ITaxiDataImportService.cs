using System.Threading.Tasks;

namespace Iwannago.Data.Core.Interfaces
{
    public interface ITaxiDataImportService
    {
        Task LoadForHireVehicleAsync(int numberOfRows);
        Task LoadYellowTaxiAsync(int numberOfRows);
        Task LoadGreenTaxiAsync(int numberOfRows);
    }
}