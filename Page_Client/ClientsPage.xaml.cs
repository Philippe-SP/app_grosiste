using App_Grosiste.Scripts;
using System.Windows.Controls;
using static App_Grosiste.Scripts.db_context;

namespace App_Grosiste
{
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
    }
}
