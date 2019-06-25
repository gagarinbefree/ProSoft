using DataAccess.Ef.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public interface IGenUoW : IDisposable
    {
        IGenRepository<Meter> RepMeter { get; }
        IGenRepository<Indication> RepIndication { get; }
        IGenRepository<Apartment> RepApartment { get; }
    }
}
