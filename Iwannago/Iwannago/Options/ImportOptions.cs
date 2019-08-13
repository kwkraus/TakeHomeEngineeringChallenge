using CommandLine;

namespace Iwannago.Options
{
    [Verb("import", HelpText = "Import Taxi data for searching.")]
    class ImportOptions
    {
        [Option('c', "Count", Required = true, HelpText = "Number of TaxiTrip records to load.")]
        public int Count { get; set; }

        [Option('t', "TaxiType", Required = true, HelpText = "Type of TaxiTrip records to load. e.g. 'FHV', 'Yellow', 'Green'")]
        public string TaxiType { get; set; }
    }
}
