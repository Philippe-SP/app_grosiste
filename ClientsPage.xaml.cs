using App_Grosiste.Scripts;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static App_Grosiste.Scripts.db_context;

namespace App_Grosiste
{
    /// <summary>
    /// Logique d'interaction pour ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        private readonly db_context _context;

        public ClientsPage()
        {
            InitializeComponent();
            _context = new db_context();
            LoadClients();
        }

        public void LoadClients()
        {
            List<Client> Clients = ConvertDataToList();
            ClientList.ItemsSource = Clients;
        }

        public List<Client> ConvertDataToList()
        {
            return _context.Clients.Select(c => new Client
            {
                id = c.id,
                nom = c.nom,
                adresse = c.adresse,
                siret = c.siret,
            }).ToList();
        }

        //redirection LOGIN
        public void RedirectLogin(object sender, RoutedEventArgs e)
        {
          //  this.NavigationService.Navigate(new LoginWindow());
        }

        //redirection HOME
        public void RedirectHome(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomePage());
        }

        //redirection PRODUITS
        public void RedirectProduits(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ProductPage());
        }
    }
}
