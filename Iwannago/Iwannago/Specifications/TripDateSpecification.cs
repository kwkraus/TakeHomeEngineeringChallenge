using Iwannago.Data.Core.Specifications;
using Iwannago.Data.EntityFrameworkCore.Models;
using System;
using System.Linq.Expressions;

namespace Iwannago.Specifications
{
    public sealed class TripDateSpecification : Specification<TaxiCabTrip>
    {
        private readonly DateTime _pickupDate;

        public TripDateSpecification(DateTime pickupDate)
        {
            _pickupDate = pickupDate;
        }

        public override Expression<Func<TaxiCabTrip, bool>> ToExpression()
        {
            return TaxiCabTrip => TaxiCabTrip.pickup_datetime.Date == _pickupDate.Date;
        }
    }
}
