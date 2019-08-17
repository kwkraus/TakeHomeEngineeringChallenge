using CommandLine;
using Iwannago.Data.Core.Enums;
using System;

namespace Iwannago.Options
{
    [Verb("inataxi", HelpText = "Runs the inataxi command for a start/end borough combination.")]
    public class InATaxiOptions
    {
        [Option('f', "From", Required = true, HelpText = "Name of Borough to start from.")]
        public int From { get; set; }

        [Option('t', "To", Required = true, HelpText = "Name of Borough to end at.")]
        public int To { get; set; }

        [Option('i', "In", Required = true, HelpText = "Type of taxi. 'Yellow', 'Green', 'ForHireVehicle'")]
        public TaxiType TaxiType { get; set; }

        [Option('o', "On", Required = true, HelpText = "Date of trip data you are looking for")]
        public DateTime TripDate { get; set; }

    }
}
