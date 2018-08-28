using System;
using System.Collections;
using System.Collections.Generic;
using PutridParrot.Collections;
using PutridParrot.Utilities;

namespace PutridParrot.Math
{
    /// <summary>
    /// An implementation of a mathematical matrix based upon doubles. 
    /// </summary>
    public class Matrix : IEnumerable<double>
    {
        private const string MATRICIES_CANNOT_BE_NULL = "The supplied matricies cannot be null";

        // uses a Matrix generic collection.
        private Matrix<double> _matrix;

        /// <summary>
        /// Creates an empty matrix
        /// </summary>
        public Matrix()
        {
        }
        /// <summary>
        /// Copy constructor, creates a copy of a given matrix
        /// </summary>
        /// <param name="matrix">The matrix to copy</param>
        public Matrix(Matrix matrix)
        {
            Copy(matrix);
        }
        /// <summary>
        /// Creates a copy of the supplied two dimensional array
        /// </summary>
        /// <param name="matrix">the two dimensional array to copy</param>
        public Matrix(double[,] matrix)
        {
            Copy(matrix);
        }
        /// <summary>
        /// Creates a matrix out of the vector of the size rows * columns. Note: if
        /// this is smaller than the size of the vector then no further data is copied 
        /// from the vector. If the matrix is larger than the vector the available space 
        /// will be defaulted to the default value of a double.
        /// </summary>
        /// <param name="vector">The vector to use for the matrix data</param>
        /// <param name="rows">The number of rows within the matrix</param>
        /// <param name="columns">the number of columns within the matrix</param>
        public Matrix(double[] vector, int rows, int columns)
        {
            Copy(vector, rows, columns);
        }
        /// <summary>
        /// Creates a matrix of a given size. The matrix defaults to all elements
        /// being set to zero. Thus this can be seen as creating a "Zero Matrix".
        /// </summary>
        /// <param name="rows">The number of rows within the matrix</param>
        /// <param name="columns">The number of columns within the matrix</param>
        public Matrix(int rows, int columns)
        {
            _matrix = new Matrix<double>(rows, columns);
        }
        /// <summary>
        /// Creates a matrix of a given size and defaults it to the supplied value
        /// </summary>
        /// <param name="rows">The number of rows within the matrix</param>
        /// <param name="columns">The number of columns within the matrix</param>
        /// <param name="defaultValue">The default value to set within the matrix</param>
        public Matrix(int rows, int columns, double defaultValue)
        {
            _matrix = new Matrix<double>(rows, columns, defaultValue);
        }
        /// <summary>
        /// Indexer for indexing into the matrix via the row and column
        /// </summary>
        /// <param name="row">The row to get the data from</param>
        /// <param name="column">The column to get the data from</param>
        /// <returns>The value found at the given row/column</returns>
        public double this[int row, int column]
        {
            get => _matrix[row, column];
            set => _matrix[row, column] = value;
        }
        /// <summary>
        /// Gets whether the matrix is empty
        /// </summary>
        public bool IsEmpty => _matrix == null || _matrix.IsEmpty;

        /// <summary>
        /// Gets whether the matrix is square, that is it has the same number of rows
        /// as columns. Note: Currently it assumed that an empty matrix is also square
        /// this may be better if we threw an exception if the matrix has not been created ???
        /// </summary>
        public bool IsSquare => (_matrix == null || _matrix.Rows == _matrix.Columns);

        /// <summary>
        /// A non-negative matrix is one where all the elements are equal to 
        /// or above zero.
        /// </summary>
        public bool IsNonNegative
        {
            get
            {
                for (var i = 0; i < Rows; i++)
                {
                    for (var j = 0; j < Columns; j++)
                    {
                        if (this[i, j] < 0)
                            return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Gets whether the matrix is diagonal, that is that it's a square matrix in which 
        /// the values outside of the diagonal are all zero. A square zero matrix by definition
        /// IS diagonal.
        /// </summary>
        /// <example>
        /// 1 0 0
        /// 0 4 0 
        /// 0 0 3
        /// 
        /// The above IS a diagional matrix whereas the below is NOT
        /// 
        /// 1 0 1
        /// 0 4 0
        /// 0 0 3
        /// </example>
        public bool IsDiagonal
        {
            get
            {
                // may be better to exception !!!
                if (_matrix == null || !IsSquare)
                    return false;

                for (var i = 0; i < _matrix.Rows; i++)
                {
                    for (var j = 0; j < _matrix.Columns; j++)
                    {
                        if (i != j && _matrix[i, j] != 0)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// Gets the number of rows within the matrix
        /// </summary>
        public int Rows => _matrix?.Rows ?? 0;

        /// <summary>
        /// Gets the number of columns within the matrix
        /// </summary>
        public int Columns => _matrix?.Columns ?? 0;

        /// <summary>
        /// Copies a given matrix
        /// </summary>
        /// <param name="matrix"></param>
        public void Copy(Matrix matrix)
        {
            _matrix = new Matrix<double>(matrix._matrix);
        }
        /// <summary>
        /// Creates a matrix of rows * columns in dimensions and copies the
        /// vector into the matrix. If there is more data in the vector than the
        /// matrix can hold then the data will be truncated (i.e. not copied). If
        /// the matrix is larger than the vector the matrix will default the available
        /// space to the default for a double (0).
        /// </summary>
        /// <param name="vector">The vector to copy</param>
        /// <param name="rows">The number of rows within the matrix</param>
        /// <param name="columns">The number of columns within the matrix</param>
        public void Copy(double[] vector, int rows, int columns)
        {
            _matrix = new Matrix<double>(rows, columns);
            if (vector != null)
            {
                var idx = 0;
                for (var i = 0; i < _matrix.Rows; i++)
                {
                    for (var j = 0; j < _matrix.Columns; j++)
                    {
                        if (idx >= vector.Length)
                            return;

                        _matrix[i, j] = vector[idx++];
                    }
                }
            }
        }
        /// <summary>
        /// Copies a supplied two dimensional array
        /// </summary>
        /// <param name="matrix">Two two dimensional array to copy</param>
        public void Copy(double[,] matrix)
        {
            _matrix = new Matrix<double>(matrix);
        }
        /// <summary>
        /// Resizes the matrix without preserving existing data. Any data within the
        /// matrix will be lost.
        /// </summary>
        /// <param name="rows">The number of rows to resize the matrix to</param>
        /// <param name="columns">The number of columns to resize the matrix to</param>
        public void Resize(int rows, int columns)
        {
            if (_matrix == null)
                _matrix = new Matrix<double>();

            _matrix.Resize(rows, columns);
        }
        /// <summary>
        /// Resizes the matrix preserving existing data if preserve is True.
        /// </summary>
        /// <param name="rows">The number of rows to resize the matrix to</param>
        /// <param name="columns">The number of columns to resize the matrix to</param>
        /// <param name="preserve">True to preserve the state of the existing data otherwise False to clear the data</param>
        public void Resize(int rows, int columns, bool preserve)
        {
            if (_matrix == null)
                _matrix = new Matrix<double>();

            _matrix.Resize(rows, columns, preserve);
        }
        /// <summary>
        /// Tests to see whether the object passed in is equal to this matrix.
        /// <see cref="Equals(Matrix)"/>
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>True if the object and this matrix are equivalent else False.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Matrix);
        }
        /// <summary>
        /// Tests to see whether the matrix passed is equivalent to this matrix. If
        /// both are the same reference or if the data within them is the same then they
        /// are equivalent.
        /// </summary>
        /// <param name="matrix">The matrix to test equivalence against</param>
        /// <returns>True if the matrix is equivalent to this matrix else False.</returns>
        public bool Equals(Matrix matrix)
        {
            if (matrix == null)
                return false;

            if (Object.ReferenceEquals(this, matrix))
                return true;

            if (Rows != matrix.Rows || Columns != matrix.Columns)
                return false;

            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Columns; j++)
                {
                    if (this[i, j] != matrix[i, j])
                        return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// Transposes this matrix. i.e. each row becomes a column thus
        /// A[i,j] = A[j,i]
        /// </summary>
        public void Transpose()
        {
            var transposition = new Matrix(Columns, Rows);
            var column = 0;
            for (var i = 0; i < Rows; i++)
            {
                var row = GetRow(i);
                for (var j = 0; j < transposition.Rows; j++)
                {
                    transposition[j, column] = row[j];
                }
                column++;
            }
            _matrix = transposition._matrix;
        }

        #region Special matrix creation methods
        /// <summary>
        /// Simple method to create a matrix under the formalized name of ZeroMatrix.
        /// Creating a Matrix with only rows and columns or with a default value of 0
        /// will create a zero matrix. This just wraps it in a method name to show the 
        /// intention.
        /// </summary>
        /// <param name="size">The size is used to create a square matrix of size*size</param>
        /// <returns>A square matrix with all values set to 0.</returns>
        public static Matrix ZeroMatrix(int size)
        {
            return new Matrix(size, size);
        }
        /// <summary>
        /// Simple method to create an identity matrix. The matrix must be square and
        /// the diagonal will be filled with 1's.
        /// </summary>
        /// <param name="size">The size is used to create a square matrix of size*size</param>
        /// <returns>A square matrix with a diagonal of 1's and all other values are 0.</returns>
        public static Matrix IdentityMatrix(int size)
        {
            var matrix = new Matrix(size, size);
            for (var i = 0; i < matrix.Rows; i++)
            {
                for (var j = 0; j < matrix.Columns; j++)
                {
                    if (i == j)
                        matrix[i, j] = 1;
                }
            }
            return matrix;
        }
        /// <summary>
        /// Gets a row as a vector
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public double[] GetRow(int row)
        {
            if (IsEmpty)
                return null;

            Condition.IsTrue(row >= 0 && row < Rows, "You have specified a row which does not exist.");

            var vector = new double[Columns];
            for (var c = 0; c < vector.Length; c++)
            {
                vector[c] = _matrix[row, c];
            }
            return vector;
        }
        /// <summary>
        /// Gets a column as a vector
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public double[] GetColumn(int column)
        {
            if (IsEmpty)
                return null;

            Condition.IsTrue(column >= 0 && column < Columns, "You have specified a column which does not exist.");

            double[] vector = new double[Rows];
            for (int r = 0; r < vector.Length; r++)
            {
                vector[r] = _matrix[r, column];
            }
            return vector;
        }

        #region IEnumerable<double> Members

        IEnumerator<double> IEnumerable<double>.GetEnumerator()
        {
            return (_matrix is IEnumerable<double> enumerable ? enumerable.GetEnumerator() : null) ?? 
                throw new InvalidOperationException();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_matrix is IEnumerable enumerable ? enumerable.GetEnumerator() : null) ?? 
                throw new InvalidOperationException();
        }

        #endregion

        #endregion

        #region Mathematical methods/operators
        /// <summary>
        /// Adds two matricies together
        /// </summary>
        /// <param name="a">The left side of the addition</param>
        /// <param name="b">The right side of the addition</param>
        /// <returns>The result of the additions of the two matricies</returns>
        public static Matrix Add(Matrix a, Matrix b)
        {
            Condition.IsTrue(a != null && b != null, MATRICIES_CANNOT_BE_NULL);
            Condition.IsTrue(a.Rows == b.Rows && a.Columns == b.Columns, "The supplied matricies must be the same dimensions");

            var result = new Matrix(a.Rows, a.Columns);
            for (var i = 0; i < result.Rows; i++)
            {
                for (var j = 0; j < result.Columns; j++)
                {
                    result[i, j] = a[i, j] + b[i, j];
                }
            }
            return result;
        }
        /// <summary>
        /// <see cref="Add"/>
        /// </summary>
        /// <param name="a">The left side of the addition</param>
        /// <param name="b">The right side of the addition</param>
        /// <returns>The result of the additions of the two matricies</returns>
        public static Matrix operator +(Matrix a, Matrix b)
        {
            return Matrix.Add(a, b);
        }
        /// <summary>
        /// Subtracts one matrix from the other
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix Subtract(Matrix a, Matrix b)
        {
            Condition.IsTrue(a != null && b != null, MATRICIES_CANNOT_BE_NULL);
            Condition.IsTrue(a.Rows == b.Rows && a.Columns == b.Columns, "The supplied matricies must be the same dimensions");

            var result = new Matrix(a.Rows, a.Columns);
            for (var i = 0; i < result.Rows; i++)
            {
                for (var j = 0; j < result.Columns; j++)
                {
                    result[i, j] = a[i, j] - b[i, j];
                }
            }
            return result;
        }
        /// <summary>
        /// <see cref="Subtract"/>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix operator -(Matrix a, Matrix b)
        {
            return Matrix.Subtract(a, b);
        }

        //        public static Matrix operator -(Matrix m)
        //        {
        //            // Negative matrix
        //            return null;
        //        }

        private static double MultipleRowByColumn(double[] a, double[] b)
        {
            Condition.IsTrue(a != null && b != null, "One or both of the vectors are null.");
            Condition.IsTrue(a.Length == b.Length, "The vectors must be of the same size");

            double result = 0;
            for (var i = 0; i < a.Length; i++)
            {
                result += a[i] * b[i];
            }
            return result;
        }

        /// <summary>
        /// Multiples one matrix by the other
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix Multiply(Matrix a, Matrix b)
        {
            Condition.IsTrue(a.Columns == b.Rows, "You can only multiply matricies where the number of rows in a is the same as the number of columns in b.");

            var result = new Matrix(a.Rows, b.Columns);
            for (var i = 0; i < result.Rows; i++)
            {
                for (var j = 0; j < result.Columns; j++)
                {
                    result[i, j] = MultipleRowByColumn(a.GetRow(i), b.GetColumn(j));
                }
            }

            return result;
        }
        /// <summary>
        /// <see cref="Multiply(Matrix, Matrix)"/>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix a, Matrix b)
        {
            return Matrix.Multiply(a, b);
        }
        /// <summary>
        /// The direct sum of two matricies.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix DirectSum(Matrix a, Matrix b)
        {
            Condition.IsTrue(a != null && b != null, MATRICIES_CANNOT_BE_NULL);

            var result = new Matrix(a.Rows + b.Rows, a.Columns + b.Columns);
            for (var i = 0; i < a.Rows; i++)
            {
                for (var j = 0; j < a.Columns; j++)
                {
                    result[i, j] = a[i, j];
                }
            }

            for (var i = 0; i < b.Rows; i++)
            {
                for (var j = 0; j < b.Columns; j++)
                {
                    result[i + a.Rows, j + a.Columns] = b[i, j];
                }
            }
            return result;
        }
        /// <summary>
        /// Scalar multiplication.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Matrix Multiply(double scalar, Matrix m)
        {
            var result = new Matrix(m.Rows, m.Columns);
            for (var i = 0; i < m.Rows; i++)
            {
                for (var j = 0; j < m.Columns; j++)
                {
                    result[i, j] = scalar * m[i, j];
                }
            }
            return result;
        }
        /// <summary>
        /// <see cref="Multiply(double, Matrix)"/>
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Matrix operator *(double scalar, Matrix m)
        {
            return Matrix.Multiply(scalar, m);
        }
        /// <summary>
        /// <see cref="Multiply(double, Matrix)"/>
        /// </summary>
        /// <param name="m"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix m, double scalar)
        {
            return Matrix.Multiply(scalar, m);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Matrix a, Matrix b)
        {
            if ((object)a == null)
                return ((object)b != null);

            return !a.Equals(b);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Matrix a, Matrix b)
        {
            if ((object)a == null)
                return ((object)b == null);

            return a.Equals(b);
        }
        /// <summary>
        /// <see cref="T:Matrix.Transpose"/>
        /// </summary>
        /// <param name="m">The matrix to transpose</param>
        /// <returns>The transposed matrix</returns>
        public static Matrix Transpose(Matrix m)
        {
            var transposition = new Matrix(m);
            transposition.Transpose();
            return transposition;
        }
        #endregion
    }

}
