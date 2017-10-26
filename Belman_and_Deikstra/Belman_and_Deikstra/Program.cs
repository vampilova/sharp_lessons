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
                new List<int> {0,0,1,1,2},
                new List<int> {1,2,2,3,3}, 
                new List<int> {1,1,2,3,2});
            G.add(3,4,5);
            G.print();
            G.Ford(2);
            G.Deikstra(2);
            Console.ReadKey();
        }
    }
}
