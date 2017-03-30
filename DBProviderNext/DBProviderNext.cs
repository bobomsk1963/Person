using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DBInterface;
using System.Data.Entity;
namespace DBProviderNext
{
    public class Врач : Persona
    {

        public int СтажРаботы { get; set; }
        public int  Участок { get; set; }
        public string Специальность { get; set; }

    }
    

    public class Слесарь : Persona
    {

        public Education Образование { get; set; }
        public int Разряд { get; set; }

    }

    public class Ученый : Persona
    {

        public Degree УченаяСтепень { get; set; }
        public int Возраст { get; set; }
        public int КоличествоИзобретений { get; set; }

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
            Object o = dbSet.Create();
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
            : base("DBPersonNext")
        {
        }

        // Отражение таблиц базы данных на свойства с типом DbSet
        public DbSet<Врач> Врачи { get; set; }
        public DbSet<Слесарь> Слесари { get; set; }
        public DbSet<Ученый> Ученые { get; set; }

        public List<dbProvider> DBTable()
        {
            List<dbProvider> ret = new List<dbProvider>();
            Врачи.OrderBy(Persona => Persona.ID).Load();
            ret.Add(new dbProvider(Врачи, Врачи.Local.ToBindingList()));
            Слесари.OrderBy(Persona => Persona.ID).Load();
            ret.Add(new dbProvider(Слесари, Слесари.Local.ToBindingList()));
            Ученые.OrderBy(Persona => Persona.ID).Load();
            ret.Add(new dbProvider(Ученые, Ученые.Local.ToBindingList()));
            return ret;
        }
    }

    
}
