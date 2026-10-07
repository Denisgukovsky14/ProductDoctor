using SQLite;

namespace Barcoding.Models
{
    public class Compositions
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Barcode { get; set; }

        public string Title { get; set; }
        public string Manufacturer { get; set; }

        public int ProductRating { get; set; }

        public string Composition {  get; set; }

        public int ImageCode { get; set; }

        public string Proteins { get; set; }
        public string Fats { get; set; }
        public string Carbohydrates { get; set; }
        public string Kilocalories { get; set; }

        public string category { get; set; }

    }
}
