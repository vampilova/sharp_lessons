using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr1_ver3
{

    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph(new List<int> { 0, 1, 2, 1, 0 },
                        new List<int> { 1, 2, 3, 3, 2});
 
             graph.print();
             graph.add(6,5);
             graph.add(4,5);
             graph.print();
             graph.ConnectedComponent();
            graph.BFS(1);
            Console.WriteLine();
           List<int> P = graph.BFS2(1, graph.I, graph.J);
            for (int i=0;i<P.Count;i++)
            {
                Console.Write("{0} ", P[i]);
            }
        }
    }
}
