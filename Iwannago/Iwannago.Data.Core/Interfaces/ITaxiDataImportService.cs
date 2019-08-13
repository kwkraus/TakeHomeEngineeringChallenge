namespace Iwannago.Data.Core.Interfaces
{
    public interface ITaxiDataImportService
    {
        void LoadForHireVehicle(int numberOfRows);
        void LoadYellowTaxi(int numberOfRows);
        void LoadGreenTaxi(int numberOfRows);
    }
}