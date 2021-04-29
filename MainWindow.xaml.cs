using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonListManipulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Person neu = new Person();
        public MainWindow()
        {
            Person.ExistingPersons.Remove(neu);
            InitializeComponent();
            Person.InitializeTestSubjects();
            if (Person.ExistingPersons.Count > 0) { DataContext = Person.ExistingPersons[0]; }
            else { DataContext = neu; }
        }

        private void PersonView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PersonView.SelectedIndex >= 0) { DataContext = Person.ExistingPersons[PersonView.SelectedIndex]; }
            else { DataContext = neu; }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            new Individual().ShowDialog();
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmation = MessageBox.Show($"Opravdu chcete permanentně odstranit položku {Person.ExistingPersons[PersonView.SelectedIndex].Name} {Person.ExistingPersons[PersonView.SelectedIndex].Surname}?", "Potvrzení", MessageBoxButton.YesNo);
            switch (confirmation)
            {
                case MessageBoxResult.Yes:
                    Person.ExistingPersons.RemoveAt(PersonView.SelectedIndex);
                    break;
                case MessageBoxResult.No:
                    DataContext = neu;
                    break;
                default:
                    DataContext = neu;
                    break;
            }
        }

        private void PersonView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new Individual(Person.ExistingPersons[PersonView.SelectedIndex]).ShowDialog();
        }
    }
}
