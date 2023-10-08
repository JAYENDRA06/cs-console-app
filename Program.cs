using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Extensions.Configuration;

namespace HelloWorld
{
    internal class Program
    {
        static void Maiswn(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            DataContextEF entityFramework = new(config);

            DateTime rightNow = DateTime.Now;
            Computer computer = new()
            {
                Motherboard = "INTEL i5",
                CPUCores = 8,
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = rightNow,
                Price = 18.9000m,
                VideoCard = "RTX 3060",
            };
            entityFramework.Add(computer);
            entityFramework.SaveChanges();


            IEnumerable<Computer>? result = entityFramework.Computer?.ToList<Computer>();
            if (result != null)
            {
                foreach (Computer comp in result)
                {
                    Console.WriteLine("'" +
                        comp.ComputerId + "','" +
                        comp.Motherboard + "','" +
                        comp.CPUCores + "','" +
                        comp.HasWifi + "','" +
                        comp.HasLTE + "','" +
                        comp.ReleaseDate + "','" +
                        comp.Price + "','" +
                        comp.VideoCard +
                    "')");
                }
            }
        }
    }
}