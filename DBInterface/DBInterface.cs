using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace DBInterface
{

    public enum Gender { Муж, Жен }
    public enum Education { Начальное, НеполноеСреднее, Среднее, Высшее }
    public enum Degree { Кандидат, Доктор, Академик, Доцент }

    public class Persona
    {        
        
        [ReadOnly(true)]
        public int ID { get; set; }
        public string ФИО { get; set; }        
    }

    public interface IDatabase
    {
        object GetList();
        Persona CretePerson();
        void Update(Persona person);
        void Delete(Persona person);
    }
}
