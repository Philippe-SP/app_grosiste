using System.Data.SQLite;
using System.IO;

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
                            nom TEXT NOT NULL,
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
            /*string insertClients1 = @"INSERT INTO Clients (nom, adresse, siret) VALUES ('EDF', '3 rue des alobroches', '41006906600042');";
            string insertClients2 = @"INSERT INTO Clients (nom, adresse, siret) VALUES ('SNCF', '5 rue Saint Paul', '41006906600042');";
            string insertClients3 = @"INSERT INTO Clients (nom, adresse, siret) VALUES ('Atos', '34 rue de la soie', '48397413500020');";*/

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
}


