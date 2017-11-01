using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS_DFS_Deikstra
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph G = new Graph(
                new List<int> { 0,3,2,2 },
                new List<int> { 3,2,4,1 });
           G.print_graph();
            G.BFS(3);
           // Console.WriteLine();
            //G.BFS2(3);*/
           /* Graph_with_C Gr=new Graph_with_C(   
                new List<int> { 0, 0, 1, 1, 2 },
                new List<int> { 1, 2, 2, 3, 3 },
                new List<int> { 1, 1, 2, 3, 2 });
            Gr.print_graph();
            Gr.Deikstra(3);/*/
            Console.ReadKey();
        }
    }
}
