using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Factories;

namespace BookStore.Repositories
{
    public class CoverTypeRepository : IDisposable
    {
        private Database1Entities1 db;
        private CoverType coverType;

        public CoverTypeRepository()
        {
            db = Database.getDb();
            coverType = new CoverType();
        }

        public void Dispose()
        {
           db.Dispose();
        }

        public void insertCoverType(string CoverTypeName, string CoverTypeMaterial)
        {
            coverType = CoverTypeFactory.createCoverType(CoverTypeName, CoverTypeMaterial);
            db.CoverTypes.Add(coverType);
            db.SaveChanges();
        }

        public void deleteCoverType(int CoverTypeID)
        {
            coverType = findCoverTypeByID(CoverTypeID);
            if (coverType != null)
            {
                db.CoverTypes.Remove(coverType);
                db.SaveChanges();
            }
        }

        public void updateCoverType(int CoverTypeID, string CoverTypeName, string CoverTypeMaterial)
        {
            coverType = findCoverTypeByID(CoverTypeID);
            if (coverType != null)
            {
                coverType.CoverTypeName = CoverTypeName;
                coverType.CoverTypeMaterial = CoverTypeMaterial;
                db.SaveChanges();
            }
        }

        public CoverType findCoverTypeByID(int CoverTypeID)
        {
            coverType = db.CoverTypes.Find(CoverTypeID);
            return coverType;
        }

        public List<CoverType> getAllCoverType()
        {
            List<CoverType> list = new List<CoverType>();
            list = (from ct in db.CoverTypes select ct).ToList();
            return list;
        }
    }
}