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

namespace App_Grosiste
{
    /// <summary>
    /// Logique d'interaction pour LoginPage.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            //db_connexion.initializeConnexion();
            //db_connexion.AddDataToDb();
        }

        public void ConnectUser(object sender, RoutedEventArgs e)
        {
            //Récupération de l'email et mot de passe de l'utilisateur
            string emailSelected = EmailInput.Text;
            string passwordSelected = PasswordInput.Password;
            string hashedPassword = db_connexion.HashPassword(passwordSelected);

            //Booleen permettant le redirect si utilisateur ok
            bool isRedirectOk = false;

            //Ajout d'un utilisateur en bdd
            //db_connexion.AddUser(emailSelected, hashedPassword);

            //Vérification des données de l'utilisateur
            db_connexion.VerifyPassword(emailSelected, hashedPassword, ref isRedirectOk);

            //Redirect en cas d'utilisateur authentifié
            if (isRedirectOk == true)
            {
                var homeWindow = new HomeWindow();

                Application.Current.MainWindow = homeWindow;
                this.Close();
                homeWindow.Show();
            }
            
        }
    }
}
