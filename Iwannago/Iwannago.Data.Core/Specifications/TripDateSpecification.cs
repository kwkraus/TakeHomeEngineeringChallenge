using Iwannago.Data.Core.Models;
using System;
using System.Linq.Expressions;

namespace Iwannago.Data.Core.Specifications
{
    public class TripDateSpecification : Specification<TaxiCabTrip>
    {
        private readonly DateTime _pickupDate;

        public TripDateSpecification(DateTime pickupDate)
        {
            _pickupDate = pickupDate;
        }

        public override Expression<Func<TaxiCabTrip, bool>> ToExpression()
        {
            return taxiCabTrip => taxiCabTrip.pickup_datetime.Date == _pickupDate.Date;
        }
    }
}
