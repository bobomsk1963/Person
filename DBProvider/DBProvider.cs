using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DBInterface;
using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.Windows.Forms;
//using System.Collections.ObjectModel;
//using System.Reflection;
namespace DBProvider
{
    
    public class Студент : Persona
    {

        public int НомерКурса { get; set; }
        public string Факультет { get; set; }

    }

    public class Преподаватель : Persona
    {
        
        public int Возраст { get; set; }
        public Gender Пол { get; set; }

    }


    public class dbProvider : IDatabase
    {
        DbSet dbSet;
        object tbl;

        public dbProvider(DbSet dbset, object o)
        {
            dbSet = dbset;            
            tbl = o;
        }

        public Persona CretePerson()
        {
            Object o= dbSet.Create();
            return (Persona)o;
        }

        public object GetList()
        {
            return tbl;
        }

        public void Update(Persona person)
        {
            dbSet.Add(person);
        }

        public void Delete(Persona person)
        {
            dbSet.Remove(person);
        }

        public override string ToString()
        {                        
            return dbSet.ElementType.Name;             
        }
    }          
  
    
    public class Context : DbContext
    {
        // Имя будущей базы данных можно указать через
        // вызов конструктора базового класса
        public Context()
            : base("DBPerson")
        {            
        }

        // Отражение таблиц базы данных на свойства с типом DbSet
        public DbSet<Студент> Студенты { get; set; }
        public DbSet<Преподаватель> Преподаватели { get; set; }

        public List<dbProvider> DBTable()
        {
            List<dbProvider> ret = new List<dbProvider>();
            Студенты.OrderBy(Persona => Persona.ID).Load();
            ret.Add(new dbProvider(Студенты, Студенты.Local.ToBindingList()));
            Преподаватели.OrderBy(Persona => Persona.ID).Load();
            ret.Add(new dbProvider(Преподаватели, Преподаватели.Local.ToBindingList()));
            return ret;
        }
    }

    
}
