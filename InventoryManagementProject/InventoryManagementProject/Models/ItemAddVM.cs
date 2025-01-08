namespace InventoryManagementProject.Models
{
    public class ItemAddVM
    {
        public Item Item { get; set; } = new Item();

        public List<string> Categories { get; set; } = new List<string>();
    }
}
