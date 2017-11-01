using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BFS_DFS_Deikstra
{
    class Graph
    {
        public List<int> I, J,IJ, H, L, R, P,numComp;
        public Graph(List<int> I, List<int> J)
        {
            this.I = I;
            this.J = J;
            H = new List<int>();
            L = new List<int>();
            IJ = new List<int>();
            for (int i = 0; i < I.Count * 2; i++)
                IJ.Add(0);
            for (int i=0;i<I.Count;i++)
            {
                IJ[i] = I[i];
                IJ[2 * I.Count - 1 - i] = J[i];
            }
            int n = -1;
            int max = 0;
            for (int i=0;i<I.Count;i++)
            {
                max = Math.Max(I[i], J[i]);
                if (max > n) n = max;
            }
            for (int i = 0; i <= n; i++)
                H.Add(-1); ;
            for (int i = 0; i < I.Count*2; i++)
                L.Add(-1);
            for (int k = 0; k < I.Count*2; k++)
            {
                int z = IJ[k];
                L[k] = H[z];
                H[z] = k;
            }
        }

        public void print_graph()
        {
            try
            {
                StreamWriter b=new StreamWriter("C: \\Users\\1\\Desktop\\V semester\\Комбинаторика и теория графов\\BFS_DFS_Deikstra\\graph.gv");
                b.WriteLine("digraph  G{");
                for (int i = 0; i < H.Count; i++)
                {
                    for (int k = H[i]; k != -1; k = L[k])
                        b.WriteLine(IJ[k] + "->" + IJ[2*I.Count-1-k]);
                }
                b.Write("}");
                b.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ConnectedComponent()
        {
            int i = 0;
            numComp = new List<int>();
            List<int> S = new List<int>();
            for (int k=0;k<H.Count;k++)
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
            for (int i0=0;i0<H.Count;i0++)
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

        public void DFS(int vertex, int currComp, List<int> S, List<int> Hn)
        {
            int k = 0;
            int j = 0;
            int w = 0;
            while(true)
            {
                numComp[vertex] = currComp;
                for (k=Hn[vertex];k!=-1;k=L[k])
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

        public void BFS(int vertex)
        {
            R = new List<int>();
            P = new List<int>();
            for (int i=0;i<H.Count;i++)
            {
                R.Add(H.Count);
                P.Add(-2);
            }
            R[vertex] = 0;
            P[vertex] = -1;
            int[] q = new int[H.Count];
            q[0] = vertex;
            int r = 0;
            int w = 1;
            while(r<w)
            {
                int i = q[r];
                r++;
                for (int k=H[i];k!=-1;k=L[k])
                {
                    int j = IJ[I.Count*2-k-1];
                    if (R[j]==H.Count)
                    {
                        R[j] = R[i] + 1;
                        P[j] = i;
                        q[w] = j;
                        w++;
                    }
                }
            }
            for (int i = 0; i < P.Count; i++)
            { Console.Write("{0} ", P[i]); }
        }


        public void BFS2(int vertex)
        {
            List<int> rang = new List<int>();
            List<int> parent = new List<int>();
            for (int i = 0; i < H.Count; i++)
            {
                rang.Add(-1);
                parent.Add(-1);
            }
            Queue<int> que = new Queue<int>();
            que.Enqueue(vertex);
            rang[vertex] = 0;
            while (que.Count != 0)
            {
                int from = que.Peek();
                que.Dequeue();
                for (int i = H[vertex]; i != -1; i = L[i])
                {
                    int to = IJ[I.Count * 2 - i - 1];
                    if (rang[to] == -1)
                    {
                        que.Enqueue(to);
                        rang[to] = rang[from] + 1;
                        parent[to] = from;
                    }
                }
            }
            for (int i = 0; i < parent.Count; i++)
            { Console.Write("{0} ", parent[i]); }
        }
    }
}
