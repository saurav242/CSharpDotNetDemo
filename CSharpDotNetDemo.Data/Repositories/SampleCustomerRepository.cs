using Bogus;
using CSharpDotNetDemo.Data.Models;
using System;
using System.Collections.Generic;
using Bogus.DataSets;
using Bogus.Extensions;
using System.Text;
using System.Linq;

namespace CSharpDotNetDemo.Data.Repositories
{
    public class SampleCustomerRepository
    {

        public IEnumerable<Customer> GetCustomers(int customerCount = 100, int orderMaxCount = 10, int orderItemsMaxCount = 10, string localeCode = "en_IND")
        {

            Randomizer.Seed = new Random(123456);

            var FruitsAndVegetables = new[] { "Apple", "Apricot", "Artichoke", "Asian Pear", "Asparagus", "Atemoya", "Avocado", "Bamboo Shoots", "Banana", "Bean Sprouts", "Beans", "Beets", "Belgian Endive", "Bell Peppers", "Bitter Melon", "Blackberries", "Blueberries", "Bok Choy", "Boniato", "Boysenberries", "Broccoflower", "Broccoli", "Brussels Sprouts", "Cabbage", "Cactus Pear", "Cantaloupe", "Carambola", "Carrots", "Casaba Melon", "Cauliflower", "Celery", "Chayote", "Cherimoya", "Cherries", "Coconuts", "Collard Greens", "Corn", "Cranberries", "Cucumber", "Dates", "Dried Plums", "Eggplant", "Endive", "Escarole", "Feijoa", "Fennel", "Figs", "Garlic", "Gooseberries", "Grapefruit", "Grapes", "Green Beans", "Green Onions", "Greens", "Guava", "Hominy", "Honeydew Melon", "Horned Melon", "Iceberg Lettuce", "Jerusalem Artichoke", "Jicama", "Kale", "Kiwifruit", "Kohlrabi", "Kumquat", "Leeks", "Lemons", "Lettuce", "Lima Beans", "Limes", "Longan", "Loquat", "Lychee", "Madarins", "Malanga", "Mandarin Oranges", "Mangos", "Mulberries", "Mushrooms", "Napa", "Nectarines", "Okra", "Onion", "Oranges", "Papayas", "Parsnip", "Passion Fruit", "Peaches", "Pears", "Peas", "Peppers", "Persimmons", "Pineapple", "Plantains", "Plums", "Pomegranate", "Potatoes", "Prickly Pear", "Prunes", "Pummelo", "Pumpkin", "Quince", "Radicchio", "Radishes", "Raisins", "Raspberries", "Red Cabbage", "Rhubarb", "Romaine Lettuce", "Rutabaga", "Shallots", "Snow Peas", "Spinach", "Sprouts", "Squash", "Strawberries", "String Beans", "Sweet Potato", "Tangelo", "Tangerines", "Tomatillo", "Tomato", "Turnip", "Water Chestnuts", "Watercress", "Watermelon", "Waxed Beans", "Yams", "Yellow Squash", "Yuca/Cassava", "Zucchini Squash" };
            var orderNumber = new Random().Next(100000, 900000);

            var orderItemGenerator = new Faker<OrderItem>(localeCode)
                .RuleFor(o => o.Name, f => f.PickRandom(FruitsAndVegetables))
                .RuleFor(o => o.Price, f => Math.Round(f.Random.Decimal(1.0m, 200.0m), 2))
                //A nullable int? with 80% probability of being null.
                //The .OrNull extension is in the Bogus.Extensions namespace.
                .RuleFor(o => o.LotNumber, f => f.Random.Int(0, 100).OrNull(f, .8f))
                .RuleFor(o => o.Quantity, f => f.Random.Number(1, 20))
                .RuleFor(o => o.ManufacturingDate, f => f.Date.Recent(f.Random.Number(10)))
                .RuleFor(o => o.BestBeforeDate, f => f.Date.Soon(f.Random.Number(10)));

            var orderGenerator = new Faker<Order>(localeCode)
                .RuleFor(o => o.Id, Guid.NewGuid)
                .RuleFor(o => o.OrderNumber, f => orderNumber++)
                .RuleFor(o => o.OrderDate, f => f.Date.Recent(f.Random.Number(10)))
                .RuleFor(o => o.Shipped, f => f.Random.Bool(0.6f))
                .RuleFor(o => o.ShipDate, f => f.Date.Soon(f.Random.Number(10)))
                //.RuleFor(o => o.Delivered, (f, o) => o.Shipped ? f.Random.Bool() : false)
                //.RuleFor(o => o.DeliveryDate, (f, o) => o.Delivered ? o.ShipDate.AddDays(f.Random.Number(7)) : null)
                .RuleFor(o => o.OrderItems, f => orderItemGenerator.Generate(f.Random.Number(orderItemsMaxCount)))
                .RuleFor(o => o.OrderValue, (f, o) => o.OrderItems.Sum(x => x.Price * x.Quantity));

            var customerGenerator = new Faker<Customer>(localeCode)
                //Ensure all properties have rules. By default, StrictMode is false
                //Set a global policy by using Faker.DefaultStrictMode if you prefer.
                .StrictMode(true)
                .RuleFor(c => c.Id, Guid.NewGuid())
                .RuleFor(c => c.FirstName, (f, c) => f.Name.FirstName((Name.Gender)c.Gender))
                .RuleFor(c => c.LastName, (f, c) => f.Name.LastName((Name.Gender)c.Gender))
                .RuleFor(c => c.Avatar, f => f.Internet.Avatar())
                .RuleFor(c => c.UserName, (f, c) => f.Internet.UserName(c.FirstName, c.LastName))
                .RuleFor(c => c.Gender, f => f.PickRandom<Gender>())
                .RuleFor(c => c.Address, f => f.Address.FullAddress())
                .RuleFor(c => c.City, f => f.Address.City())
                .RuleFor(c => c.Country, f => f.Address.Country())
                .RuleFor(c => c.ZipCode, f => f.Address.ZipCode())
                .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
                .RuleFor(c => c.RegistrationDate, f => f.Date.Between(DateTime.Now.AddYears(-3), DateTime.Now.AddMinutes(-1)))
                .RuleFor(c => c.IsActive, f => f.Finance.Random.Bool(0.9f))
                .RuleFor(c => c.Level, f => f.Random.Number(1, 5))
                .RuleFor(c => c.Orders, f => orderGenerator.Generate(f.Random.Number(orderMaxCount)));

            return customerGenerator.Generate(customerCount);
        }
    }
}
