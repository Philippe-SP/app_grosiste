using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;

namespace App_Grosiste
{
    public static class db_connexion
    {
        private static readonly string connectionString = @"Data Source=../../../libraryManage.db;Version=3";

        public static void initializeConnexion()
        {
            if (!File.Exists(connectionString))
            {
                SQLiteConnection.CreateFile(@"../../../libraryManage.db");

                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    //Création des tables
                    //Table Clients
                    string createClientsTable = @"
                    CREATE TABLE IF NOT EXISTS Clients (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            nom TEXT NOT NULL,
                            adresse TEXT NOT NULL,
                            siret TEXT NOT NULL
                    );";

                    //Table Categories
                    string createCategoriesTable = @"
                    CREATE TABLE IF NOT EXISTS Categories (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            nom TEXT NOT NULL
                    );";

                    //Table Users
                    string createUsersTable = @"
                    CREATE TABLE IF NOT EXISTS Users (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            email TEXT NOT NULL,
                            password TEXT NOT NULL
                    );";

                    //Table Produits
                    string createProduitsTable = @"
                    CREATE TABLE IF NOT EXISTS Produits (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            quantite INTEGER NOT NULL,
                            prix INTEGER NOT NULL,
                            nom TEXT NOT NULL,
                            categorieID INTEGER NOT NULL,
                            emplacement INTEGER NOT NULL,
                            FOREIGN KEY (categorieID) REFERENCES Categories(id)
                    );";

                    //Table Order
                    string createOrderTable = @"
                    CREATE TABLE IF NOT EXISTS Produits (
                            clientID INTEGER PRIMARY KEY,
                            produitID INTEGER NOT NULL,
                            quantite INTEGER NOT NULL,
                            dateComm DATE NOT NULL,
                            statut TEXT NOT NULL,
                            FOREIGN KEY (clientID) REFERENCES Clients(id),
                            FOREIGN KEY (produitID) REFERENCES Produits(id)
                    );";

                    using (var command = new SQLiteCommand(connection))
                    {
                        //Execution des CREATE TABLE
                        command.CommandText = createClientsTable;
                        command.ExecuteNonQuery();

                        command.CommandText = createCategoriesTable;
                        command.ExecuteNonQuery();

                        command.CommandText = createUsersTable;
                        command.ExecuteNonQuery();

                        command.CommandText = createProduitsTable;
                        command.ExecuteNonQuery();

                        command.CommandText = createOrderTable;
                        command.ExecuteNonQuery();
                    };
                };

            }
        }

        public static void AddDataToDb()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                //Ajout des clients
                string insertClients1 = @"INSERT INTO Clients (nom, adresse, siret) VALUES ('EDF', '3 rue des alobroches', '41006906600042');";
                string insertClients2 = @"INSERT INTO Clients (nom, adresse, siret) VALUES ('SNCF', '5 rue Saint Paul', '41006906600042');";
                string insertClients3 = @"INSERT INTO Clients (nom, adresse, siret) VALUES ('Atos', '34 rue de la soie', '48397413500020');";

                //Ajout des catégories
                string insertCat1 = @"INSERT INTO Categories (nom) VALUES ('Electroniques');";
                string insertCat2 = @"INSERT INTO Categories (nom) VALUES ('Alimentaire');";
                string insertCat3 = @"INSERT INTO Categories (nom) VALUES ('Hygiene');";


                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = insertCat1;
                    command.ExecuteNonQuery();

                    command.CommandText = insertCat2;
                    command.ExecuteNonQuery();

                    command.CommandText = insertCat3;
                    command.ExecuteNonQuery();
                }
            }
        }

        //Hash du mot de passe
        public static string HashPassword(string password)
        {

            using (SHA256 sha256 = SHA256.Create())
            {
                //Hash du mot de passe avec PBKDF2
                byte[] hashedPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                //Convertion du mot de passe en chaine de caracteres
                return Convert.ToBase64String(hashedPassword);
            }
        }

        //Ajout d'un utilisateur en base de données (Seulement pour créer un utilisateur)
        /*public static void AddUser(string emailEnter, string passwordEnter)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string insertUser = @"INSERT INTO Users (email, password) VALUES (@email, @password);";

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = insertUser;
                    command.Parameters.AddWithValue("@email", emailEnter);
                    command.Parameters.AddWithValue("@password", passwordEnter);
                    command.ExecuteNonQuery();
                }
            }
        }*/

        //Vérification des logins pour permettre la connexion
        public static void VerifyPassword(string emailEnter, string passwordEnter, ref bool redirectCondition)
        {

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                //Récuperation des utilisateurs depuis la bdd
                string selectUserEmail = @"SELECT COUNT(*) FROM Users WHERE email = @email AND password = @password";

                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = selectUserEmail;
                    command.Parameters.AddWithValue("@email", emailEnter);
                    command.Parameters.AddWithValue("@password", passwordEnter);

                    //Vérifie si il y a bien un utilisateur avec ces données la
                    int userCount = Convert.ToInt32(command.ExecuteScalar());

                    if (userCount > 0)
                    {
                        redirectCondition = true;
                    }
                    else
                    {
                        MessageBox.Show("Utilisateur introuvable");
                    }
                }
            }
        }

        /*public static void SelectProducts()
        {
            List<Produit> products = new List<Produit>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectRequest = @"SELECT quantite, prix, nom, categorieID, emplacement FROM Produits;";
                SQLiteCommand command = new SQLiteCommand(selectRequest, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        object[] values = new object[reader.FieldCount];
                        int columnCount = reader.GetValues(values); // `GetValues` returns the number of populated columns

                        // Now, map `values` to a `Produit` instance
                        Produit product = new Produit
                        {
                            quantite = Convert.ToInt32(values[0]),
                            prix = Convert.ToInt32(values[1]),
                            nom = values[2].ToString(),
                            categorieID = Convert.ToInt32(values[3]),
                            emplacement = Convert.ToInt32(values[4])
                        };

                        products.Add(product);
                    }
                }
            }

            static void GenerateTable(Table ProductTable, List<Produit> productList)
            {
                // Supprime les lignes existantes
                ProductTable.RowGroups.Clear();

                // Crée un nouveau groupe de lignes pour le tableau
                TableRowGroup groupeLignes = new TableRowGroup();

                foreach (var element in productList)
                {
                    // Crée une nouvelle ligne
                    TableRow ligne = new TableRow();

                    // Crée une cellule avec le texte de l'élément
                    TableCell cellule = new TableCell();
                    Paragraph paragraphe = new Paragraph(new Run(element));
                    cellule.Blocks.Add(paragraphe);

                    // Ajoute la cellule à la ligne
                    ligne.Cells.Add(cellule);

                    // Ajoute la ligne au groupe de lignes
                    groupeLignes.Rows.Add(ligne);
                }

                // Ajoute le groupe de lignes au tableau
                ProductTable.RowGroups.Add(groupeLignes);
            }*/
    }
}