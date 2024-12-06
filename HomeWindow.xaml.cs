using System.Windows;

namespace App_Grosiste
{
    /// <summary>
    /// Logique d'interaction pour HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            MainFrame.Content = new HomePage();
        }

        //redirection HOME
        public void RedirectHome(object sender, RoutedEventArgs e)
        {
            //Redirect vers la bonne page
            MainFrame.Content = new HomePage();
            
            //Augmentation de l'epaisseur du text du bouton
            AccueilBtn.FontWeight = FontWeights.ExtraBold;

            //Modification des text des autres boutons
            ProduitBtn.FontWeight = FontWeights.Regular;
            ClientBtn.FontWeight = FontWeights.Regular;
            LoginBtn.FontWeight = FontWeights.Regular;
            AdminBtn.FontWeight = FontWeights.Regular;
        }

        //redirection LOGIN
        public void RedirectLogin(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();

            Application.Current.MainWindow = loginWindow;
            this.Close();
            loginWindow.Show();
        }

        //redirection CLIENTS
        public void RedirectClients(object sender, RoutedEventArgs e)
        {
            //Redirect vers la bonne page
            MainFrame.Content = new ClientsPage();

            //Augmentation de l'epaisseur du text du bouton
            ClientBtn.FontWeight = FontWeights.ExtraBold;

            //Modification des text des autres boutons
            ProduitBtn.FontWeight = FontWeights.Regular;
            AccueilBtn.FontWeight = FontWeights.Regular;
            LoginBtn.FontWeight = FontWeights.Regular;
            AdminBtn.FontWeight = FontWeights.Regular;
        }

        //redirection PRODUITS
        public void RedirectProduits(object sender, RoutedEventArgs e)
        {
            //Redirect vers la bonne page
            MainFrame.Content = new ProductPage();

            //Augmentation de l'epaisseur du text du bouton
            ProduitBtn.FontWeight = FontWeights.ExtraBold;

            //Modification des text des autres boutons
            AccueilBtn.FontWeight = FontWeights.Regular;
            ClientBtn.FontWeight = FontWeights.Regular;
            LoginBtn.FontWeight = FontWeights.Regular;
            AdminBtn.FontWeight = FontWeights.Regular;
        }

        //redirection ADMIN
        public void RedirectAdmin(object sender, RoutedEventArgs e)
        {
            //Redirect vers la bonne page
            MainFrame.Content = new Page_Admin.AdminPage();

            //Augmentation de l'epaisseur du text du bouton
            AdminBtn.FontWeight = FontWeights.ExtraBold;

            //Modification des text des autres boutons
            AccueilBtn.FontWeight = FontWeights.Regular;
            ClientBtn.FontWeight = FontWeights.Regular;
            LoginBtn.FontWeight = FontWeights.Regular;
            ProduitBtn.FontWeight = FontWeights.Regular;
        }
    }
}
