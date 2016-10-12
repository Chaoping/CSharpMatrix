using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject
{
    public class Matrix
    {
        // Properties and fields
        public readonly int m, n;
        public readonly double[,] elements;



        // Methods
        public Matrix(double[] data, int nrow, int ncol)
        {
            // This mimics the way how a matrix is constructed in R
            m = nrow;
            n = ncol;
            elements = new double[m, n];
            int counter = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    elements[j, i] = data[counter];
                    counter++;                
                }
            }
        }

        public Matrix(int nrow, int ncol)
        {
            // Only the size. All elements will be initialized to 0.
            m = nrow;
            n = ncol;
            elements = new double[m, n];

        }

        public override string ToString()       // need to switch to StringBuilder later.
        {
            string strMatrix = "";
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    strMatrix += elements[i,j] + "\t";
                }
                strMatrix += "\r\n";
            }
            return strMatrix;
        }

        public double this[int a, int b]        // R-like access to an element with 1-indexing
        {
            get
            {
                return elements[a - 1, b - 1];
            }

            set
            {
                elements[a - 1, b - 1] = value;
            }
        }

        public Matrix GetRow(int a, int b)
        {
            if (a > b) throw new ArgumentException("Index b must be greater than or equal to index a!");
            Matrix smallMatrix = new Matrix(b - a + 1, n);
            for (int i = 1; i <= smallMatrix.m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    smallMatrix[i, j] = this[i + a - 1, j];              
                }
            }

            return smallMatrix;
        }

        public Matrix GetRow(int a)
        {
            return GetRow(a, a);
        }

        public Matrix GetCol(int a, int b)
        {
            if (a > b) throw new ArgumentException("Index b must be greater than or equal to index a!");
            Matrix smallMatrix = new Matrix(m, b - a + 1);
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= smallMatrix.n; j++)
                {
                    smallMatrix[i, j] = this[i, j + a - 1];
                }
            }
            return smallMatrix;
        }

        public Matrix GetCol(int a)
        {
            return GetCol(a, a);
        }

        private Matrix GetMinor(int a, int b)    // Return the minor
        {
            double[] minorData = new double[m*n - m-n+1]; // array of minor data to be filled
            int dataFillIndex = 0;
            for (int j = 1; j <= n; j++) //loop through each element, saving them to the array temporarily
            {
                for (int i = 1; i <= m; i++)
                {
                    if( i != a && j != b)
                    {
                        minorData[dataFillIndex] = this[i, j];
                        dataFillIndex++;
                    }
                }
            }
            return new Matrix(minorData, m - 1, n - 1);
        }




    }

    public class MatrixException : Exception
    {
        public MatrixException(string Message)
            : base(Message)
        { }
    }
}
