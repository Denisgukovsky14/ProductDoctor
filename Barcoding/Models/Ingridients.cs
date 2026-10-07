using SQLite;

namespace Barcoding.Models
{
    public class Ingridients
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name {  get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        

    }
}
