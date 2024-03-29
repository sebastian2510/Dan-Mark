﻿using static System.Console;
using System.Linq;
using System.Threading;

namespace FishTank
{
    class StockClass
    {
        
        public static List<FishClass> FishSortiment = new List<FishClass>(); // List of all fish in the aquarium
        public static List<TankClass> TankSortiment = new List<TankClass>(); // List of all fish tanks in the aquarium

        public void ViewAllFish()
        {
            Clear();

            WriteLine("Dan & Marks akvarium har følgende fisk i sortiment");
            WriteLine("**************************************************" + Environment.NewLine);

            int i = 0;
            foreach (FishClass fish in FishSortiment)
            {
                WriteLine($"Fisk nr. { i + 1 }");
                WriteLine($"Race: { fish.Name }");
                WriteLine($"Alder: {fish.GetAgeInfo(fish.Born)}");
                WriteLine($"Pris: { fish.Price } DKK");
                WriteLine($"Mad: { fish.FoodType }");
                WriteLine($"Vand: { fish.WaterType }");
                WriteLine($"Fodring: { fish.FoodCycle } timer");
                WriteLine($"------------------------");
                i++;
            }

            WriteLine(Environment.NewLine + "Ønsker du at:");
            WriteLine("1. Tilføje en fisk til sortiment");
            WriteLine("2. Fjerne en fisk fra sortiment");
            WriteLine("3. Vende tilbage til hovedmenuen");

            int menuChoice;
            do
            {
                Write("Vælg en mulighed fra menuen: ");
            } while (!Int32.TryParse(ReadLine(), out menuChoice) || menuChoice < 1 || menuChoice > 3);

            switch (menuChoice)
            {
                case 1:
                    AddFishToSortiment();
                    break;
                case 2:
                    RemoveFishFromSortiment();
                    break;
                case 3:
                    MenuClass menu = new MenuClass();
                    menu.MainMenu();
                    break;
            }
        }
        
        public void ViewAllFishTanks()
        {
            Clear();

            WriteLine("Dan & Marks akvarium har følgende fisketanke i sortiment");
            WriteLine("********************************************************" + Environment.NewLine);
            
            foreach (TankClass item in TankSortiment)
            {
                WriteLine($"ID: { item.ID }");
                WriteLine($"Størrelse: { item.TankSize } liter");
                WriteLine($"Mad: { item.FoodType }");
                WriteLine($"Vand: { item.WaterType }");
                WriteLine($"Rensecyklus: { item.CleaningCycle } timer");
                WriteLine($"Pris: {item.Price:N2} DKK");
                Write($"Fish types: ");

                if (item.FishInTank == null || item.FishInTank.Count < 1)
                    WriteLine("Der er ikke nogen fisk i denne tank");
                else
                {
                    int numFish = 1;
                    foreach (var fish in item.FishInTank)
                    {
                        if (numFish != item.FishInTank.Count)
                            Write(fish.Name + ", ");
                        else
                            Write(fish.Name);
                        numFish++;
                    }
                    WriteLine();
                }
                WriteLine($"------------------------");
            }

            WriteLine(Environment.NewLine + "Ønsker du at:");
            WriteLine("1. Tilføje et akvarium til sortiment");
            WriteLine("2. Fjerne et akvarium fra sortiment");
            WriteLine("3. Vende tilbage til hovedmenuen");

            int menuChoice;
            do
            {
                Write("Vælg en mulighed fra menuen: ");
            } while (!Int32.TryParse(ReadLine(), out menuChoice) || menuChoice < 1 || menuChoice > 3);

            switch (menuChoice)
            {
                case 1:
                    AddTankToSortiment();
                    break;
                case 2:
                    RemoveTankFromSortiment();
                    break;
                case 3:
                    MenuClass menu = new MenuClass();
                    menu.MainMenu();
                    break;
            }
        }

        public void RemoveFishFromSortiment()
        {
            if (FishSortiment.Count >= 0)
            {
                ClearCurrentConsoleLine();

                bool bfishFound = false;
                int fishIndex = -1;
                do
                {
                    Write("Indtast navn på den fisk, du ønsker at fjerne fra sortiment: ");
                    string fishName = ReadLine().ToLower();

                    foreach (FishClass fish in FishSortiment)
                    {
                        if (fish.Name.ToLower() == fishName)
                            fishIndex = FishSortiment.IndexOf(fish);
                    }

                    if (fishIndex != -1)
                        bfishFound = true;

                } while (!bfishFound);

                FishSortiment.RemoveAt(fishIndex);
                WriteLine("Den ønskede fisk er nu fjernet fra sortiment. Tryk en tast for at vende tilbage til oversigten...");
                ReadKey();
                ViewAllFish();
            }
            else
            {
                WriteLine("Ingen fisk i sortimentet...");
                Thread.Sleep(2000);
                return;
            }

        }

        public void RemoveTankFromSortiment()
        {
            if (TankSortiment.Count >= 0)
            {
                ClearCurrentConsoleLine();

                int i;
                do
                {
                    Write("Vælg det akvarium, du ønsker at fjerne: ");
                } while (!Int32.TryParse(ReadLine(), out i));

                TankSortiment.RemoveAt(i - 1);
                WriteLine("Det ønskede akvarie er nu fjernet fra sortiment. Tryk en tast for at vende tilbage til oversigten...");
                ReadKey();
                ViewAllFishTanks();
            }
            else
            {
                WriteLine("Ingen akvarier i sortimentet...");
                Thread.Sleep(2000);
                return;
            }

        }

        public void AddFishToSortiment()
        {
            Clear();
            WriteLine("Du kan nu tilføje en fisk til sortiment");
            WriteLine("***************************************" + Environment.NewLine);

            string fishName;
            do
            {
                Write("Hvad er navnet på fiskens type?");
                fishName = ReadLine();
            } while (string.IsNullOrEmpty(fishName));

            int foodChoice;
            do
            {
                WriteLine("1. Kød");
                WriteLine("2. Flager");
                Write("Spiser fisken kød eller flager: ");
            } while (!int.TryParse(ReadLine(), out foodChoice) || foodChoice < 1 || foodChoice > 2);


            Food foodInput = Food.Meat;
            if (foodChoice == 2)
                foodInput = Food.Flakes;

            int waterChoice;
            do
            {
                WriteLine("1. Ferskvand");
                WriteLine("2. Saltvand");
                Write("Hvilket vand lever fisken i: ");
            } while (!Int32.TryParse(ReadLine(), out waterChoice) || waterChoice < 1 || waterChoice > 2);

            Water waterType = Water.Freshwater;
            if (waterChoice == 2)
                waterType = Water.Saltwater;

            decimal fishPrice;
            do
            {
                Write("Hvad koster fisken: ");
            } while (!Decimal.TryParse(ReadLine(), out fishPrice));

            DateTime born;

            do
            {
                WriteLine("Hvornår er fisken født (dd-mm-yyyy)?");
            } while (!DateTime.TryParse(ReadLine(), out born) || born > DateTime.Now.AddMinutes(5));

            FishClass newFish = new FishClass(fishName, foodInput, waterType, fishPrice, born);
            
            FishSortiment.Add(newFish);

            WriteLine("Fisken er nu tilføjet til sortiment! Tryk en tast for at vende tilbage til menuen...");
            ReadKey();
            ViewAllFish();
        }

        public void AddTankToSortiment()
        {
            int tankSize;
            do
            {
                Console.Write("Størrelse på akvariet i liter: ");
            } while (!int.TryParse(Console.ReadLine(), out tankSize));

            int foodChoice;
            do
            {
                Write("1. Kød");
                Write("2. Flager");
                Write("Spiser fisken kød eller flager?");
            } while (!int.TryParse(ReadLine(), out foodChoice) || foodChoice < 1 || foodChoice > 2);


            Food foodInput = Food.Meat;
            if (foodChoice == 2)
                foodInput = Food.Flakes;

            int waterChoice;
            do
            {
                WriteLine("Hvilket vand lever fisken i? ");
                WriteLine("1. Ferskvand");
                WriteLine("2. Saltvand");
            } while (!Int32.TryParse(ReadLine(), out waterChoice) || waterChoice < 1 || waterChoice > 2);

            Water waterType = Water.Freshwater;
            if (waterChoice == 2)
                waterType = Water.Saltwater;


            TankClass newTank = new TankClass(tankSize, foodInput, waterType);
            TankSortiment.Add(newTank);

            WriteLine("Akvariet er nu tilføjet til sortiment! Tryk en tast for at vende tilbage til menuen...");
            ReadKey();
            ViewAllFishTanks();
        }

        public void AddTestFishToSortiment()
        {
            WriteLine("Tilføjer fisk...");
            Thread.Sleep(1000);
            FishClass Pirania = new FishClass("Pirania", Food.Meat, Water.Freshwater, 200.99m, DateTime.Now.AddDays(-3));
            FishSortiment.Add(Pirania);
            TankClass.AddFishToTank(Pirania);
            FishClass Herring = new FishClass("Herring", Food.Flakes, Water.Saltwater, 50.00m, DateTime.Now.AddDays(-1));
            FishSortiment.Add(Herring);
            TankClass.AddFishToTank(Herring);
            FishClass Trout = new FishClass("Trout", Food.Meat, Water.Saltwater, 21.99m, DateTime.Now.AddDays(-32));
            FishSortiment.Add(Trout);
            TankClass.AddFishToTank(Trout);
            FishClass Goldfish = new FishClass("Goldfish", Food.Flakes, Water.Freshwater, 9.99m, DateTime.Now.AddYears(-3));
            FishSortiment.Add(Goldfish);
            TankClass.AddFishToTank(Goldfish);
            FishClass Platy = new FishClass("Platy", Food.Flakes, Water.Freshwater, 19.99m, DateTime.Now.AddDays(-2));
            FishSortiment.Add(Platy);
            TankClass.AddFishToTank(Platy);
            FishClass Parasema = new FishClass("Parasema", Food.Flakes, Water.Saltwater, 159.00m, DateTime.Now.AddDays(-1));
            FishSortiment.Add(Parasema);
            TankClass.AddFishToTank(Parasema);
            FishClass ClownFish = new FishClass("ClownFish", Food.Flakes, Water.Saltwater, 229.00m, DateTime.Now.AddDays(-2));
            FishSortiment.Add(ClownFish);
            TankClass.AddFishToTank(ClownFish);
        }

        public void AddTestAquariumsToSortiment()
        {
            WriteLine("Tilføjer akvarier...");
            Thread.Sleep(1000);

            TankClass tank1 = new TankClass(200, Food.Meat, Water.Freshwater);
            TankSortiment.Add(tank1);
            TankClass tank2 = new TankClass(100, Food.Flakes, Water.Saltwater);
            TankSortiment.Add(tank2);
            TankClass tank3 = new TankClass(400, Food.Meat, Water.Saltwater);
            TankSortiment.Add(tank3);
            TankClass tank4 = new TankClass(1000, Food.Flakes, Water.Freshwater);
            TankSortiment.Add(tank4);
        }

        public static void ReturnToMainMenu()
        {
            MenuClass menu = new MenuClass();
            WriteLine(Environment.NewLine + "Tryk en tast for at vende tilbage til hovedmenuen...");
            ReadKey();
            menu.MainMenu();
        }

        public void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop; // Get the current line
            Console.SetCursorPosition(0, Console.CursorTop); // Set the cursor to the start of the line
            WriteLine(new string(' ', Console.WindowWidth)); // Write a new line with the same length as the window
            Console.SetCursorPosition(0, currentLineCursor); // Set the cursor to the start of the line
        }
    }
}
