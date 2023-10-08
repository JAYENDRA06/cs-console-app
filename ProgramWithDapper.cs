using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Extensions.Configuration;

namespace HelloWorld
{
    public class ProgramWithDapper
    {
        static void PrevLecture(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            DataContextDapper dapper = new(config);

            string sqlCommand = @"SELECT GETDATE()";
            DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlCommand);
            Console.WriteLine(rightNow);


            Computer computer = new()
            {
                Motherboard = "INTEL i5",
                CPUCores = 8,
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = rightNow,
                Price = 18.00m,
                VideoCard = "RTX 3060",
            };


            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                CPUCores,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES ('" +
                computer.Motherboard + "','" +
                computer.CPUCores + "','" +
                computer.HasWifi + "','" +
                computer.HasLTE + "','" +
                computer.ReleaseDate + "','" +
                computer.Price + "','" +
                computer.VideoCard +
            "')";
            int affectedRows = dapper.ExecuteSqlInt(sql);
            Console.WriteLine(affectedRows);



            string selectQuery = @"SELECT 
                Computer.Motherboard,
                Computer.CPUCores,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
            FROM TutorialAppSchema.Computer";
            IEnumerable<Computer> result = dapper.LoadData<Computer>(selectQuery);
            foreach (Computer comp in result)
            {
                Console.WriteLine("'" +
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