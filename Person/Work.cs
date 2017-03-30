using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;
namespace Person
{
    public class DBWork
    {
        DbContext DB;

        public DBWork(DbContext db)
        {
            DB = db;
        }

        public void Close()
        {
            DB.Dispose();
        }


        public int SaveChanges()
        {
           return DB.SaveChanges();
        }

        public void EntryState(object p, bool save)
        {
            if (save)
            {
                DB.Entry(p).State = EntityState.Modified;
            }
            else { DB.Entry(p).State = EntityState.Unchanged; }
            DB.SaveChanges();            
        }

    }
}
