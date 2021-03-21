using System;

namespace MatrixExceptions
{
    class Program
    {
        static void Main()
        {
            int[,] arr1 = new int[2, 3] { { 2, -3, 7 },
                                          { 4, 6, 4 } };
            int[,] arr2 = new int[3, 1] { { 1 }, { 2 }, { 3 } };
            int[,] arr3 = new int[2, 3] { { 6, 0, 3 },
                                          { 5, 1, -7 } };

            Matrix m1 = new Matrix(arr1);
            Matrix m2 = new Matrix(arr2);
            Matrix m3 = new Matrix(arr3);

            try
            {
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
