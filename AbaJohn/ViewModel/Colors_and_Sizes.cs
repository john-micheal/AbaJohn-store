namespace AbaJohn.ViewModel
{
    public class Colors_and_Sizes
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public static List<Colors_and_Sizes> getSizes()
        {
            List<Colors_and_Sizes> Size = new List<Colors_and_Sizes>();
            Size.Add(new Colors_and_Sizes() { ID=1, Name="XS",  Value=" X Small"});
            Size.Add(new Colors_and_Sizes() { ID=1, Name="S",   Value=" Small"});
            Size.Add(new Colors_and_Sizes() { ID=1, Name="M",   Value= "  Medium" });
            Size.Add(new Colors_and_Sizes() { ID=1, Name="XL",  Value=" X Large"});
            Size.Add(new Colors_and_Sizes() { ID=1, Name="2XL", Value= "2X Large" });
            Size.Add(new Colors_and_Sizes() { ID=1, Name="3XL", Value= "3X Large" });
            Size.Add(new Colors_and_Sizes() { ID=1, Name="4XL", Value= "4X Large" });
            Size.Add(new Colors_and_Sizes() { ID=1, Name="5XL", Value= "5X Large" });

            return Size;

        }

        public static List<Colors_and_Sizes> getcolors()
        {
            List<Colors_and_Sizes> cols = new List<Colors_and_Sizes>();

            cols.Add(new Colors_and_Sizes { ID = 1, Name = "blue", Value = "#007bff" });
            cols.Add(new Colors_and_Sizes { ID = 2, Name = "Gray", Value = "#6c757d" });
            cols.Add(new Colors_and_Sizes { ID = 3, Name = "White", Value = " " });
            cols.Add(new Colors_and_Sizes { ID = 4, Name = "Mocha", Value = "#6f372d" });
            cols.Add(new Colors_and_Sizes { ID = 5, Name = "Black", Value = "#000000" });
            cols.Add(new Colors_and_Sizes { ID = 6, Name = "Gold", Value = "#f9d77e" });
            cols.Add(new Colors_and_Sizes { ID = 7, Name = "Silver", Value = "#bebdb6" });
            cols.Add(new Colors_and_Sizes { ID = 8, Name = "Dark red", Value = "#8B0000" });
            cols.Add(new Colors_and_Sizes { ID = 9, Name = "Brown", Value = "#35281e" });
            cols.Add(new Colors_and_Sizes { ID = 10, Name = "Bronze", Value = "#51412d" });
            cols.Add(new Colors_and_Sizes { ID = 11, Name = "Red", Value = "#dc3545" });
            cols.Add(new Colors_and_Sizes { ID = 12, Name = "Champagne", Value = "#eed9b6" });
            cols.Add(new Colors_and_Sizes { ID = 13, Name = "Darkgreen", Value = "#006400" });
            cols.Add(new Colors_and_Sizes { ID = 14, Name = "Lightgrey", Value = "#90ee90" });
            cols.Add(new Colors_and_Sizes { ID = 15, Name = "Beige", Value = "#d9b99b" });
            cols.Add(new Colors_and_Sizes { ID = 16, Name = "Darkblue", Value = "#00008b" });
            cols.Add(new Colors_and_Sizes { ID = 17, Name = "Green", Value = "#245336" });
            cols.Add(new Colors_and_Sizes { ID = 18, Name = "Eggplant", Value = "#483248" });
            cols.Add(new Colors_and_Sizes { ID = 12, Name = "Cyan", Value = "#00FFFF" });
            cols.Add(new Colors_and_Sizes { ID = 20, Name = "Yellow", Value = "#ffc107" });
            cols.Add(new Colors_and_Sizes { ID = 21, Name = "Petroleum", Value = "#005f6a" });
            cols.Add(new Colors_and_Sizes { ID = 22, Name = "Purple", Value = "#800080" });
            cols.Add(new Colors_and_Sizes { ID = 23, Name = "Olive", Value = "#808000" });
            cols.Add(new Colors_and_Sizes { ID = 24, Name = "Orange", Value = "#FFA500" });
            return cols;
        }
    }
}
