using System;
using System.IO;

namespace MatrixExceptions
{
    class Program
    {
        static (int[,], int[,], int[,]) ReadMatrixFromFile(string path)
        {
            using var reader = File.OpenText(path);

            int[,] firstMatrix = null;
            int[,] secondMatrix = null;
            int[,] thirdMatrix = null;

            FillMatrix(ref firstMatrix);
            FillMatrix(ref secondMatrix);
            FillMatrix(ref thirdMatrix);

            return (firstMatrix, secondMatrix, thirdMatrix);

            void FillMatrix(ref int[,] matrix)
            {
                string[] matrixSize = reader.ReadLine().Split('x');

                matrix = new int[Convert.ToInt32(matrixSize[0]), Convert.ToInt32(matrixSize[1])];

                int rowCount = 0;

                string row = null;

                while (!String.IsNullOrEmpty(row = reader.ReadLine()))
                {
                    string[] rowElements = row.Split();

                    for (int i = 0; i < rowElements.Length; i++)
                    {
                        matrix[rowCount, i] = Convert.ToInt32(rowElements[i]);
                    }

                    rowCount++;
                }
            }
        }

        static void Main()
        {
            try
            {
                (int[,] firstMatrix, int[,] secondMatrix, int[,] thirdMatrix) = ReadMatrixFromFile("matrix.txt");

                Matrix m1 = new Matrix(firstMatrix);
                Matrix m2 = new Matrix(secondMatrix);
                Matrix m3 = new Matrix(thirdMatrix);

                Console.WriteLine(Matrix.Add(m1, m3));
                Console.WriteLine(m1 - m3);
                Console.WriteLine(m1 * m2);
            }
            catch (MatrixOperationException ex)
            {
                Console.WriteLine("MatrixOperationException has occured.");
                Console.WriteLine($"Description: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
