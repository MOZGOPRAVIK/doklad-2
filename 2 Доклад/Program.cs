using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Доклад
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Предпочтения мужчин
            Dictionary<string, List<string>> menPreferences = new Dictionary<string, List<string>>()
            {
                { "M1", new List<string> { "W1", "W2", "W3", "W4" } },
                { "M2", new List<string> { "W1", "W4", "W3", "W2" } },
                { "M3", new List<string> { "W2", "W1", "W3", "W4" } },
                { "M4", new List<string> { "W4", "W2", "W3", "W1" } }
            };

            // Предпочтения женщин
            Dictionary<string, List<string>> womenPreferences = new Dictionary<string, List<string>>()
            {
                { "W1", new List<string> { "M4", "M3", "M1", "M2" } },
                { "W2", new List<string> { "M2", "M4", "M1", "M3" } },
                { "W3", new List<string> { "M4", "M1", "M2", "M3" } },
                { "W4", new List<string> { "M3", "M2", "M1", "M4" } }
            };

            // Реализация алгоритма
            Dictionary<string, string> matches = StableMarriage(menPreferences, womenPreferences);

            // Вывод результатов
            Console.WriteLine("Результаты:");
            foreach (var match in matches)
            {
                Console.WriteLine($"{match.Key} – {match.Value}");
            }
            Console.ReadLine();
        }
        static Dictionary<string, string> StableMarriage(Dictionary<string, List<string>> menPreferences, Dictionary<string, List<string>> womenPreferences)
        {
            // Свободные мужчины
            Queue<string> freeMen = new Queue<string>(menPreferences.Keys);

            // Текущие помолвки
            Dictionary<string, string> currentEngagements = new Dictionary<string, string>();
            Dictionary<string, string> womenEngagedTo = new Dictionary<string, string>();

            // Пока есть свободные мужчины
            while (freeMen.Count > 0)
            {
                string man = freeMen.Dequeue();
                List<string> preferences = menPreferences[man];

                foreach (var woman in preferences)
                {
                    // Если женщина свободна
                    if (!currentEngagements.ContainsKey(woman))
                    {
                        currentEngagements[woman] = man;
                        womenEngagedTo[man] = woman;
                        break;
                    }
                    else
                    {
                        string currentPartner = currentEngagements[woman];
                        List<string> womanPreferences = womenPreferences[woman];

                        // Если женщина предпочитает нового мужчину
                        if (womanPreferences.IndexOf(man) < womanPreferences.IndexOf(currentPartner))
                        {
                            currentEngagements[woman] = man;
                            womenEngagedTo[man] = woman;
                            freeMen.Enqueue(currentPartner);
                            break;
                        }
                    }
                }
            }
            return womenEngagedTo;
        }
    }
}
