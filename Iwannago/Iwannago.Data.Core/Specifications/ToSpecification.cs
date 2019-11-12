using Iwannago.Data.Core.Models;
using System;
using System.Linq.Expressions;

namespace Iwannago.Data.Core.Specifications
{
    public class ToSpecification : Specification<TaxiCabTrip>
    {
        private readonly int _toLoc;

        public ToSpecification(int toLoc)
        {
            _toLoc = toLoc;
        }

        public override Expression<Func<TaxiCabTrip, bool>> ToExpression()
        {
            return trip => trip.DOLocationID == _toLoc;
        }
    }
}
