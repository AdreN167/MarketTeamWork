using Market.Data;
using Market.Models;
using Market.Services;
using System;
using System.Collections.Generic;

namespace Market
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isEnd = false;

            ConfigurationService.Init();

            var flatDataAccess = new FlatDataAccess();
            var houseDataAccess = new HouseDataAccess();
            var productDataAccess = new ProductDataAccess();    

            while (!isEnd)
            {
                Console.WriteLine("1. Показать весь список товаров");
                Console.WriteLine("2. Выбрать категорию и отобразить список товаров");
                Console.WriteLine("3. Выход");

                Console.WriteLine("Ваш выбор: ");
                if (int.TryParse(Console.ReadLine(), out int choice)) { }

                switch (choice)
                {
                    case 1:
                        bool isFinish = false;
                        int index = 1;

                        ICollection<House> products = null;

                        while (!isFinish)
                        {
                            //Console.Clear();
                            Console.WriteLine("1. Следующая страница");
                            Console.WriteLine("2. Выбрать товар");
                            Console.WriteLine("3. Выход");
                            Console.WriteLine("Ваш выбор:");
                            if (!int.TryParse(Console.ReadLine(), out choice)) { }
                            
                            switch (choice)
                            {
                                case 1:
                                    index = 1;
                                    products = houseDataAccess.Select();
                                    foreach (var product in products)
                                    {
                                        Console.WriteLine($"-------------{index++}-------------");
                                        Console.WriteLine($"Цена: {product.Price}");
                                        Console.WriteLine($"Оценка: {product.Mark}");
                                    }
                                    Console.WriteLine("----------------------------");
                                    Console.ReadLine();
                                    
                                    break;
                                case 2:
                                    index = 1;
                                    Console.WriteLine("Введите номер товара:");
                                    int findIndex = int.Parse(Console.ReadLine());
                                    
                                    foreach (var product in products)
                                    {
                                        if (findIndex != index)
                                            index++;
                                        else
                                        {
                                            Console.WriteLine(product.ToString());
                                            break;
                                        }
                                    }
                                    Console.ReadLine();
                                    break;
                                case 3:
                                    isFinish = true;
                                    break;
                                default:
                                    Console.WriteLine("Такого пункта нет!");
                                    break;
                            }
                        }

                        break;

                    case 2:
                        Console.WriteLine("Выберите тип товара (1-дома, 2-квартиры):");
                        break;

                    case 3:
                        isEnd = true;
                        break;

                    default:
                        Console.WriteLine("Такого пункта нет!");
                        break;
                }
            }

            
            
        }
    }
}
