using CommandLine;
using Iwannago.Data.Core.Enums;

namespace Iwannago.Options
{
    [Verb("import", HelpText = "Import Taxi data for searching.")]
    public class ImportOptions
    {
        [Option('c', "Count", Required = true, HelpText = "Number of TaxiTrip records to load.")]
        public int Count { get; set; }

        [Option('t', "TaxiType", Required = true, HelpText = "Type of TaxiTrip records to load. e.g. 'ForHireVehicle', 'Yellow', 'Green'")]
        public TaxiType TaxiType { get; set; }
    }
}
