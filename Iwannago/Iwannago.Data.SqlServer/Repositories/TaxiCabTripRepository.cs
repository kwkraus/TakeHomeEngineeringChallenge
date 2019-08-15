﻿using Iwannago.Data.EntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Iwannago.Data.EntityFrameworkCore.Repositories
{
    public class TaxiCabTripRepository : EFRepository<TaxiCabTrip>
    {
        public TaxiCabTripRepository(DbContext context) : base(context)
        { }
    }
}