using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using InventoryManagementProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InventoryManagementProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly string _connectionString;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _connectionString = configuration.GetConnectionString("ConnectionStringSql");
        }

        static void WriteData(string _connectionString, string Naziv, int Kolicina, decimal Cijena)
        {
            using(MySqlConnection connection=new MySqlConnection(_connectionString))
            {
                try
                {
                    //everything connection that opens has to close!!!
                    connection.Open();
                    string query = "INSERT INTO Skladiste (Naziv, Kolicina, Cijena) VALUES (@Naziv, @Kolicina, @Cijena);";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Naziv", Naziv);
                        command.Parameters.AddWithValue("@Kolicina", Kolicina);
                        command.Parameters.AddWithValue("@Cijena", Cijena);
                        //runs everything in database
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
                //finally always executes!
                finally
                {
                    connection.Close();
                }
            }
        }

        static List<Item> ReadData(string _connectionString)
        {
            List<Item> items = new List<Item>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    //everything connection that opens has to close!!!
                    connection.Open();
                    string query = "SELECT * FROM Skladiste;";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader=command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Item item = new Item { 
                                    Name = reader["Naziv"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Kolicina"]),
                                    Price= Convert.ToDecimal(reader["Cijena"])
                                };
                                items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                //finally always executes!
                finally
                {
                    connection.Close();
                }
            }
            return items;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AddItem()
        {
            //showing types of categories to the user
            ItemAddVM vm = new ItemAddVM();
            vm.Categories = Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>().Select(c => c.ToString()).ToList();
            return View(vm);
        }

        //method to handle POST request,to ensure the form, or in this case, adding a new item is done properly
        [HttpPost]
        public IActionResult AddItem(ItemAddVM newItem)
        {
            if (ModelState.IsValid)
            {
                WriteData(_connectionString, newItem.Item.Name, newItem.Item.Quantity, newItem.Item.Price);
                //redirects user to show a list of items (optional)
                return RedirectToAction("Inventory");
            }
            else
            {
                return View(newItem);
            }
            //if form is done incorectly, it resets it
            ViewBag.Categories = Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>().ToList();
            return View(newItem);
        }
        public IActionResult Inventory()
        {
            List<Item> items = ReadData(_connectionString);
            return View(items);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
