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
          //  graph.add(4, 5);
            graph.print();
           graph.add(7, 8);
            graph.print();
            //graph.del_edge_by_arc(1);
            //graph.print();
           // graph.del_edge_by_vertex(0, 1);
            //graph.print();
            graph.ConnectedComponent();
        }
    }
}
