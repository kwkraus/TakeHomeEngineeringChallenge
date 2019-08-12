using CommandLine;

namespace Iwannago.Options
{
    [Verb("inataxi", HelpText = "Runs the inataxi command for a start/end borough combination.")]
    class InATaxiOptions
    {
        [Option('f', "From", Required = true, HelpText = "Name of Borough to start from.")]
        public bool From { get; set; }

        [Option('t', "To", Required = true, HelpText = "Name of Borough to end at.")]
        public bool To { get; set; }

        [Option('i', "In", Required = true, HelpText = "Type of taxi. 'YellowTaxi', 'GreenTaxi', 'ForHireTaxi'")]
        public bool TaxiType { get; set; }
    }
}
