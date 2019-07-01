using DataAccess.Ef.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Ef
{
    public class EfUnit : IGenUoW, IDisposable
    {
        private bool _disposed = false;

        private ProSoftDbContext _db;

        public EfUnit(ProSoftDbContext db)
        {
            _db = db;
        }
       
        private IGenRepository<Apartment> _repApartment;
        public IGenRepository<Apartment> RepApartment
        {
            get { return _repApartment ?? (_repApartment = new Repository<Apartment>(_db)); }
        }

        private IGenRepository<Indication> _repIndication;
        public IGenRepository<Indication> RepIndication
        {
            get { return _repIndication ?? (_repIndication = new Repository<Indication>(_db)); }
        }

        private IGenRepository<Meter> _repMeter;
        public IGenRepository<Meter> RepMeter
        {
            get { return _repMeter ?? (_repMeter = new Repository<Meter>(_db)); }
        }

        private IGenRepository<Address> _repAddress;
        public IGenRepository<Address> RepAddress
        {
            get { return _repAddress ?? (_repAddress = new Repository<Address>(_db)); }
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
