using System;
using Market.Data;
using Market.Models;
using Market.Services;
using System.Collections;
using System.Collections.Generic;

namespace Market.UI
{
    public class Menu
    {
        private FlatDataAccess _flatDataAccess;
        private HouseDataAccess _houseDataAccess;

        public Menu()
        {
            ConfigurationService.Init();

            _flatDataAccess = new FlatDataAccess();
            _houseDataAccess = new HouseDataAccess();
        }

        public void MainMenu()
        {
            var isEnd = false;

            while (!isEnd)
            {
                Console.Clear();

                ShowMenu("-------------Главное меню------------", "Зарегистрироваться", "Войти", "Закрыть");
                Console.Write("Ваш выбор: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    ThrowError("Некорректный ввод!");
                }

                switch (choice)
                {
                    case 1:
                        RegistrationMenu();
                        break;

                    case 2:
                        LogInMenu();
                        break;

                    case 3:
                        isEnd = true;
                        break;

                    default:
                        ThrowError("Такого пункта нет!");
                        break;
                }
            }
        }

        private void MarketMenu()
        {
            var isEnd = false;
            var page = 0;

            while (!isEnd)
            {
                Console.Clear();
                ShowMenu("-------------Меню магазина------------", "Выбрать категории", "Сменить аккаунт", "Закрыть");
                Console.Write("Ваш выбор: ");

                if (!int.TryParse(Console.ReadLine(), out int marketChoice))
                {
                    ThrowError("Некорректный ввод!");
                }

                switch (marketChoice)
                {
                    case 1:
                        Console.Clear();
                        int house = 1, flat = 2;

                        ShowMenu("-------------Тип------------", "Дома", "Квартиры");
                        Console.Write("Ваш выбор: ");

                        if (!int.TryParse(Console.ReadLine(), out int typeChoice) || typeChoice < house || typeChoice > flat)
                        {
                            ThrowError("Некорректный ввод!");
                            break;
                        }

                        ShowMenu("-------------Площадь------------");
                        Console.Write("Введите минимальную площадь: ");

                        if (!double.TryParse(Console.ReadLine(), out double areaChoice) || areaChoice <= 0)
                        {
                            ThrowError("Некорректный ввод!");
                            break;
                        }

                        ShowMenu("-------------Количество комнат------------");
                        Console.Write("Введите минимальное количество комнат: ");

                        if (!int.TryParse(Console.ReadLine(), out int roomsCountChoice) || roomsCountChoice < 1)
                        {
                            ThrowError("Некорректный ввод!");
                            break;
                        }

                        ShowMenu("-------------Цена------------");
                        Console.Write("Введите свой бюджет: ");

                        if (!int.TryParse(Console.ReadLine(), out int moneyChoice) || moneyChoice < 0)
                        {
                            ThrowError("Некорректный ввод!");
                            break;
                        }

                        ICollection products = null;

                        bool isChosen = false;
                        while (!isChosen)
                        {
                            Console.Clear();
                            if (typeChoice == house)
                            {
                                products = _houseDataAccess.SelectBy(areaChoice, moneyChoice, roomsCountChoice, page) as List<House>;
                            }

                            else if (typeChoice == flat)
                            {
                                products = _flatDataAccess.SelectBy(areaChoice, moneyChoice, roomsCountChoice, page) as List<Flat>;
                            }

                            ShowProducts(products);

                            ShowMenu("-------------------------", "Следующая страница", "Предыдущая страница", "Выбрать товар", "Назад");
                            Console.Write("Ваш выбор: ");

                            if (!int.TryParse(Console.ReadLine(), out int сhoice))
                            {
                                ThrowError("Некорректный ввод!");
                            }

                            switch (сhoice)
                            {
                                case 1:
                                    if (products.Count > 0)
                                        ++page;
                                    break;

                                case 2:
                                    if (page - 1 >= 0)
                                        --page;
                                    break;

                                case 3:
                                    Console.Write("Введите номер товара: ");
                                    if (!int.TryParse(Console.ReadLine(), out int productChoice) && (productChoice < 1 || productChoice > products.Count))
                                    {
                                        ThrowError("Некорректный ввод!");
                                        break;
                                    }

                                    Console.Clear();
                                    InfoMenu(productChoice, products);

                                    break;

                                case 4:
                                    isChosen = true;
                                    break;

                                default:
                                    ThrowError("Такого пункта нет!");
                                    break;
                            }
                        }

                        break;

                    case 2:
                        LogInMenu();
                        break;

                    case 3:
                        isEnd = true;
                        break;

                    default:
                        ThrowError("Такого пункта нет!");
                        break;
                }
            }
        }

        private void InfoMenu(int productChoice, ICollection products)
        {
            int count = 0;
            foreach (var product in products)
            {
                if (count == productChoice - 1)
                {
                    if (product is Flat flat)
                    {
                        Console.WriteLine(flat.ToString());
                        BuyMenu(flat);
                    }
                    else if (product is House house)
                    {
                        Console.WriteLine(house.ToString());
                        BuyMenu(house);
                    }
                }
                count++;
            }
        }

        private void BuyMenu(Realty house)
        {
            var isEnd = false;
            while (!isEnd)
            {
                // TODO
            }
        }

        private void RegistrationMenu()
        {
            // TODO
            MarketMenu();
        }

        private void LogInMenu()
        {
            // TODO
            MarketMenu();
        }

        private void ShowProducts(ICollection products)
        {
            foreach (var product in products)
            {
                int count = 1;
                if (product is Flat flat)
                {
                    Console.WriteLine($"~~Квартира[{count++}]~~");
                    Console.WriteLine($"Адрес: {flat.Address}");
                    Console.WriteLine($"Рейтинг: {flat.Mark}");
                    Console.WriteLine($"Цена: {flat.Price}\n");
                }
                else if (product is House house)
                {
                    Console.WriteLine($"~~Дом[{count++}]~~");
                    Console.WriteLine($"Адрес: {house.Address}");
                    Console.WriteLine($"Рейтинг: {house.Mark}");
                    Console.WriteLine($"Цена: {house.Price}\n");
                }
            }
        }

        private void ShowMenu(string head, params string[] arguments)
        {
            Console.WriteLine(head);
            for (int i = 0; i < arguments.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {arguments[i]}");
            }
        }

        private void ThrowError(string message)
        {
            Console.WriteLine($"\n{message}");
            Console.ReadLine();
        }
    }
}
