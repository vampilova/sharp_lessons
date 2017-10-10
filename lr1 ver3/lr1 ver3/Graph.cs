using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr1_ver3
{
    class Graph
    {
        List<int> I, J, IJ, L, H,numComp;
        public Graph(List<int>I,List<int>J)
        {
            this.I = I;
            this.J = J;
            IJ = new List<int>();
            for (int i = 0; i < I.Count*2; i++)
            {
                IJ.Add(0);
            }
            for (int i=0;i<I.Count;i++)
            {
                IJ[i] = I[i];
                IJ[2 * I.Count - 1 - i] = J[i];
            }
            this.L = new List<int>();
            this.H = new List<int>();
            int n = 0;
            for (int i=0;i<I.Count;i++)
            {
                if (I[i] > n)
                    n = I[i];
            }
            for (int i = 0; i <= n; i++)
                H.Add(-1);
            for (int i = 0; i < IJ.Count; i++)
                L.Add(-1);
            for (int i = 0; i < I.Count; i++)
            {
                int k = I[i];
                L[i] = H[k];
                H[k] = i;
            }
        }

        public void add(int i,int j)
        {
            if (i > j)
            {
                int temp;
                temp = j;
                j = i;
                i = temp;
            }
            I.Add(i);
            J.Add(j);
            IJ.InsertRange(I.Count()-1,new List<int> { i, j });
            int count = H.Count;
            int max = Math.Max(i, j);
            
            for (int k = 0; k <= max - count; k++)
                H.Add(-1);
            L.Add(H[i]);
            H[i] = I.Count - 1;
        }

        public void del(int vertex, int arc)
        {
            if (H[vertex] == arc)
            {
                int tmp = H[vertex];
                H[vertex] = L[H[vertex]];
                L[tmp] = -1;
            }
            else
                for (int k = H[vertex]; k != -1; k = L[k])
                {
                    if (L[k] == arc)
                    {
                        int tmp = L[k];
                        L[k] = L[L[k]];
                        L[tmp] = -1;
                    }
                }
        }

        public void del_edge_by_arc(int arc)
        {
           int vertex_begin = I[arc];
            del(vertex_begin, arc);
        }

        public void del_edge_by_vertex(int vertex_begin, int vertex_end)
        {
            int arc = 0;
            for (int k = H[vertex_begin]; k != -1; k = L[k])
            {
                if ((I[k] == vertex_begin) && (J[k] == vertex_end))
                    arc = k;
                break;
            }
            del(vertex_begin, arc);
        }

        public void DFS(int vertex,int currComp,List<int> S,List<int> Hn)
        {
            int k = 0;
            int j = 0;
            int w = 0;
            while(true)
            {
                numComp[vertex] = currComp;
                for(k=Hn[vertex];k!=-1;k=L[k])
                {
                    j = IJ[2 * I.Count - 1 - k];
                    if (numComp[j] == -1)
                        break;
                }
                if (k != -1)
                {
                    Hn[vertex] = L[k];
                    S[w] = vertex;
                    w++;
                    vertex = j;
                }
                else
                if (w == 0) break;
                else
                {
                    w--;
                    vertex = S[w];
                }
            }
        }

        public void ConnectedComponent()
        {
            int i;
            numComp = new List<int>();
            List<int> S = new List<int>();
            for (int k = 0; k < H.Count; k++)
            {
                numComp.Add(-1);
                S.Add(0);
            }
            List<int> Hn = new List<int>();
            for (int k = 0; k < H.Count; k++)
                Hn.Add(0);
            for (int k = 0; k < H.Count; k++)
                Hn[k] = H[k];
            int x = -1;
            for (int i0 = 0; i0 < H.Count; i0++)
            {
                if (numComp[i0] != -1) continue;
                x++;
                i = i0;
                DFS(i, x, S, Hn);
            }
            for (int k = 0; k < numComp.Count; k++)
                Console.Write(numComp[k] + " ");
            Console.WriteLine();
        }

        public void print()
        {
            try
            {
                StreamWriter picture = new StreamWriter("C:\\Users\\1\\Desktop\\V semester\\Комбинаторика и теория графов\\lr1 ver3\\picture.gv");
                picture.WriteLine("graph graphik{");
                for (int i = 0; i < H.Count; i++)
                {
                    if (H[i] == -1) picture.WriteLine(i + "--" + i);
                    for (int k = H[i]; k != -1; k = L[k])
                    {
                        picture.WriteLine(I[k] + "--" + J[k]);
                    }
                }
                picture.Write("}");
                picture.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}