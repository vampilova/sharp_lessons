using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
namespace Heap
{

    public class Heap
    {
        public int[] A;
        public int n;
        public Heap(int[] A)
        {
            this.A = A;
            this.n = A.Length;
        }

        public void MAKE_HEAP()
        {
            for (int k = (n - 1) / 2; k >= 0; k--)
                REM_N(k);
        } //массив стал кучей

        public void REM_N(int k0)

        {
            int k1, k2;
            for (int k = k0; k < (n - 1) / 2; k = k1)
            {
                k1 = 2 * k + 1; //первый потомок k
                k2 = k1 + 1; //второй потомок k
                if (k2 < n && A[k2] < A[k1]) //второй потомок есть и он меньше первого, сравниваем со 2, а иначе работаем с первым
                    k1 = k2;
                if (A[k] < A[k1]) break;
                int temp = A[k];
                A[k] = A[k1];
                A[k1] = temp;
            }

        }

        public void REM_V(int k0)
        {
            int k1;
            for (int k = k0; k > 0; k = k1)
            {
                k1 = (k - 1) / 2; //предок k
                if (A[k1] < A[k]) break;
                int temp = A[k];
                A[k] = A[k1];
                A[k1] = temp;
            }

        } //конец внутреннего ремонта

        public int[] piramid_sort()
        {
            MAKE_HEAP();
            int n0 = A.Length;
            for (n = n0; n > 0;)
            {
                int temp = A[0];
                A[0] = A[n - 1];
                A[n - 1] = temp;
                n--;
                REM_N(0);
            }
            return A;
        }

    }
}