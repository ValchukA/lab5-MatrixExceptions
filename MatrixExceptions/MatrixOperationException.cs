using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixExceptions
{
    public class MatrixOperationException : Exception
    {
        public (int, int) FirstMatrixSize { get; set; }

        public (int, int) SecondMatrixSize { get; set; }

        public MatrixOperationException() { }

        public MatrixOperationException(string message) : base(message) { }

        public MatrixOperationException(string message, Exception inner) : base(message, inner) { }
    }
}
