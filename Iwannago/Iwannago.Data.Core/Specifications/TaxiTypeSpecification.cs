using Iwannago.Data.Core.Enums;
using Iwannago.Data.Core.Models;
using System;
using System.Linq.Expressions;

namespace Iwannago.Data.Core.Specifications
{
    public class TaxiTypeSpecification : Specification<TaxiCabTrip>
    {
        private readonly string _taxiType;

        public TaxiTypeSpecification(TaxiType taxiType)
        {
            _taxiType = Enum.GetName(typeof(TaxiType), taxiType);
        }

        public override Expression<Func<TaxiCabTrip, bool>> ToExpression()
        {
            return trip => trip.TaxiType == _taxiType;
        }
    }
}
