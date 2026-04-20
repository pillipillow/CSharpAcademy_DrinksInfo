
namespace DrinksInfo
{
    internal class UserInput
    {
        DrinkService drinkService = new DrinkService();

        internal async Task GetCategoriesInput()
        {
            Console.Clear();
            Console.WriteLine("---Welcome to Drinks Info app---");

            await drinkService.GetCategories();

            Console.WriteLine("Choose a category by typing their name: ");
            Console.ReadLine();
        }
    }
}
