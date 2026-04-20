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
            foreach (var c in categories)
            {
                Console.WriteLine($"- {c.strCategory}");
            }

            Console.WriteLine("Choose a category by typing their name: ");
            string category = Console.ReadLine();

            await GetDrinksInput(category);
        }

        internal async Task GetDrinksInput(string category)
        {
            Console.Clear();
            Console.WriteLine($"---{category}---");

            var drinks = await drinkService.GetDrinksByCategory(category);
            foreach (var d in drinks)
            {
                Console.WriteLine($"{d.idDrink}\t{d.strDrink}");
            }

            Console.WriteLine("Choose a drink by typing their id: ");
            string drink = Console.ReadLine();
        }
    }
}
