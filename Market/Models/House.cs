namespace Market.Models
{
    public class House : Realty
    {
        public double DistanceToTheСity { get; set; }
        public string WallsMaterial { get; set; }
        public int FloorsCount { get; set; }
        public bool IsInfrastructure { get; set; }
        public bool IsCellar { get; set; }
        public bool IsBathhouse { get; set; }
        public double GardensArea { get; set; }

        public override string ToString()
        {
            var baseResult = base.ToString();
            var isInfrastructureAsText = (IsInfrastructure) ? "есть" : "нет";
            var isGarageAsText = (IsGarage) ? "есть" : "нет";
            var isWarehouseAsText = (IsWarehouse) ? "есть" : "нет";
            var isCellarAsText = (IsCellar) ? "есть" : "нет";
            var isBathhouseAsText = (IsBathhouse) ? "есть" : "нет";
            return $"{baseResult}\nКоличество этажей: {FloorsCount}\nРасстояние до города: {DistanceToTheСity}\nМатериал стен: {WallsMaterial}\nИнфраструктура: {isInfrastructureAsText}\nГараж: {isGarageAsText}\nСклад: {isWarehouseAsText}\nПодвал: {isCellarAsText}\nБаня: {isBathhouseAsText}\nПлощадь сада: {GardensArea} соток(и)";
        }
    }
}
