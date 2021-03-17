namespace Market.Models
{
    public abstract class Realty : Product
    {
        public double Area { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public int RoomsCount { get; set; }
        public bool IsGarage { get; set; }
        public bool IsWarehouse { get; set; }

        public override string ToString()
        {
            var baseResult = base.ToString();
            var isGarageAsText = (IsGarage) ? "есть" : "нет";
            var isWarehouseAsText = (IsWarehouse) ? "есть" : "нет";
            return $"{baseResult}\nГород: {CityName}\nАдрес: {Address}\nПлощадь: {Area}\nКоличество комнат: {RoomsCount}\nНаличие гаража: {isGarageAsText}\nНаличие складского помещения: {isWarehouseAsText}";
        }
    }
}
