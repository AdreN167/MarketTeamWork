namespace Market.Models
{
    public abstract class Product
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public double Mark { get; set; }
        public bool IsSold { get; set; }

        public override string ToString()
        {
            var isSoldAsText = (IsSold) ? "продано" : "продается";
            return $"Цена: {Price}\nОценка: {Mark}\nСтатус: {isSoldAsText}\n";
        }
    }
}
