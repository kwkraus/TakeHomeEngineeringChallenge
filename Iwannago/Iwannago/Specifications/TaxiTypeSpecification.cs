using Iwannago.Data.Core.Specifications;
using Iwannago.Data.EntityFrameworkCore.Models;
using Iwannago.Enums;
using System;
using System.Linq.Expressions;

namespace Iwannago.Specifications
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
