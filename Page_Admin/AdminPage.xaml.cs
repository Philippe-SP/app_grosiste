using App_Grosiste.Scripts;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace App_Grosiste.Page_Admin
{
    public partial class AdminPage : Page
    {
        //Récupération du context de la base de données
        private readonly db_context _context;

        //Chemin absolu d'accès au fichier logs.txt
        private string logFilePath = @"C:\Users\phili\OneDrive\Bureau\projets Ynov\App_Grosiste\App_Grosiste\logs.txt";

        //Variable globale permettant d'afficher le dernier produit vendu
        public static class GlobalVariableProductSelled
        {
            public static string lastProductSelled = "Aucun produits vendus aujourd'hui";
        }

        public AdminPage()
        {
            InitializeComponent();
            _context = new db_context();
        }

        //Ouverture du fichier logs pour l'utilisateur
        public void ShowLogs_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = logFilePath,
                UseShellExecute = true // permet d'ouvrir le fichier avec l'application par defaut
            });
        }

        /* ************************* Gestion des clients **************************** */

        //Supression d'un client
        public void SupClient_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string clientToSupName = ClientSupName.Text;
            string clientToSupSiret = ClientSupSiret.Text;

            // Récupération du client en fonction du nom et du SIRET
            var clientToDelete = _context.Clients
                .FirstOrDefault(c => c.nom == clientToSupName && c.siret == clientToSupSiret);

            // Vérifie si un client a été trouvé
            if (clientToDelete != null)
            {
                // Supprime le client
                _context.Clients.Remove(clientToDelete);

                // Sauvegarde les changements dans la base de données
                _context.SaveChanges();

                // Afficher un message de succès
                MessageBox.Show("Client supprimé avec succès.");

                //Ajout d'un log
                try
                {
                    //Initialisation de l'ecriture du log dans un using pour permettre l'ouverture et la fermture auto du fichier
                    using (StreamWriter logsWriter = new StreamWriter(logFilePath, append: true))
                    {
                        logsWriter.WriteLine($"[{DateTime.Now}] INFO: Supression du client: {clientToSupName}.");
                        logsWriter.Flush();//Ecris directement dans le fichier avant fermeture de celui-ci
                    } 
                } 
                catch (Exception ex)
                {
                    MessageBox.Show("Echec de l'ecriture du log: " + ex.ToString());
                }

            }
            else
            {
                // Si aucun client n'est trouvé
                MessageBox.Show("Client introuvable.");
            }
        }

        //Ajout d'un client
        public void AddClient_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string clientToAddName = ClientAddName.Text;
            string clientToAddAdress = ClientAddAdress.Text;
            string clientToAddSiret = ClientAddSiret.Text;

            //Vérification que les valeurs entrées correspondent bien au format requis
            if (clientToAddName.Any(char.IsDigit) || clientToAddName == null)
            {
                MessageBox.Show("Nom invalide");
            } 
            else if (!Regex.IsMatch(clientToAddAdress, @"^\d+\s[a-zA-Zà-ÿÀ-Ÿ\s-]+$") || clientToAddAdress == null)
            {
                MessageBox.Show("Adresse invalide");
            } 
            else if (!Regex.IsMatch(clientToAddSiret, @"^[0-9]{14}$") || clientToAddSiret == null)
            {
                MessageBox.Show("N° de siret invalide");
            } else
            {
                //Création du client a ajouté
                var newClient = new db_context.Client
                {
                    nom = clientToAddName,
                    adresse = clientToAddAdress,
                    siret = clientToAddSiret,

                };

                //Ajout du client
                void AddClient()
                {
                    _context.Clients.Add(newClient);
                }

                AddClient();
                _context.SaveChanges();

                MessageBox.Show("Client ajouté avec succès.");

                //Ajout d'un log
                try
                {
                    using (StreamWriter logsWriter = new StreamWriter(logFilePath, append: true))
                    {
                        logsWriter.WriteLine($"[{DateTime.Now}] INFO: Ajout du client: {newClient.nom}.");
                        logsWriter.Flush();
                    }
                } 
                catch (Exception ex) 
                {
                    MessageBox.Show("Echec de l'ecriture du log: " + ex.ToString()); 
                }
                
            }
        }
        /* ********************* Fin de gestion des clients ************************* */

        /* ************************* Gestion des produits *************************** */

        //Vente d'un ou plusieurs produit
        public void SellProduct_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string productToSellName = ProductSellName.Text;
            string productQteToSell = ProductSellQte.Text;

            //Vente du produit 
            void SellProduct()
            {
                //Récupération du produit a vendre
                var produit = _context.Produits
                    .Where(p => p.nom == productToSellName)
                    .FirstOrDefault();

                //Vérification que le produit est présent en stock
                if (produit != null)
                {
                    produit.quantite = produit.quantite - int.Parse(productQteToSell);//Modif
                    _context.SaveChanges();//Sauvegarde de la modification en base de données

                    GlobalVariableProductSelled.lastProductSelled = produit.nom;//Récpération du nom du produit vendu

                    MessageBox.Show("Produit(s) vendu(s) avec succès.");

                    //Ajout d'un log
                    try
                    {
                        using (StreamWriter logsWriter = new StreamWriter(logFilePath, append: true))
                        {
                            logsWriter.WriteLine($"[{DateTime.Now}] INFO: Vente de {productQteToSell} {produit.nom}. - Stock restant: {produit.quantite}.");
                            logsWriter.Flush();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Echec de l'ecriture du log: " + ex.ToString());
                    }

                }
                else
                {
                    MessageBox.Show("Produit non trouvé en stock");
                }
            }

            SellProduct();
        }

        //Ajout de produits
        public void AddProduct_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string productToAddName = ProductAddName.Text;
            string productToAddQte = ProductAddQte.Text;
            string productToAddPrice = ProductAddPrice.Text;
            string productToAddCategorie = ProductAddCategorie.Text;
            string productToAddPlace = ProductAddPlace.Text;

            //Vérification que les valeurs entrées correspondent bien au format requis
            if (productToAddName.Any(char.IsDigit) || productToAddName == null)
            {
                MessageBox.Show("Nom invalide");
            }
            else if (!Regex.IsMatch(productToAddQte, @"[0-9]") || productToAddQte == null)
            {
                MessageBox.Show("Quantitée invalide");
            }
            else if (!Regex.IsMatch(productToAddPrice, @"[0-9]") || productToAddPrice == null)
            {
                MessageBox.Show("Prix invalide");
            }
            else if (!Regex.IsMatch(productToAddCategorie, @"[0-9]") || productToAddCategorie == null)
            {
                MessageBox.Show("ID de catégorie invalide");
            }
            else if (!Regex.IsMatch(productToAddPlace, @"[0-9]") || productToAddPlace == null)
            {
                MessageBox.Show("ID d'emplacement invalide");
            }
            else
            {
                //Création du produit a ajouté
                var newProduct = new db_context.Produit
                {
                    nom = productToAddName,
                    quantite = int.Parse(productToAddQte),
                    prix = int.Parse(productToAddPrice),
                    categorieID = int.Parse(productToAddCategorie),
                    emplacement = int.Parse(productToAddPlace),

                };

                //Ajout du produit
                void AddProduct()
                {
                    _context.Produits.Add(newProduct);
                }

                AddProduct();
                _context.SaveChanges();
                MessageBox.Show("Produit(s) ajouté(s) avec succès.");

                //Ajout d'un log
                try
                {
                    using (StreamWriter logsWriter = new StreamWriter(logFilePath, append: true))
                    {
                        logsWriter.WriteLine($"[{DateTime.Now}] INFO: Ajout du produit: {newProduct.nom}, quantitée: {newProduct.quantite}.");
                        logsWriter.Flush();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Echec de l'ecriture du log: " + ex.ToString());
                }

            }
        }
        /* ********************* Fin de gestion des produits ************************ */
    }
}
