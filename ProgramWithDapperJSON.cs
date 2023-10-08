using System.Text.Json;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HelloWorld
{
    internal class ProgramWithDapperJSON
    {
        static void SystemAndNewtonJSON(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            DataContextDapper dapper = new(config);


            string computersJSON = File.ReadAllText("Computers.json");



            // using System.text.Json
            // To match camel case is json and casing of Computer
            // Like motherboard and Motherboard
            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            IEnumerable<Computer>? computers = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJSON, options);





            // Using Newtonsoft.Json
            IEnumerable<Computer>? computersNewton = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJSON);

            if (computersNewton != null)
            {
                foreach (Computer computer in computersNewton)
                {
                    string sql = @"INSERT INTO TutorialAppSchema.Computer (
                    Motherboard,
                    CPUCores,
                    HasWifi,
                    HasLTE,
                    ReleaseDate,
                    Price,
                    VideoCard
                    ) VALUES ('" +
                        EscapeSingleQuote(computer.Motherboard) + "','" +
                        computer.CPUCores + "','" +
                        computer.HasWifi + "','" +
                        computer.HasLTE + "','" +
                        computer.ReleaseDate + "','" +
                        computer.Price + "','" +
                        EscapeSingleQuote(computer.VideoCard) +
                    "')";

                    dapper.ExecuteSqlInt(sql);
                }
            }

            // Don't need settings for deserialization but need for serialization
            JsonSerializerSettings settings = new()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string computersCopy = JsonConvert.SerializeObject(computers, settings);
        }
        
        // To solve single quote issue when running SQL
        static string EscapeSingleQuote(string input){
            return input.Replace("'", "''");
        }
    }
}