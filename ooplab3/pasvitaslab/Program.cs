using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pasvitaslab
{
    class Program
    {
         public static void Main(string[] args)
        { 
            Console.WriteLine("Сколько проводов?");
            int countWires = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Сколько комплектующих?");
            int countDetails = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Сколько компьютерных комлектующих?");
            int countCompParts = Convert.ToInt32(Console.ReadLine());

            List<Detail> details = new List<Detail>();

            List<Wire> wires = new List<Wire>();

            List<Icomponent> scraps = new List<Icomponent>();

            int[] counttypes = new int[1000];


            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < countWires; i++)
            {
                Console.Write("Длина провода: ");
                int length = int.Parse(Console.ReadLine());
                Console.Write("Разъём провода: ");
                string connector = (Console.ReadLine());
                Console.Write("Цена провода: ");
                int cost = int.Parse(Console.ReadLine());
                wires.Add(new Wire(cost, length, connector));
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < countDetails+countCompParts; i++)
            {
                if (i < countDetails)
                {
                    Console.Write("Мощность комплектующей: ");
                    int power = int.Parse(Console.ReadLine());
                    Console.Write("Цена комплектующей: ");
                    int cost = int.Parse(Console.ReadLine());
                    Console.Write("Тип комплектующей: ");
                    int type = int.Parse(Console.ReadLine());
                    details.Add(  new Detail(type, cost, power));
                    Console.WriteLine();
                }
                else
                {
                    Console.Write("Мощность комплектующей: ");
                    int power = int.Parse(Console.ReadLine());
                    Console.Write("Цена комплектующей: ");
                    int cost = int.Parse(Console.ReadLine());
                    Console.Write("Тип комплектующей: ");
                    int type = int.Parse(Console.ReadLine());
                    Console.Write("Рабочая температура: ");
                    int workingtemperature = int.Parse(Console.ReadLine());
                    details.Add( new ComPart(type, cost, power, workingtemperature));
                    Console.WriteLine();
                }

            }

            for (int i = 0; i < countWires + countDetails + countCompParts; i++)
            {
                if (i < countWires)
                {
                    scraps.Add (wires[i]);
                }
                else
                {  
                    scraps.Add (details[i - countWires]);
                }
            }
            double middleCost = FindMiddleCost(scraps);
            int maxCost = FindMaxCost(scraps);
            int maxPower = FindMaxPower(details);
            double middlePower = FindMiddlePower(details);

            Console.WriteLine("Средняя стоимость: " + middleCost);
            Console.WriteLine("Средняя производительность: " + middlePower);
            Console.WriteLine("Максимальная производительность: " + maxPower);
            Console.WriteLine("Максимальная стоимость: " + maxCost);
            

            for(int i = 0; i < details.Count; i++)
            {
                counttypes[details[i].Type]++;
            }
            for (int i = 0; i < counttypes.Length; i++)
            {
                if (counttypes[i] != 0)
                {
                    Console.WriteLine("Средняя мощность типа " + i + " = " + FindMiddlePowerSBT(details, i));
                    Console.WriteLine("Средняя стоимость типа " + i + " = " + FindMiddleCostSBT(details, i));
                }
            }

            var topDetails = DataProcessor<Icomponent>.DataProcessing(scraps);
            Console.WriteLine();
            Console.WriteLine("Топ деталей");
            Console.WriteLine();
            foreach (var item in topDetails)
                item.PrintInfo();
            Console.WriteLine();
            Console.WriteLine("Информацция обо всех объектах");
            Console.WriteLine();
            foreach (var item in scraps)
            {
                item.PrintInfo();
            }
            bool usersAnswer = false;
            int usersRequest;
            Console.WriteLine("Введите цену искомой детали");
            while (!usersAnswer)
            {
                try
                {
                    usersRequest = int.Parse(Console.ReadLine());
                    var result = DataProcessor<Icomponent>.Search(scraps, usersRequest);
                    if(result.Count < 1)
                    {
                        Console.WriteLine("Объект не найден");
                    }
                    else
                    {
                        result[0].PrintInfo();
                    }
                    usersAnswer = true;
                }
                catch(FormatException e)
                {
                    Console.WriteLine("Необходимо ввести целое число");
                }
                catch(ExceptionMarker ex)
                {
                    Console.WriteLine("Цена должна быть положительной");
                }
            }
            Console.ReadLine();


         }

       public static double FindMiddlePower(List<Detail> details)
        {
            double middlePower = 0;
            for (int i = 0; i< details.Count; i++)
            {
                middlePower += details[i].Power;
            }
            middlePower = middlePower / details.Count;
            return middlePower;
        }
        public static double FindMiddleCost(List<Icomponent> scraps)
        {
            double middleCost = 0;
            for (int i = 0; i < scraps.Count; i++)
            {
                middleCost += scraps[i].Cost;
            }
            middleCost = middleCost / scraps.Count;
            return middleCost;
        }
        public static int FindMaxCost(List<Icomponent> scraps)
        {
            int max = 0;
            for (int i = 0; i < scraps.Count; i++)
            {
                if (scraps[i].Cost>max)
                {
                    max = scraps[i].Cost;
                }
            }
            return max;
        }
        public static int FindMaxPower(List<Detail> details)
        {
            int max = 0;
            for (int i = 0; i < details.Count; i++)
            {
                if (details[i].Power > max)
                {
                    max = details[i].Power;
                }
            }
            return max;
        }
        public static double FindMiddlePowerSBT(List<Detail> details, int type)
        {
            int count = 0;
            double summ = 0;
            for (int i = 0; i < details.Count; i++)
            {
                if (details[i].Type == type)
                {
                    summ += details[i].Power;
                    count++;
                }
            }
            return summ / count;
        }
        public static double FindMiddleCostSBT(List<Detail> details, int type)
        {
            int count = 0;
            double summ = 0;
            for (int i = 0; i < details.Count; i++)
            {
                if (details[i].Type == type)
                {
                    summ += details[i].Cost;
                    count++;
                }
            }
            return summ / count;
        }

    }
    
    

}





