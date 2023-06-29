using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Repositories
{
    public class Database : IDisposable
    {
        private static Database1Entities1 db = null;

        public void Dispose()
        {
            db.Dispose();
        }

        public static Database1Entities1 getDb()
        {
            if(db == null)
            {
                db = new Database1Entities1();
            }
            return db;
        }
    }
}