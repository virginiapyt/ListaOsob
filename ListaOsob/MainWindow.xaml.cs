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
using System.ComponentModel;

namespace ListaOsob
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Osoba> Personlist { get; set; }
        public Osoba choosenPerson { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            PrzygotujWiazanie();           

        }

        private void PrzygotujWiazanie()
        {
            Personlist = new ObservableCollection<Osoba>();
            Personlist.Add(new Osoba("Ala", "Kowalska", new DateTime(2000, 12, 13)));
            Personlist.Add(new Osoba("Ola", "Nowak", new DateTime(2012, 4, 5)));
            Personlist.Add(new Osoba("Ula", "Abacka", new DateTime(2004, 1, 25)));
            Personlist.Add(new Osoba("Beata", "Abacka", new DateTime(2004, 1, 25)));
            Personlist.Add(new Osoba("Ola", "Abacka", new DateTime(2004, 1, 25)));
            Personlist.Add(new Osoba("Alina", "Abacka", new DateTime(2004, 1, 25)));
            DataContext = this;

            CollectionView widok =(CollectionView)CollectionViewSource.GetDefaultView(Personlist);
            widok.SortDescriptions.Add(new SortDescription("LastName", ListSortDirection.Ascending));
            widok.SortDescriptions.Add(new SortDescription("FirstName", ListSortDirection.Ascending));

            widok.Filter = Filtruzytkownika;
        }

        private bool Filtruzytkownika(object obj)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return (obj as Osoba).LastName.
                    IndexOf(txtFilter.Text,StringComparison.OrdinalIgnoreCase) >= 0;
        }
        //filtr decyduje dla każdego obiektu czy pokazać czy nie pokazać na liście
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(Personlist).Refresh();
        }

        private void btn_dodaj_Click(object sender, RoutedEventArgs e)
        {
            var osoba = new Osoba();
            var window = new WindowOsoba(osoba);
            var result = window.ShowDialog();
            if (result == true)
                Personlist.Add(osoba);


        }
    }
}
