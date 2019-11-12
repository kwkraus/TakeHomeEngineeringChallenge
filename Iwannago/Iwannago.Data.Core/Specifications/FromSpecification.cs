using Iwannago.Data.Core.Models;
using System;
using System.Linq.Expressions;

namespace Iwannago.Data.Core.Specifications
{
    public class FromSpecification : Specification<TaxiCabTrip>
    {
        private readonly int _fromLoc;

        public FromSpecification(int fromLoc)
        {
            _fromLoc = fromLoc;
        }

        public override Expression<Func<TaxiCabTrip, bool>> ToExpression()
        {
            return trip => trip.PULocationID == _fromLoc;
        }
    }
}
