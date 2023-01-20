using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace pasvitaslab
{
    class DataProcessor<T>
    {
        public static List<T> DataProcessing(List<Icomponent> list)
        {
            var filtredList = list
                    .Where(x => x.Cost > 10)
                    .Where(x => x.Cost < 30)
                    .OrderBy(x => x.Cost)
                    .Take(3);
            List<T> top = new List<T>();
            foreach (T item in filtredList)
                top.Add(item);
            return top;
        }
        public static List<T> Search(List<Icomponent> list, int request)
        {
            if (request < 1)
            {
                throw new ExceptionMarker("Число должно быть положительным");
            }
            var foundObjects = list.Where(x => x.Cost == request);
            List<T> result = new List<T>();
            foreach (T item in foundObjects)
                result.Add(item);
            return result;
        }
    }
}
