namespace Market.Models
{
    public class Flat : Realty
    {
        public int Floor { get; set; }
        public bool IsBuiltInFurniture { get; set; }
        public bool IsConnectedCentralHeating { get; set; }
        public bool IsParkingPlace { get; set; }
        public bool IsBalcony { get; set; }
        public int SanitaryUnitsCount { get; set; }
        public int YearOfRepair { get; set; }

        public override string ToString()
        {
            var baseResult = base.ToString();
            var isBuiltInFurnitureAsText = (IsBuiltInFurniture) ? "есть" : "нет";
            var isConnectedCentralHeatingAsText = (IsConnectedCentralHeating) ? "подключена" : "не подключена";
            var isParkingPlaceAsText = (IsParkingPlace) ? "есть" : "нет";
            var isBalconyAsText = (IsBalcony) ? "есть" : "нет";
            return $"{baseResult}\nЭтаж: {Floor}\nНаличие мебели: {isBuiltInFurnitureAsText}\nПодключение к центральному отоплению: {isConnectedCentralHeatingAsText}\nНаличие парковочного места: {isParkingPlaceAsText}\nНаличие балкона: {isBalconyAsText}\nКоличество санузлов: {SanitaryUnitsCount}\nГод последнего ремонта: {YearOfRepair}";
        }
    }
}
