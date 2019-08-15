using Iwannago.Data.Core.Specifications;
using Iwannago.Data.EntityFrameworkCore.Models;
using Iwannago.Enums;
using System;
using System.Linq.Expressions;

namespace Iwannago.Specifications
{
    public sealed class TaxiDataSpecification : Specification<TaxiCabTrip>
    {
        private readonly DateTime _pickupDate;
        private readonly TaxiType _taxiType;

        public TaxiDataSpecification(DateTime pickupDate, TaxiType taxiType)
        {
            _pickupDate = pickupDate;
            _taxiType = taxiType;
        }

        public override Expression<Func<TaxiCabTrip, bool>> ToExpression()
        {
            return TaxiCabTrip => TaxiCabTrip.pickup_datetime.Date == _pickupDate.Date
                && TaxiCabTrip.TaxiType == Enum.GetName(typeof(TaxiType), _taxiType);
        }
    }
}
