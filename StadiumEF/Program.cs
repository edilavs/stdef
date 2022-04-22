using StadiumEF.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StadiumEF
{
    class Program
    {
        static void Main(string[] args)
        {
            StadiumDbContext dbContext = new StadiumDbContext();

            bool check = true;
            string answer = "";
            string stadiumName = "";
            string hourPriceStr = "";
            decimal hourPrice;
            string capacityStr;
            int capacity;

            while (check)
            {
                Console.WriteLine("---MENU---");
                Console.WriteLine("1.Stadion elave et\n" +
                    "2.Stadionlari goster\n" +
                    "3.Verilmish id-li stadionu goster" +
                    "\n4.Verilmish id-li stadionu sil" +
                    "\n0.Proqrami bitir");
                answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        do
                        {
                            Console.WriteLine("Stadion adi daxil edin:");
                            stadiumName = Console.ReadLine();
                        } while (String.IsNullOrEmpty(stadiumName));
                        do
                        {
                            Console.WriteLine("Stadionun limitini daxil edin: ");
                            capacityStr = Console.ReadLine();
                        } while (!int.TryParse(capacityStr, out capacity));
                        do
                        {
                            Console.WriteLine("Stadionun saatliq qiymetini:");
                            hourPriceStr = Console.ReadLine();
                        } while (!decimal.TryParse(hourPriceStr, out hourPrice));

                        Stadium stadium = new Stadium
                        {
                            Name = stadiumName,
                            HourPrice = hourPrice,
                            Capacity = capacity,
                        };

                        dbContext.Stadiums.Add(stadium);
                        dbContext.SaveChanges();
                        break;

                    case "2":
                        List<Stadium> Stadiums = dbContext.Stadiums.ToList();
                        foreach (var std in Stadiums)
                        {
                            Console.WriteLine($"{std.Id}-{std.Name}-{std.HourPrice}-{std.Capacity}");
                        }
                        break;

                    case "3":
                        Console.WriteLine("Axtardiginiz Id-i daxil edin: ");
                        int idInsert = Convert.ToInt32(Console.ReadLine());
                        var st = dbContext.Stadiums.Find(idInsert);
                        Console.WriteLine($"{st.Id}-{st.Name}-{st.Capacity}-{st.HourPrice}");
                        break;

                    case "4":
                        Console.WriteLine("Silinecek Id-i daxil edin: ");
                        int idRemoved = Convert.ToInt32(Console.ReadLine());
                        var data = dbContext.Stadiums.FirstOrDefault(x => x.Id == idRemoved);
                        if (data != null)
                        {
                            dbContext.Stadiums.Remove(data);
                        }
                        break;
                    case "0":
                        Console.WriteLine("Proqram bitdi.");
                        check = false;
                        break;
                    default:
                        break;
                }


            }
            
            
        }
    }
}
