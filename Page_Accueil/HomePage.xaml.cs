using App_Grosiste.Scripts;
using System.Windows;
using System.Windows.Controls;
using static App_Grosiste.Scripts.db_context;
using Microsoft.Web.WebView2.Core;

namespace App_Grosiste
{
    /// <summary>
    /// Logique d'interaction pour HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private readonly db_context _context;

        public HomePage()
        {
            InitializeComponent();
            _context = new db_context();
            LoadProduits();
            InitializeWebView();
        }

        //Assignation des produits a des variables pour l'affichage en XAML
        public void LoadProduits()
        {
            List<Produit> Products = ConvertDataToList();
            Produit bestProductSell = Products.FirstOrDefault();
            BestSeller.Text = $"Meilleur Produit: {bestProductSell.nom}";
            TotalProduct.Text = $"Total: {Products.Count} produits";
        }

        //Récupération des produits depuis la bdd + convertion en liste
        public List<Produit> ConvertDataToList()
        {
            return _context.Produits.Select(p => new Produit
                {
                    id = p.id,
                    quantite = p.quantite,
                    prix = p.prix,
                    nom = p.nom,
                    categorieID = p.categorieID,
                    emplacement = p.emplacement,
                }).ToList();
        }

        //Convertion des produits en format JSON pour etre lisible en JS
        private string ConvertProductsToGoogleChartData(List<Produit> products)
        {
            var chartData = new List<string> { "['Produits', 'Quantitée']" }; // Entête du tableau
            foreach (var product in products)
            {
                chartData.Add($"['{product.nom}', {product.quantite}]"); //Données du tableau
            }
            return string.Join(", ", chartData); // Combine les lignes en une chaîne pour générer un tableau lisible en JS
        }

        /* ****************** Diagramme Google Charts ********************* */
        private async void InitializeWebView()
        {
            try
            {
                // Initialisation asynchrone de WebView2
                await webView2.EnsureCoreWebView2Async(null);

                // Récupère les produits et les convertit en données JSON pour le diagramme
                List<Produit> products = ConvertDataToList();
                string googleChartData = ConvertProductsToGoogleChartData(products);

                // Code HTML Permettant l'affichage du diagramme
                string html = $@"
        <!DOCTYPE html>
        <html>
        <head>
            <title>Diagramme stats</title>
            <script type='text/javascript' src='https://www.gstatic.com/charts/loader.js'></script>
            <script>
                google.charts.load('current', {{packages: ['corechart']}});
                google.charts.setOnLoadCallback(drawChart);
                function drawChart() {{
                    var data = google.visualization.arrayToDataTable([
                        {googleChartData} // Données insérées ici
                    ]);
                    var options = {{ 
                        title: 'Statistiques produits', 
                        curveType: 'function', 
                        legend: {{ position: 'bottom' }}
                    }};
                    var chart = new google.visualization.LineChart(document.getElementById('chart_div'));
                    chart.draw(data, options);
                }}
            </script>
        </head>
        <body>
            <div id='chart_div' style='width: 100%; height: 500px;'></div>
        </body>
        </html>";

                // Vérifie que WebView2 est bien initialisé
                if (webView2.CoreWebView2 != null)
                {
                    // Charge le contenu HTML sur le thread principal (UI thread)
                    Dispatcher.Invoke(() =>
                    {
                        webView2.CoreWebView2.NavigateToString(html);
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'initialisation de WebView2 : {ex.Message}");
            }
        }
        /* **************** Fin Diagramme Google Charts ******************* */
    }
}
