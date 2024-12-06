using App_Grosiste.Scripts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Logique d'interaction pour ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private readonly db_context _context;

        public class FinalProduct
        {
            public required int id { get; set; }
            public required int quantite { get; set; }
            public required int prix { get; set; }
            public required string nom { get; set; }
            public required string categorieNom { get; set; }
            public required int emplacement { get; set; }
        }

        public ProductPage()
        {
            InitializeComponent();
            _context = new db_context();
            LoadProducts();
        }

        //Récupération de la liste de produits
        public void LoadProducts()
        {
            List<FinalProduct> Products = ConvertProductToList();
            ProductList.ItemsSource = Products;
        }

        //Récupération de la liste de categories
        public void LoadCategories()
        {
            List<Categorie> Categories = ConvertCategorieToList();
            ProductList.ItemsSource = Categories;
        }

        public void AddCategoriesToProducts()
        {
            var Products = ConvertProductToList();
            LoadProducts();//Récupérer la liste des produits
            foreach (FinalProduct product in Products)
            {
                //Récupérer le categorieID et le comparer a la table categorie
            }
        }

        //Convertion des données de la bdd en liste -- *PRODUITS*
        public List<FinalProduct> ConvertProductToList()
        {
            var query = from Produit in _context.Produits
                         join Categorie in _context.Categories
                         on Produit.categorieID equals Categorie.id
                         select new
                         {
                             ProduitId = Produit.id,
                             ProduitQte = Produit.quantite,
                             ProduitPrix = Produit.prix,
                             ProduitNom = Produit.nom,
                             CategorieNom = Categorie.nom,
                             ProduitEmplacement = Produit.emplacement,
                         };

            var result = query.AsEnumerable()
                .Select(x=> new FinalProduct
                {
                    id = x.ProduitId,
                    quantite = x.ProduitQte,
                    prix = x.ProduitPrix,
                    nom = x.ProduitNom,
                    categorieNom = x.CategorieNom,
                    emplacement = x.ProduitEmplacement
                }).ToList();


            return result;
        }

        //Convertion des données de la bdd en liste -- *CATEGORIES*
        public List<Categorie> ConvertCategorieToList()
        {
            return _context.Categories.Select(c => new Categorie
            {
                id = c.id,
                nom = c.nom,
            }).ToList();
        }
    }
}
