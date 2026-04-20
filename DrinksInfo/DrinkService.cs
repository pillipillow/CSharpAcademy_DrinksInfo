using DrinksInfo.Models;
using Newtonsoft.Json;

namespace DrinksInfo
{
    internal class DrinkService
    {
        private static readonly HttpClient client = new HttpClient
        { 
            BaseAddress = new Uri("http://www.thecocktaildb.com/api/json/v1/1/")
        };

        internal async Task GetCategories()
        {
            var response = await client.GetAsync("list.php?c=list");

            response.EnsureSuccessStatusCode();

            var rawResponse = await response.Content.ReadAsStringAsync();

            var serialize = JsonConvert.DeserializeObject<Categories>(rawResponse);
            List<Category> categories = serialize.categoriesList;

            foreach (var c in categories)
            { 
                Console.WriteLine($"- {c.strCategory}");
            }
        }
    }
}
