using System.Text.Json;
using AutoMapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Extensions.Configuration;

namespace HelloWorld
{
    internal class ProgramWithDapperAutoMapperJSON
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            DataContextDapper dapper = new(config);

            string computersJSON = File.ReadAllText("ComputersSnake.json");




            // using AutoMapper
            // alternatively we can set JsonPropertyName in model of Computer to directly specify casing changes
            // but with that we cannot update values like here for price
            Mapper mapper = new(new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<ComputerSnake, Computer>()
                .ForMember(destination => destination.ComputerId, options => options.MapFrom(source => source.computer_id))
                .ForMember(destination => destination.Motherboard, options => options.MapFrom(source => source.motherboard))
                .ForMember(destination => destination.CPUCores, options => options.MapFrom(source => source.cpu_cores))
                .ForMember(destination => destination.HasWifi, options => options.MapFrom(source => source.has_wifi))
                .ForMember(destination => destination.HasLTE, options => options.MapFrom(source => source.has_lte))
                .ForMember(destination => destination.ReleaseDate, options => options.MapFrom(source => source.release_date))
                .ForMember(destination => destination.Price, options => options.MapFrom(source => source.price * .8m))
                .ForMember(destination => destination.VideoCard, options => options.MapFrom(source => source.video_card));
            }));


            IEnumerable<ComputerSnake>? computers = JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersJSON);

            if (computers != null)
            {
                IEnumerable<Computer> computerRes = mapper.Map<IEnumerable<Computer>>(computers);
                foreach (Computer computer in computerRes)
                {
                    Console.WriteLine(computer.ComputerId);
                }
            }

            


            // using JsonPropertyName
            IEnumerable<ComputerWithProperty>? computers2 = JsonSerializer.Deserialize<IEnumerable<ComputerWithProperty>>(computersJSON);
            if (computers2 != null)
            {
                foreach (ComputerWithProperty computer in computers2)
                {
                    Console.WriteLine(computer.ComputerId);
                }
            }

        }
    }
}