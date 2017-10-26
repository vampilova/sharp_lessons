using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belman_and_Deikstra
{
    class Graph
    {
        public List<int> I, J, L, H, C, R, P;
        private int[] Bucket, Fw, Bk;

        public Graph(List<int> I, List<int> J, List<int> C)
        {
            this.I = I;
            this.J = J;
            this.C = C;
            this.L = new List<int>();
            this.H = new List<int>();
            int n = -1;
            int max = 0;
            for (int i = 0; i < I.Count; i++)
            {
                max = Math.Max(I[i], J[i]);
                if (max > n) n = max;
            }
            for (int i = 0; i <= n; i++)
                H.Add(-1);
            List<int> temp = new List<int>();
            temp.AddRange(I);
            I.AddRange(J);
            J.AddRange(temp);
            C.AddRange(C);
            for (int i = 0; i < I.Count; i++)
                L.Add(-1);
            buildHL();
        }

        public void add(int i, int j, int c)
        {
            I.Insert(I.Count / 2, i);
            J.Insert(J.Count / 2, j);
            C.Insert(C.Count / 2, c);
            int max = Math.Max(i, j);
            int size = H.Count;
            for (int z = 0; z <= max - size; z++)
                H.Add(-1);
            I.Add(j);
            J.Add(i);
            C.Add(c);
            for (int k = 0; k < H.Count; k++)
                H[k] = -1;
            for (int k = 0; k < L.Count; k++)
                L[k] = -1;
            L.Add(-1);
            L.Add(-1);
            buildHL();
        }

        private void buildHL()
        {
            for (int k = 0; k < I.Count; k++)
            {
                int z = I[k];
                L[k] = H[z];
                H[z] = k;
            }
        }

        public void print()
        {
            try
            {
                StreamWriter picture = new StreamWriter("C:\\Users\\1\\Desktop\\V semester\\Комбинаторика и теория графов\\Belman_and_Deikstra\\graph.gv");
                picture.WriteLine("digraph A{");
                for (int i = 0; i < I.Count; i++)
                    picture.WriteLine(I[i] + "->" + J[i] + "[label=" + C[i] + "]");
                picture.Write("}");
                picture.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public void Ford(int vertex)
        {
            Queue<int> Q=new Queue<int>();
            int max = int.MaxValue;
            R = new List<int>();
            P = new List<int>();
            for (int i = 0; i < H.Count; i++)
                R.Add(max);
            R[vertex] = 0;
            Q.Enqueue(vertex);
            for (int i = 0; i < H.Count; i++)
                P.Add(-1);

            while (Q.Count!=0)
            {
                int from = Q.Dequeue();
                for (int i = 0; i < I.Count; i++)
                {
                    if (I[i] == from)
                    {
                        int to = J[i];
                        if (R[from]+C[i]<R[to])
                        {
                            R[to] = R[from] + C[i];
                            Q.Enqueue(to);
                            P[to] = i;
                        }
                    }
                }     
            }

            try
            {
                StreamWriter picture2 = new StreamWriter("C:\\Users\\1\\Desktop\\V semester\\Комбинаторика и теория графов\\Belman_and_Deikstra\\graph_Belman_Ford.gv");
                picture2.WriteLine("digraph G{");
                for (int i = 0; i < P.Count; i++)
                {
                    if (P[i] == -1) continue;
                    picture2.WriteLine(I[P[i]] + "->" + J[P[i]] + "[label=" + C[P[i]] + "]");
                }
                picture2.Write("}");
                picture2.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public void Deikstra(int vertex)
        {
            int i;
            int max = int.MaxValue;
            R = new List<int>();
            P = new List<int>();
            for (int k = 0; k < H.Count; k++)
            {
                R.Add(max);
                P.Add(-2);
            }
            R[vertex] = 0;
            P[vertex] = -1;
            int Cmax = int.MinValue;
            for (int k = 0; k < C.Count; k++)
            {
                if (C[k] > Cmax) Cmax = C[k];
            }
            int M=H.Count * Cmax;
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
                StreamWriter picture3 = new StreamWriter("C:\\Users\\1\\Desktop\\V semester\\Комбинаторика и теория графов\\Belman_and_Deikstra\\graph_Deikstra.gv");
                picture3.WriteLine("digraph G{");
                for (int k = 0; k < P.Count; k++)
                {
                    if (P[k] == -1) continue;
                    picture3.WriteLine(I[P[k]] + "->" + J[P[k]] + "[label=" + C[P[k]] + "]");
                }
                picture3.Write("}");
                picture3.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        private int get(int k) //взять любую вершину из черпака r и удалить ее оттуда
        {
            int i;
            i = Bucket[k];
            if (i != -1) Bucket[k] = Fw[i];
            return i;
        }

        private void insert(int i, int k) //поместить вершину i в черпак r
        {
            int j;
            j = Bucket[k];
            Fw[i] = j;
            if (j != -1) Bk[j] = i;
            Bucket[k] = i;
        }

        private void remove(int i, int k) //удалить вершину i из черпака r
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
