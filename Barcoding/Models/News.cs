using SQLite;

namespace Barcoding.Models
{
    public class News
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Source { get; set; }

        public string NewsName { get; set; }

        public string FullNews { get; set; }

        [Ignore]
        public string FormatedFullNews { get; set; }

        public int Image1Code { get; set; }
        public int Image2Code { get; set; }

        public DateTime Date { get; set; }

        [Ignore]
        public string FormatedDate { get; set; }


    }
}