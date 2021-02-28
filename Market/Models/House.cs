namespace Market.Models
{
    public class House : Product
    {
        public int DistanceToTheСity { get; set; }
        public string WallsMaterial { get; set; }
        public int FloorsCount { get; set; }
        public string Address { get; set; }
        public double Area { get; set; }
        public bool IsInfrastructure { get; set; }
        public bool IsGarage { get; set; }
        public bool IsWarehouse { get; set; }

        public override string ToString()
        {
            var baseResult = base.ToString();
            var IsInfrastructureAsText = (IsInfrastructure) ? "есть" : "нет";
            var IsGarageAsText = (IsGarage) ? "есть" : "нет";
            var IsWarehouseAsText = (IsWarehouse) ? "есть" : "нет";
            return $"Адрес: {Address}\nПлощадь: {Area}\nКоличество этажей: {FloorsCount}\nРасстояние до города: {DistanceToTheСity}\nМатериал стен: {WallsMaterial}\nИнфраструктура: {IsInfrastructureAsText}\nГараж: {IsGarageAsText}\nСклад: {IsWarehouseAsText}\n{baseResult}";
        }
    }
}
