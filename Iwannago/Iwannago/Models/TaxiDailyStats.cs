using Iwannago.Enums;
using System;

namespace Iwannago.Models
{
    public class TaxiTripStats
    {
        public string TaxiType { get; set; }
        public DateTime Date { get; set; }
        public HourInTheDay Hour { get; set; }
        public int NumberOfTrips { get; set; }
        public int StartLocation { get; set; }
        public int EndLocation { get; set; }
        public decimal TotalDistance { get; set; }
        public decimal ShortestDistance { get; set; }
        public decimal LongestDistance { get; set; }
        public decimal AvgDistance { get; set; }
        public decimal TotalFare { get; set; }
        public decimal LowestFare { get; set; }
        public decimal HighestFare { get; set; }
        public decimal AvgFare { get; set; }

    }
}