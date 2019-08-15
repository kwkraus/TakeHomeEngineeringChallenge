using Iwannago.Data.Core.Enums;
using Iwannago.Data.Core.Models;
using Iwannago.Data.Core.Specifications;
using System;
using System.Linq.Expressions;

namespace Iwannago.Specifications
{
    public sealed class TaxiDataSpecification : Specification<TaxiCabTrip>
    {
        private readonly DateTime _pickupDate;
        private readonly TaxiType _taxiType;
        private readonly int _pickUpLocation;
        private readonly int _dropOffLocation;

        public TaxiDataSpecification(
            int pickUpLocation,
            int dropOffLocation,
            DateTime pickUpDate, 
            TaxiType taxiType)
        {
            _pickUpLocation = pickUpLocation;
            _dropOffLocation = dropOffLocation;
            _pickupDate = pickUpDate;
            _taxiType = taxiType;
        }

        public override Expression<Func<TaxiCabTrip, bool>> ToExpression()
        {
            return TaxiCabTrip => TaxiCabTrip.pickup_datetime.Date == _pickupDate.Date
                && TaxiCabTrip.TaxiType == Enum.GetName(typeof(TaxiType), _taxiType)
                && TaxiCabTrip.PULocationID == _pickUpLocation
                && TaxiCabTrip.DOLocationID == _dropOffLocation;
        }
    }
}
