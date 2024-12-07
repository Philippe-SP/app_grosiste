using System.IO;
using System.Windows;

namespace App_Grosiste
{
    /// <summary>
    /// Logique d'interaction pour LoginPage.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        //Chemin absolu d'accès au fichier logs.txt
        private string logFilePath = @"C:\Users\phili\OneDrive\Bureau\projets Ynov\App_Grosiste\App_Grosiste\logs.txt";

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
                //Ajout d'un log
                try
                {
                    //Initialisation de l'ecriture du log dans un using pour permettre l'ouverture et la fermture auto du fichier
                    using (StreamWriter logsWriter = new StreamWriter(logFilePath, append: true))
                    {
                        logsWriter.WriteLine($"[{DateTime.Now}] INFO: Utilisateur authentifié, accès à l'application.");
                        logsWriter.Flush();//Ecris directement dans le fichier avant fermeture de celui-ci
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Echec de l'ecriture du log: " + ex.ToString());
                }

                var homeWindow = new HomeWindow();

                Application.Current.MainWindow = homeWindow;
                this.Close();
                homeWindow.Show();
            }
            
        }
    }
}
