namespace Market.Models
{
    public class Flat : Product
    {
        public int Floor { get; set; }
        public int RoomsCount { get; set; }
        public bool IsBuiltInFurniture { get; set; }
        public double Area { get; set; }
        public string Address { get; set; }
        public bool IsRepairedCurrentYear { get; set; }

        public override string ToString()
        {
            var baseResult = base.ToString();
            var IsBuiltInFurnitureAsText = (IsBuiltInFurniture) ? "встроена" : "нет";
            var IsRepairedCurrentYearAsText = (IsRepairedCurrentYear) ? "в этом году" : "больше года назад";
            return $"Адрес: {Address}\nПлощадь: {Area}\nЭтаж: {Floor}\nКоличество комнат: {RoomsCount}\nМебель: {IsBuiltInFurnitureAsText}\nПоследний ремонт: {IsRepairedCurrentYearAsText}\n{baseResult}";
        }
    }
}
