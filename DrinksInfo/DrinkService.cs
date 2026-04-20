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
            var response = await client.GetAsync("list.php?c=list");

            response.EnsureSuccessStatusCode();

            var rawResponse = await response.Content.ReadAsStringAsync();

            var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);
            List<Category> categories = serialize.categoriesList;

            return categories; 
        }

        internal async Task<List<Drink>> GetDrinksByCategory(string category)
        { 
            var response = await client.GetAsync($"filter.php?c={HttpUtility.UrlEncode(category)}");

            response.EnsureSuccessStatusCode();

            var rawResponse = await response.Content.ReadAsStringAsync();

            var serialize = JsonConvert.DeserializeObject<Drinks>(rawResponse);
            List<Drink> drinks = serialize.drinksList;

            return drinks;

        }
    }
}
