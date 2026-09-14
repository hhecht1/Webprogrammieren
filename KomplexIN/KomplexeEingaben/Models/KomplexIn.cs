namespace KomplexeEingaben.Models
{
    public class KomplexIn
    {
        public string Name { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public decimal Number { get; set; } = 123.45m;

        // DateTime
        public DateTime BirthDate { get; set; }

        public DateTime Time { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;


        public bool IsSubscribed { get; set; }

        public int IsSubscribedInt { get; set; }

        public List<int> SelectedIds { get; set; } = new List<int>();

        public List<MultiCheck> CheckboxField { get; set; } = new List<MultiCheck>();

        public string SelectedIdsTH { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public List<int> SelectedCategories { get; set; } = new List<int>();

        public string RadioStatus { get; set; }

        public string FileInput { get; set; } = string.Empty;

        public int Range { get; set; }

        public string ColorSelected { get; set; } = string.Empty;

        public string ImageInput { get; set; } = @"/img/DSC_9129_sm.png";

        
    }

    public class MultiCheck
    {
        public int Wert { get; set; }
        public bool IsChecked { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
