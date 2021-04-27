using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace PersonListManipulator
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Individual : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        bool AtemptedToSubmit { get; set; } = false;
        bool CreatingNew { get; set; } = false;
        bool CalledByAdd { get; set; } = false;

        Person individual = new Person();

        //constructors
        public Individual()
        {
            InitializeComponent();
            NameBox.DataContext = individual;
            DataContext = this;
            CreatingNew = true;
            year = individual.BirthDate.Year;
            month = individual.BirthDate.Month;
            day = individual.BirthDate.Day;
        }

        public Individual(Person aIndividual)
        {
            InitializeComponent();
            Person.ExistingPersons.Remove(individual);
            individual = aIndividual;
            NameBox.DataContext = individual;
            SurnameBox.DataContext = individual;
            DataContext = this;
            year = individual.BirthDate.Year;
            month = individual.BirthDate.Month;
            day = individual.BirthDate.Day;
        }


        //name
        public string NameErrText
        {
            get
            {
                if (NameBox.Text.Length < 2 && AtemptedToSubmit) { return "Jméno musí mít alespoň 2 znaky."; }
                if (NameBox.Text.Length > 20 && AtemptedToSubmit) { return "Jméno nesmí obsahovat více než 20 znaků."; }
                else { return ""; }
            }
        }

        public int NameErrHeight
        {
            get
            {
                if (NameErrText == "") { return 2; }
                else { return 15; }
            }
        }

        public Visibility NameErrVis
        {
            get
            {
                if(NameErrText == "") { return Visibility.Hidden; }
                else { return Visibility.Visible; }
            }
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NameErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NameErrHeight"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NameErrVis"));
        }


        //surname
        public string SurnameErrText
        {
            get
            {
                if (SurnameBox.Text.Length < 2 && AtemptedToSubmit) { return "Přijmení musí mít alespoň 2 znaky."; }
                if (SurnameBox.Text.Length > 20 && AtemptedToSubmit) { return "Přijmení nesmí obsahovat více než 20 znaků."; }
                else { return ""; }
            }
        }

        public int SurnameErrHeight
        {
            get
            {
                if (SurnameErrText == "") { return 2; }
                else { return 15; }
            }
        }

        public Visibility SurnameErrVis
        {
            get
            {
                if (SurnameErrText == "") { return Visibility.Hidden; }
                else { return Visibility.Visible; }
            }
        }
        private void SurnameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SurnameErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SurnameErrHeight"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SurnameErrVis"));
        }



        //birth date
        bool dayInvallid { get; set; } = false;
        int day;
        public int Day
        {
            get { return day; }
            set
            {
                day = value;
                try 
                { 
                    individual.BirthDate = new DateTime(Year, Month, value);
                    dayInvallid = false;
                }
                catch (ArgumentException e) { dayInvallid = true; }
            }
        }
        bool monthInvallid { get; set; } = false;
        int month;
        public int Month
        {
            get { return month; }
            set
            {
                month = value;
                try 
                { 
                    individual.BirthDate = new DateTime(Year, value, Day);
                    monthInvallid = false;
                }
                catch (ArgumentException a) { monthInvallid = true; }
            }
        }
        bool yearInvallid { get; set; } = false;
        int year;
        public int Year
        {
            get { return year; }
            set
            {
                year = value;
                try 
                { 
                    individual.BirthDate = new DateTime(value, Month, Day);
                    yearInvallid = false;
                }
                catch (ArgumentException e) { yearInvallid = true; }
            }
        }
        private void DayBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Day"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BirthErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BirthErrVis"));
        }

        private void MonthBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Month"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BirthErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BirthErrVis"));
        }
        private void YearBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Year"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BirthErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BirthErrVis"));
        }

        public string BirthErrText
        {
            get
            {
                if ((yearInvallid || monthInvallid ||dayInvallid) && AtemptedToSubmit) { return "Špatně zadaný rok, měsíc nebo den."; }
                else { return ""; }
            }
        }

        public Visibility BirthErrVis
        {
            get
            {
                if (BirthErrText == "") { return Visibility.Hidden; }
                else { return Visibility.Visible; }
            }
        }

        //buttons
        private void AddButt_Click(object sender, RoutedEventArgs e)
        {
            AtemptedToSubmit = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NameErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NameErrHeight"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NameErrVis"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SurnameErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SurnameErrHeight"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SurnameErrVis"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BirthErrText"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BirthErrVis"));
            if (NameErrText == "" && SurnameErrText == "" && BirthErrText == "") 
            {
                CalledByAdd = true;    
                Close(); 
            }
            else { MessageBox.Show("Některé položky nebyly vyplněny správně."); }
        }

        private void CancelButt_Click(object sender, RoutedEventArgs e)
        {
            if (!CreatingNew) { MessageBox.Show("Editaci existující osoby nelze ukončit."); }
            else
            {
                Person.ExistingPersons.Remove(individual);
                Close();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            if (!CreatingNew && !CalledByAdd) { MessageBox.Show("Editaci existující osoby nelze ukončit."); }
            else
            {
                if (Person.ExistingPersons.Contains(individual) && !CalledByAdd && CreatingNew) { Person.ExistingPersons.Remove(individual); }
                base.OnClosed(e);
            }
        }
    }
}
