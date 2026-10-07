using SQLite;

namespace Barcoding.Models
{
    public class Rating
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Barcode { get; set; }
        public int ProductRating { get; set; }

        public string WhyThisMark { get; set; }
        public string WhyThisMark2 { get; set; }

        public int Image1Code { get; set; }
        public int Image2Code { get; set; }

        
        public string Category { get; set; }

        [Ignore]
        public string Image1Path => $"i{Image1Code}.jpg";

    }
}

