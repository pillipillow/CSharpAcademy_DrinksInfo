using DrinksInfo.Models;
using Newtonsoft.Json;
using System.Web;

namespace DrinksInfo
{
    internal class DrinkService
    {
        private static readonly HttpClient client = new HttpClient
        { 
            BaseAddress = new Uri("http://www.thecocktaildb.com/api/json/v1/1/")
        };

        internal async Task<List<Category>> GetCategories()
        {
            List<Category> categories = new List<Category>();

            try
            {
                var response = await client.GetAsync("list.php?c=list");

                response.EnsureSuccessStatusCode();

                var rawResponse = await response.Content.ReadAsStringAsync();

                var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);
                categories = serialize.categoriesList;
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"An unexpected error occured: {ex.Message}");
            }

            return categories; 
        }

        internal async Task<List<Drink>> GetDrinksByCategory(string category)
        {
            List<Drink> drinks = new List<Drink>();

            try
            {
                var response = await client.GetAsync($"filter.php?c={HttpUtility.UrlEncode(category)}");

                response.EnsureSuccessStatusCode();

                var rawResponse = await response.Content.ReadAsStringAsync();

                var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);
                drinks = serialize.drinksList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occured: {ex.Message}");
            }

            return drinks;

        }
    }
}
