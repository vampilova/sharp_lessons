using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belman_and_Deikstra
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph G = new Graph(
                new List<int> { 0,1,2,1,0,3,3 },
                new List<int> { 1,2,3,3,2,4,5}, 
                new List<int> { 2,4,3,1,5,2,10});
            G.add(4, 5, 3);
            G.print();
            G.Ford(2);
            G.Deikstra(2);
            Console.ReadKey();
        }
    }
}
