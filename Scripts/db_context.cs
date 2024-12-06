using Microsoft.EntityFrameworkCore;

namespace App_Grosiste.Scripts
{
    public class db_context : Microsoft.EntityFrameworkCore.DbContext
    {
        public Microsoft.EntityFrameworkCore.DbSet<Produit> Produits { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Client> Clients { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Categorie> Categories { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=../../../libraryManage.db");
        }

        public class Produit
        {
            public int id { get; set; }
            public required int quantite { get; set; }
            public required int prix { get; set; }
            public required string nom { get; set; }
            public required int categorieID { get; set; }
            public required int emplacement { get; set; }
        }

        public class Client
        {
            public int id { get; set; }
            public required string nom { get; set; }
            public required string adresse { get; set; }
            public required string siret { get; set; }
        }

        public class Categorie
        {
            public required int id { get; set; }
            public required string nom { get; set; }
            public List<Produit> Produits { get; set; }
        }

        public class User
        {
            public required int id { get; set; }
            public required string email { get; set; }
            public required string password { get; set; }
        }
    }
}
