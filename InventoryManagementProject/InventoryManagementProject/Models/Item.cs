namespace InventoryManagementProject.Models
{
    public enum ItemCategory
    {
        Electronics,
        Furniture,
        Clothing
    }


    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public ItemCategory Category { get; set; }

        public Item()
        {
            this.Id = Id;
            this.Price = Price;
            this.Quantity = Quantity;
            this.Name = Name;
        }

    }

    public class Electronics : Item
    {
        public string Brand { get; set; }
        public string Model { get; set; }

    }

    public class Furniture : Item
    {
        public string Material { get; set; }
        public string Dimension { get; set; }

    }

    public class Clothing : Item
    {
        public string Material { get; set; }
        public string Size { get; set; }
    }

}
