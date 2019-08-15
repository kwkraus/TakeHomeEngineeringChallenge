using Iwannago.Data.Core.Enums;
using Iwannago.Data.Core.Models;
using Iwannago.Specifications;
using System;
using System.Linq.Expressions;

namespace Iwannago.Data.Core.Specifications
{
    public sealed class TaxiTypeSpecification : Specification<TaxiCabTrip>
    {
        private readonly TaxiType _taxiType;

        public TaxiTypeSpecification(TaxiType taxiType)
        {
            _taxiType = taxiType;
        }

        public override Expression<Func<TaxiCabTrip, bool>> ToExpression()
        {
            return TaxiCabTrip => TaxiCabTrip.TaxiType == Enum.GetName(typeof(TaxiType), _taxiType);
        }
    }
}
