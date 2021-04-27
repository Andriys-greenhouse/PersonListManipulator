using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PersonListManipulator
{
    public class Person
    {
        public static ObservableCollection<Person> ExistingPersons { get; set; }  = new ObservableCollection<Person>();
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string FormatedBirthDate
        {
            get
            {
                return BirthDate.ToString("yyyy/MM/dd");
            }
        }

        public static void InitializeTestSubjects()
        {
            new Person("Jan", "Kostek", new DateTime(1984, 7, 2));
            new Person("Ander", "Valmont", new DateTime(1978, 4, 13));
            new Person("Petr", "Zemlek", new DateTime(1970, 8, 3));
            new Person("Ondřej", "Rukavica", new DateTime(1994, 10, 31));
        }

        public Person(string aName, string aSurname, DateTime aBirthDate)
        {
            Name = aName;
            Surname = aSurname;
            BirthDate = aBirthDate;
            ExistingPersons.Add(this);
        }

        public Person()
        {
            ExistingPersons.Add(this);
        }
    }
}
