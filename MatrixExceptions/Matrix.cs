using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixExceptions
{
    public class Matrix : ICloneable
    {
        private readonly int[,] matrix;

        public (int Rows, int Columns) MatrixSize { get; private set; }

        public int this[int row, int column]
        {
            get { return matrix[row, column]; }
            set { matrix[row, column] = value; }
        }

        public Matrix(int[,] matrix)
        {
            this.matrix = (int[,])matrix.Clone();
            MatrixSize = (matrix.GetLength(0), matrix.GetLength(1));
        }

        private static void Validate(Matrix m1, Matrix m2, Func<Matrix, Matrix, bool> validationRule, 
            string exMessage)
        {
            if (!validationRule(m1, m2))
            {
                var ex = new MatrixOperationException($"{exMessage} " +
                    $"First matrix size: {m1.MatrixSize.Rows}x{m1.MatrixSize.Columns}. " +
                    $"Second matrix size: {m2.MatrixSize.Rows}x{m2.MatrixSize.Columns}.")
                {
                    FirstMatrixSize = m1.MatrixSize,
                    SecondMatrixSize = m2.MatrixSize
                };

                throw ex;
            }
        }

        public static Matrix Add(Matrix m1, Matrix m2)
        {
            Validate(m1, m2, (x, y) => x.MatrixSize == y.MatrixSize, 
                "Matrix sizes must be the same.");

            int[,] resultMatrix = new int[m1.MatrixSize.Rows, m1.MatrixSize.Columns];

            for (int row = 0; row < m1.MatrixSize.Rows; row++)
            {
                for (int column = 0; column < m1.MatrixSize.Columns; column++)
                {
                    resultMatrix[row, column] = m1[row, column] + m2[row, column];
                }
            }

            return new Matrix(resultMatrix);
        }

        public static Matrix Subtract(Matrix m1, Matrix m2)
        {
            Validate(m1, m2, (x, y) => x.MatrixSize == y.MatrixSize,
                "Matrix sizes must be the same.");

            int[,] resultMatrix = new int[m1.MatrixSize.Rows, m1.MatrixSize.Columns];

            for (int row = 0; row < m1.MatrixSize.Rows; row++)
            {
                for (int column = 0; column < m1.MatrixSize.Columns; column++)
                {
                    resultMatrix[row, column] = m1[row, column] - m2[row, column];
                }
            }

            return new Matrix(resultMatrix);
        }

        public static Matrix Multiply(Matrix m1, Matrix m2)
        {
            Validate(m1, m2, (x, y) => x.MatrixSize.Columns == y.MatrixSize.Rows,
                "The number of columns of the first matrix must be equal " +
                "to the number of rows of the second matrix.");

            int[,] resultMatrix = new int[m1.MatrixSize.Rows, m2.MatrixSize.Columns];

            for (int i = 0; i < m2.MatrixSize.Columns; i++)
            {
                for (int j = 0; j < m1.MatrixSize.Rows; j++)
                {
                    for (int k = 0; k < m1.MatrixSize.Columns; k++)
                    {
                        resultMatrix[j, i] += m1[j, k] * m2[k, i];
                    }
                }
            }

            return new Matrix(resultMatrix);
        }

        public static Matrix operator +(Matrix m1, Matrix m2) => Add(m1, m2);

        public static Matrix operator -(Matrix m1, Matrix m2) => Subtract(m1, m2);

        public static Matrix operator *(Matrix m1, Matrix m2) => Multiply(m1, m2);

        public static Matrix GetEmpty(int rows, int columns) => new Matrix(new int[rows, columns]);

        public object Clone() => new Matrix((int[,])matrix.Clone());

        public override string ToString()
        {
            StringBuilder matrixStr = new StringBuilder();

            for (int row = 0; row < MatrixSize.Rows; row++)
            {
                for (int column = 0; column < MatrixSize.Columns; column++)
                {
                    matrixStr.Append($"{this[row, column]} ");
                }

                matrixStr.Append(Environment.NewLine);
            }

            return matrixStr.ToString();
        }
    }
}
