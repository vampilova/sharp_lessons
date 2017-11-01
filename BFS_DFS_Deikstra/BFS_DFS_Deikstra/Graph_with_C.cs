using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BFS_DFS_Deikstra
{
    class Graph_with_C
    {
        public List<int> I, J, L, H, C, R, P;
        private int[] Bucket, Fw, Bk;
        public Graph_with_C(List<int> I, List<int> J, List<int> C)
        {
            this.I = I;
            this.J = J;
            this.C = C;
            this.L = new List<int>();
            this.H = new List<int>();
            int n = -1;
            int max = 0;
            for (int i=0;i<I.Count;i++)
            {
                max = Math.Max(I[i], J[i]);
                if (max > n) n = max;
            }
            for (int i = 0; i <= n; i++)
                H.Add(-1);
            List<int> tmp = new List<int>();
            tmp.AddRange(I);
            I.AddRange(J);
            J.AddRange(tmp);
            C.AddRange(C);
            for (int i = 0; i < I.Count; i++)
                L.Add(-1);
            Build_HL();
        }

        private void Build_HL()
        {
            for (int k=0;k<I.Count;k++)
            {
                int z = I[k];
                L[k] = H[z];
                H[z] = k;
            }
        }

        public void print_graph()
        {
            try
            {
                StreamWriter c = new StreamWriter("C:\\Users\\1\\Desktop\\V semester\\Комбинаторика и теория графов\\BFS_DFS_Deikstra\\graph_withC.gv");
                c.WriteLine("digraph  G{");
                for (int k = 0; k < I.Count; k++)
                {
                    c.WriteLine(I[k] + "->" + J[k] + "[label=" + C[k] + "]");
                }
                c.Write("}");
                c.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void Deikstra(int vertex)
        {
            int i;
            int max = int.MaxValue;
            R = new List<int>();
            P = new List<int>();
            for (int k=0;k<H.Count;k++)
            {
                R.Add(max);
                P.Add(-2);
            }
            R[vertex] = 0;
            P[vertex] = -1;
            int Cmax = int.MinValue;
            for (int k=0;k<C.Count;k++)
            {
                if (C[k] > Cmax) Cmax = C[k];
            }
            int M = H.Count * Cmax;
            Bucket = new int[M + 1];
            for (int k = 0; k <= M; k++)
                Bucket[k] = -1;
            Bk = new int[H.Count];
            Fw = new int[H.Count];
            for (int k = 0; k < H.Count; k++)
            {
                Bk[k] = -1;
                Fw[k] = -1;
            }
            insert(vertex, 0);
            for (int b=0;b<=M;b++)
                while ((i=get(b))!=-1)
                    for (int k=H[i];k!=-1;k=L[k])
                    {
                        int j = J[k];
                        int rj = R[j];
                        if (R[i]+C[k]<rj)
                        {
                            R[j] = R[i] + C[k];
                            P[j] = k;
                            if (rj != max) remove(j, rj);
                            insert(j, R[j]);
                        }
                    }
            try
            {
                StreamWriter D = new StreamWriter("C: \\Users\\1\\Desktop\\V semester\\Комбинаторика и теория графов\\BFS_DFS_Deikstra\\deikstra.gv");
                D.WriteLine("digraph G {");
                for (int k=0;k<P.Count;k++)
                {
                    if (P[k] == -1) continue;
                    D.WriteLine(I[P[k]] + "->" + J[P[k]] + "[label=" + C[P[k]] + "]");
                }
                D.Write("}");
                D.Close();
            }  
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        private int get(int k)
        {
            int i;
            i = Bucket[k];
            if (i != -1) Bucket[k] = Fw[i];
            return i;
        }

        private void insert (int i, int k)
        {
            int j;
            j = Bucket[k];
            Fw[i] = j;
            if (j != -1) Bk[j] = i;
            Bucket[k] = i;
        }

        private void remove(int i, int k)
        {
            int fi = Fw[i];
            int bi = Bk[i];
            if (i == Bucket[k])
                Bucket[k] = fi;
            else
            {
                Fw[bi] = fi;
                if (fi != -1) Bk[fi] = bi;
            }
        }
    }
}
