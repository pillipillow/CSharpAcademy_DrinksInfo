using DrinksInfo.Models;

namespace DrinksInfo
{
    internal class UserInterface
    {
        DrinkService drinkService = new DrinkService();

        internal async Task GetCategoriesInput()
        {
            Console.Clear();
            Console.WriteLine("---Welcome to Drinks Info app---");

            var categories = await drinkService.GetCategories();
            if (categories.Count <= 0)
            {
                Console.WriteLine("No categories found.");
            }
            else
            {
                foreach (var c in categories)
                {
                    Console.WriteLine($"- {c.strCategory}");
                }

                Console.WriteLine("Choose a category by typing their name: ");
                string category = Console.ReadLine();

                while (!Validator.IsStringValid(category))
                {
                    Console.WriteLine("\nInvalid category");
                    category = Console.ReadLine();
                }

                if (!categories.Any(x => x.strCategory == category))
                {
                    Console.WriteLine("Category doesn't exists.");
                    Console.ReadLine();
                    await GetCategoriesInput();
                }

                await GetDrinksInput(category);
            }
        }

        internal async Task GetDrinksInput(string category)
        {
            Console.Clear();
            Console.WriteLine($"---{category.ToUpper()}---");

            var drinks = await drinkService.GetDrinksByCategory(category);
            if (drinks.Count <= 0)
            {
                Console.WriteLine("No drinks found");
            }
            else
            {
                foreach (var d in drinks)
                {
                    Console.WriteLine($"{d.idDrink}\t{d.strDrink}");
                }

                Console.WriteLine("Choose a drink by typing their id: ");
                string drink = Console.ReadLine();

                if (drink == "0") await GetCategoriesInput();

                while (!Validator.IsIdValid(drink))
                {
                    Console.WriteLine("\nInvalid drink");
                    drink = Console.ReadLine();
                }

                if (!drinks.Any(x => x.idDrink == drink))
                {
                    Console.WriteLine("Drink doesn't exists");
                    Console.ReadLine();
                    await GetDrinksInput(category);
                }

                await GetDrinkDetailInput(drink);
            }
        }

        internal async Task GetDrinkDetailInput(string drink)
        {
            Console.Clear();
            var drinkList = await drinkService.GetDrink(drink);
            if (drinkList.Count <= 0)
            {
                Console.WriteLine("No drink found");
            }
            else
            {
                DrinkDetail drinkDetail = drinkList[0];

                foreach (var detail in drinkDetail.GetDrinkDetails())
                { 
                    Console.WriteLine(detail.Key + ": " + detail.Value);
                }


                Console.WriteLine("\nPress Enter to return back to the categories menu");
                Console.ReadLine();
                await GetCategoriesInput();
            }

        }

    }
}
