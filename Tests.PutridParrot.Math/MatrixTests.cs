using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using PutridParrot.Math;

namespace Tests.PutridParrot.Math
{
    [TestFixture]
    public class MatrixTests
    {
        [Test]
        public void CreateEmptyMatrix()
        {
            var matrix = new Matrix();
            Assert.IsTrue(matrix.IsEmpty);
        }
        [Test]
        public void CreateMatrix()
        {
            var matrix = new Matrix(3, 5);
            Assert.IsFalse(matrix.IsEmpty);
            Assert.AreEqual(3, matrix.Rows);
            Assert.AreEqual(5, matrix.Columns);
        }
        [Test]
        public void CreateCopyMatrix()
        {
            var matrix = new Matrix(2, 4);
            var count = 0;
            for (var i = 0; i < matrix.Rows; i++)
            {
                for (var j = 0; j < matrix.Columns; j++)
                {
                    matrix[i, j] = count++;
                }
            }

            var copy = new Matrix(matrix);

            Assert.AreEqual(matrix.Rows, copy.Rows);
            Assert.AreEqual(matrix.Columns, copy.Columns);
            count = 0;
            for (var i = 0; i < copy.Rows; i++)
            {
                for (var j = 0; j < copy.Columns; j++)
                {
                    Assert.AreEqual(count++, copy[i, j]);
                }
            }
        }

        [Test]
        public void CreateMatrixFromVector()
        {
            var vector = new double[] { 1, 2, 3, 4, 5, 6 };
            var matrix = new Matrix(vector, 2, 3);
            var idx = 0;
            for (var i = 0; i < matrix.Rows; i++)
            {
                for (var j = 0; j < matrix.Columns; j++)
                {
                    Assert.AreEqual(vector[idx++], matrix[i, j]);
                }
            }
        }

        [Test]
        public void CreateMatrixFromVectorTruncated()
        {
            var vector = new double[] { 1, 2, 3, 4, 5, 6 };
            var matrix = new Matrix(vector, 2, 2);
            var idx = 0;
            for (var i = 0; i < matrix.Rows; i++)
            {
                for (var j = 0; j < matrix.Columns; j++)
                {
                    Assert.AreEqual(vector[idx++], matrix[i, j]);
                }
            }
        }

        [Test]
        public void CreateMatrixFromVectorLargerThanRequired()
        {
            var vector = new double[] { 1, 2, 3, 4, 5, 6 };
            var matrix = new Matrix(vector, 3, 3);
            var idx = 0;
            for (var i = 0; i < matrix.Rows; i++)
            {
                for (var j = 0; j < matrix.Columns; j++)
                {
                    Assert.AreEqual(idx >= vector.Length ? 
                        default(double) : 
                        vector[idx++], matrix[i, j]);
                }
            }
        }

        [Test]
        public void CreateCopyMatrixFrom2DArray()
        {
            var matrix = new double[3, 5];
            var count = 0;
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = count++;
                }
            }

            var copy = new Matrix(matrix);

            Assert.AreEqual(matrix.GetLength(0), copy.Rows);
            Assert.AreEqual(matrix.GetLength(1), copy.Columns);
            count = 0;
            for (var i = 0; i < copy.Rows; i++)
            {
                for (var j = 0; j < copy.Columns; j++)
                {
                    Assert.AreEqual(count++, copy[i, j]);
                }
            }
        }

        [Test]
        public void CreateMatrixWithDefault()
        {
            var matrix = new Matrix(5, 3, 666.666);
            for (var i = 0; i < matrix.Rows; i++)
            {
                for (var j = 0; j < matrix.Columns; j++)
                {
                    Assert.AreEqual(666.666, matrix[i, j]);
                }
            }
        }

        [Test]
        public void SquareMatrix()
        {
            var matrix = new Matrix(9, 9);
            Assert.IsFalse(matrix.IsEmpty);
            Assert.IsTrue(matrix.IsSquare);
        }

        [Test]
        public void NotSquareMatrix()
        {
            var matrix = new Matrix(9, 3);
            Assert.IsFalse(matrix.IsEmpty);
            Assert.IsFalse(matrix.IsSquare);
        }

        [Test]
        public void IsDiagonalEmptyMatrix()
        {
            var matrix = new Matrix();

            Assert.IsFalse(matrix.IsDiagonal);
        }

        [Test]
        public void IsDiagonalNonSquareMatrix()
        {
            Matrix matrix = new Matrix(3, 5);

            Assert.IsFalse(matrix.IsDiagonal);
        }

        [Test]
        public void IsDiagonal()
        {
            var matrix = new Matrix(3, 3)
            {
                [0, 0] = 1,
                [0, 1] = 0,
                [0, 2] = 0,
                [1, 0] = 0,
                [1, 1] = 4,
                [1, 2] = 0,
                [2, 0] = 0,
                [2, 1] = 0,
                [2, 2] = 5
            };

            Assert.IsTrue(matrix.IsDiagonal);
        }

        [Test]
        public void IsNotDiagonal()
        {
            var matrix = new Matrix(3, 3)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [0, 2] = 0,
                [1, 0] = 0,
                [1, 1] = 4,
                [1, 2] = 0,
                [2, 0] = 0,
                [2, 1] = 0,
                [2, 2] = 5
            };

            Assert.IsFalse(matrix.IsDiagonal);
        }

        [Test]
        public void GetRow()
        {
            var matrix = new Matrix(3, 3)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [0, 2] = 0,
                [1, 0] = 0,
                [1, 1] = 4,
                [1, 2] = 0,
                [2, 0] = 0,
                [2, 1] = 0,
                [2, 2] = 5
            };

            var vector = matrix.GetRow(1);
            Assert.IsNotNull(vector);
            Assert.AreEqual(vector.Length, matrix.Columns);
            for (var i = 0; i < vector.Length; i++)
            {
                Assert.AreEqual(matrix[1, i], vector[i]);
            }
        }

        [Test]
        public void GetRowFromEmptyMatrix()
        {
            var matrix = new Matrix();
            var vector = matrix.GetRow(0);
            Assert.IsNull(vector);
        }

        [Test]
        public void GetColumn()
        {
            var matrix = new Matrix(3, 3)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [0, 2] = 0,
                [1, 0] = 0,
                [1, 1] = 4,
                [1, 2] = 0,
                [2, 0] = 0,
                [2, 1] = 0,
                [2, 2] = 5
            };

            var vector = matrix.GetColumn(2);
            Assert.IsNotNull(vector);
            Assert.AreEqual(vector.Length, matrix.Rows);
            for (var i = 0; i < vector.Length; i++)
            {
                Assert.AreEqual(matrix[i, 2], vector[i]);
            }
        }

        [Test]
        public void GetColumnFromEmptyMatrix()
        {
            var matrix = new Matrix();
            var vector = matrix.GetColumn(0);
            Assert.IsNull(vector);
        }

        [Test]
        public void ZeroMatrixIsDiagonal()
        {
            var zeroMatrix = Matrix.ZeroMatrix(6);
            Assert.IsTrue(zeroMatrix.IsDiagonal);
        }

        [Test]
        public void ZeroMatrix()
        {
            var zeroMatrix = Matrix.ZeroMatrix(3);
            for (var i = 0; i < zeroMatrix.Rows; i++)
            {
                for (var j = 0; j < zeroMatrix.Columns; j++)
                {
                    Assert.AreEqual(0, zeroMatrix[i, j]);
                }
            }
        }

        [Test]
        public void IdentityMatrix()
        {
            var zeroMatrix = Matrix.IdentityMatrix(3);
            for (var i = 0; i < zeroMatrix.Rows; i++)
            {
                for (var j = 0; j < zeroMatrix.Columns; j++)
                {
                    Assert.AreEqual(i == j ? 1 : 0, zeroMatrix[i, j]);
                }
            }
        }

        [Test]
        public void AddMatricies()
        {
            var a = new Matrix(3, 3)
            {
                [0, 0] = 1,
                [0, 1] = 3,
                [0, 2] = 2,
                [1, 0] = 1,
                [1, 1] = 0,
                [1, 2] = 0,
                [2, 0] = 1,
                [2, 1] = 2,
                [2, 2] = 2
            };
            var b = new Matrix(3, 3)
            {
                [0, 0] = 0,
                [0, 1] = 0,
                [0, 2] = 5,
                [1, 0] = 7,
                [1, 1] = 5,
                [1, 2] = 0,
                [2, 0] = 2,
                [2, 1] = 1,
                [2, 2] = 1
            };

            var c = a + b;

            var result = new Matrix(3, 3)
            {
                [0, 0] = 1,
                [0, 1] = 3,
                [0, 2] = 7,
                [1, 0] = 8,
                [1, 1] = 5,
                [1, 2] = 0,
                [2, 0] = 3,
                [2, 1] = 3,
                [2, 2] = 3
            };

            Assert.AreEqual(result.Rows, c.Rows);
            Assert.AreEqual(result.Columns, c.Columns);

            for (var i = 0; i < c.Rows; i++)
            {
                for (var j = 0; j < c.Columns; j++)
                {
                    Assert.AreEqual(result[i, j], c[i, j]);
                }
            }
        }

        [Test]
        public void AddEqualsMatricies()
        {
            var a = new Matrix(3, 3)
            {
                [0, 0] = 1,
                [0, 1] = 3,
                [0, 2] = 2,
                [1, 0] = 1,
                [1, 1] = 0,
                [1, 2] = 0,
                [2, 0] = 1,
                [2, 1] = 2,
                [2, 2] = 2
            };
            var b = new Matrix(3, 3)
            {
                [0, 0] = 0,
                [0, 1] = 0,
                [0, 2] = 5,
                [1, 0] = 7,
                [1, 1] = 5,
                [1, 2] = 0,
                [2, 0] = 2,
                [2, 1] = 1,
                [2, 2] = 1
            };

            a += b;

            var result = new Matrix(3, 3)
            {
                [0, 0] = 1,
                [0, 1] = 3,
                [0, 2] = 7,
                [1, 0] = 8,
                [1, 1] = 5,
                [1, 2] = 0,
                [2, 0] = 3,
                [2, 1] = 3,
                [2, 2] = 3
            };

            Assert.AreEqual(result.Rows, a.Rows);
            Assert.AreEqual(result.Columns, a.Columns);

            for (var i = 0; i < a.Rows; i++)
            {
                for (var j = 0; j < a.Columns; j++)
                {
                    Assert.AreEqual(result[i, j], a[i, j]);
                }
            }
        }
        [Test]
        public void SubtractMatricies()
        {
            var a = new Matrix(3, 3)
            {
                [0, 0] = 1,
                [0, 1] = 3,
                [0, 2] = 2,
                [1, 0] = 1,
                [1, 1] = 0,
                [1, 2] = 0,
                [2, 0] = 1,
                [2, 1] = 2,
                [2, 2] = 2
            };
            var b = new Matrix(3, 3)
            {
                [0, 0] = 0,
                [0, 1] = 0,
                [0, 2] = 5,
                [1, 0] = 7,
                [1, 1] = 5,
                [1, 2] = 0,
                [2, 0] = 2,
                [2, 1] = 1,
                [2, 2] = 1
            };

            var c = a - b;

            var result = new Matrix(3, 3)
            {
                [0, 0] = 1,
                [0, 1] = 3,
                [0, 2] = -3,
                [1, 0] = -6,
                [1, 1] = -5,
                [1, 2] = 0,
                [2, 0] = -1,
                [2, 1] = 1,
                [2, 2] = 1
            };

            Assert.AreEqual(result.Rows, c.Rows);
            Assert.AreEqual(result.Columns, c.Columns);

            for (var i = 0; i < c.Rows; i++)
            {
                for (var j = 0; j < c.Columns; j++)
                {
                    Assert.AreEqual(result[i, j], c[i, j]);
                }
            }
        }

        [Test]
        public void DirectSum()
        {
            var a = new Matrix(2, 3)
            {
                [0, 0] = 1,
                [0, 1] = 3,
                [0, 2] = 2,
                [1, 0] = 2,
                [1, 1] = 3,
                [1, 2] = 1
            };
            var b = new Matrix(2, 2)
            {
                [0, 0] = 1,
                [0, 1] = 6,
                [1, 0] = 0,
                [1, 1] = 1
            };

            var c = Matrix.DirectSum(a, b);

            var result = new Matrix(4, 5)
            {
                [0, 0] = 1,
                [0, 1] = 3,
                [0, 2] = 2,
                [0, 3] = 0,
                [0, 4] = 0,
                [1, 0] = 2,
                [1, 1] = 3,
                [1, 2] = 1,
                [1, 3] = 0,
                [1, 4] = 0,
                [2, 0] = 0,
                [2, 1] = 0,
                [2, 2] = 0,
                [2, 3] = 1,
                [2, 4] = 6,
                [3, 0] = 0,
                [3, 1] = 0,
                [3, 2] = 0,
                [3, 3] = 0,
                [3, 4] = 1
            };

            Assert.AreEqual(result.Rows, c.Rows);
            Assert.AreEqual(result.Columns, c.Columns);

            for (var i = 0; i < c.Rows; i++)
            {
                for (var j = 0; j < c.Columns; j++)
                {
                    Assert.AreEqual(result[i, j], c[i, j]);
                }
            }
        }

        [Test]
        public void MultiplyScalar()
        {
            var matrix = new Matrix(3, 3)
            {
                [0, 0] = 1,
                [0, 1] = 3,
                [0, 2] = 5,
                [1, 0] = -1,
                [1, 1] = -8,
                [1, 2] = 10,
                [2, 0] = -7,
                [2, 1] = -5,
                [2, 2] = 13
            };

            var expected = new Matrix(3, 3)
            {
                [0, 0] = 4,
                [0, 1] = 12,
                [0, 2] = 20,
                [1, 0] = -4,
                [1, 1] = -32,
                [1, 2] = 40,
                [2, 0] = -28,
                [2, 1] = -20,
                [2, 2] = 52
            };

            var result = 4 * matrix;

            Assert.AreEqual(expected.Rows, result.Rows);
            Assert.AreEqual(expected.Columns, result.Columns);
            for (var i = 0; i < result.Rows; i++)
            {
                for (var j = 0; j < result.Columns; j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j]);
                }
            }
        }

        [Test]
        public void MultiplyScalar2()
        {
            var matrix = new Matrix(3, 3)
            {
                [0, 0] = 1,
                [0, 1] = 3,
                [0, 2] = 5,
                [1, 0] = -1,
                [1, 1] = -8,
                [1, 2] = 10,
                [2, 0] = -7,
                [2, 1] = -5,
                [2, 2] = 13
            };

            var expected = new Matrix(3, 3)
            {
                [0, 0] = 4,
                [0, 1] = 12,
                [0, 2] = 20,
                [1, 0] = -4,
                [1, 1] = -32,
                [1, 2] = 40,
                [2, 0] = -28,
                [2, 1] = -20,
                [2, 2] = 52
            };

            var result = matrix * 4;

            Assert.AreEqual(expected.Rows, result.Rows);
            Assert.AreEqual(expected.Columns, result.Columns);
            for (var i = 0; i < result.Rows; i++)
            {
                for (var j = 0; j < result.Columns; j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j]);
                }
            }
        }

        [Test]
        public void Multiplication()
        {
            var a = new Matrix(1, 4)
            {
                [0, 0] = 2,
                [0, 1] = 0,
                [0, 2] = -1,
                [0, 3] = 1
            };

            var b = new Matrix(4, 3)
            {
                [0, 0] = 1,
                [0, 1] = 5,
                [0, 2] = -7,
                [1, 0] = 1,
                [1, 1] = 1,
                [1, 2] = 0,
                [2, 0] = 0,
                [2, 1] = -1,
                [2, 2] = 1,
                [3, 0] = 2,
                [3, 1] = 0,
                [3, 2] = 0
            };

            var expected = new Matrix(1, 3)
            {
                [0, 0] = 4,
                [0, 1] = 11,
                [0, 2] = -15
            };

            var result = a * b;

            Assert.AreEqual(a.Rows, result.Rows);
            Assert.AreEqual(b.Columns, result.Columns);

            for (var i = 0; i < expected.Rows; i++)
            {
                for (var j = 0; j < expected.Columns; j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j]);
                }
            }
        }

        [Test]
        public void Multiplication2()
        {
            var a = new Matrix(2, 4)
            {
                [0, 0] = 2,
                [0, 1] = 0,
                [0, 2] = -1,
                [0, 3] = 1,
                [1, 0] = 1,
                [1, 1] = 2,
                [1, 2] = 0,
                [1, 3] = 1
            };

            var b = new Matrix(4, 3)
            {
                [0, 0] = 1,
                [0, 1] = 5,
                [0, 2] = -7,
                [1, 0] = 1,
                [1, 1] = 1,
                [1, 2] = 0,
                [2, 0] = 0,
                [2, 1] = -1,
                [2, 2] = 1,
                [3, 0] = 2,
                [3, 1] = 0,
                [3, 2] = 0
            };

            var expected = new Matrix(2, 3)
            {
                [0, 0] = 4,
                [0, 1] = 11,
                [0, 2] = -15,
                [1, 0] = 5,
                [1, 1] = 7,
                [1, 2] = -7
            };

            var result = a * b;

            Assert.AreEqual(a.Rows, result.Rows);
            Assert.AreEqual(b.Columns, result.Columns);

            for (var i = 0; i < expected.Rows; i++)
            {
                for (var j = 0; j < expected.Columns; j++)
                {
                    Assert.AreEqual(expected[i, j], result[i, j]);
                }
            }
        }
        [Test]
        public void Equality()
        {
            var a = new Matrix(2, 2)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [1, 0] = 3,
                [1, 1] = 4
            };

            var b = new Matrix(2, 2)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [1, 0] = 3,
                [1, 1] = 4
            };

            Assert.IsTrue(a == b);
            Assert.IsTrue(b == a);
        }
        [Test]
        public void EqualityFailOnDimensions()
        {
            var a = new Matrix(1, 2)
            {
                [0, 0] = 1,
                [0, 1] = 2
            };

            var b = new Matrix(2, 2)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [1, 0] = 3,
                [1, 1] = 4
            };

            Assert.IsFalse(a == b);
            Assert.IsFalse(b == a);
        }
        [Test]
        public void EqualityObjectFailOnDimensions()
        {
            var a = new Matrix(1, 2)
            {
                [0, 0] = 1,
                [0, 1] = 2
            };

            var b = new Matrix(2, 2)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [1, 0] = 3,
                [1, 1] = 4
            };

            Assert.IsFalse(a.Equals((object)b));
        }
        [Test]
        public void EqualityReference()
        {
            var a = new Matrix(2, 2)
            {
                [0, 0] = 4,
                [0, 1] = 3,
                [1, 0] = 2,
                [1, 1] = 1
            };

            var b = a;

            Assert.IsTrue(a == b);
            Assert.IsTrue(b == a);
        }

        [Test]
        public void InEqualityNull()
        {
            var a = new Matrix(2, 2)
            {
                [0, 0] = 4,
                [0, 1] = 3,
                [1, 0] = 2,
                [1, 1] = 1
            };

            Assert.IsTrue(null != a);
            Assert.IsTrue(a != null);
        }
        [Test]
        public void InEquality()
        {
            var a = new Matrix(2, 2)
            {
                [0, 0] = 4,
                [0, 1] = 3,
                [1, 0] = 2,
                [1, 1] = 1
            };

            var b = new Matrix(2, 2)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [1, 0] = 3,
                [1, 1] = 4
            };

            Assert.IsTrue(a != b);
            Assert.IsTrue(b != a);
        }

        [Test]
        public void Resize()
        {
            var a = new Matrix(2, 2)
            {
                [0, 0] = 4,
                [0, 1] = 3,
                [1, 0] = 2,
                [1, 1] = 1
            };

            a.Resize(3, 3);
            Assert.AreEqual(3, a.Rows);
            Assert.AreEqual(3, a.Columns);
            for (var i = 0; i < a.Rows; i++)
            {
                for (var j = 0; j < a.Columns; j++)
                {
                    Assert.AreEqual(default(double), a[i, j]);
                }
            }
        }

        [Test]
        public void ResizeEmptyMatrix()
        {
            var m = new Matrix();
            m.Resize(3, 4);

            Assert.AreEqual(3, m.Rows);
            Assert.AreEqual(4, m.Columns);
        }

        [Test]
        public void ResizeEmptyMatrixNoPreserve()
        {
            var m = new Matrix();
            m.Resize(3, 4, false);

            Assert.AreEqual(3, m.Rows);
            Assert.AreEqual(4, m.Columns);
        }

        [Test]
        public void ResizeNoPreserve()
        {
            var a = new Matrix(2, 2)
            {
                [0, 0] = 4,
                [0, 1] = 3,
                [1, 0] = 2,
                [1, 1] = 1
            };

            a.Resize(3, 3, false);
            Assert.AreEqual(3, a.Rows);
            Assert.AreEqual(3, a.Columns);
            for (var i = 0; i < a.Rows; i++)
            {
                for (var j = 0; j < a.Columns; j++)
                {
                    Assert.AreEqual(default(double), a[i, j]);
                }
            }
        }

        [Test]
        public void ResizePreserve()
        {
            var a = new Matrix(2, 2)
            {
                [0, 0] = 4,
                [0, 1] = 3,
                [1, 0] = 2,
                [1, 1] = 1
            };

            var b = new Matrix(2, 2)
            {
                [0, 0] = 4,
                [0, 1] = 3,
                [1, 0] = 2,
                [1, 1] = 1
            };

            a.Resize(3, 3, true);
            Assert.AreEqual(3, a.Rows);
            Assert.AreEqual(3, a.Columns);
            for (var i = 0; i < a.Rows; i++)
            {
                for (var j = 0; j < a.Columns; j++)
                {
                    if (i >= b.Rows || j >= b.Rows)
                    {
                        Assert.AreEqual(default(double), a[i, j]);
                    }
                    else
                    {
                        Assert.AreEqual(b[i, j], a[i, j]);
                    }
                }
            }
        }

        [Test]
        public void MatrixGetHashCode()
        {
            var matrix = new Matrix();
            matrix.GetHashCode();
        }

        [Test]
        public void TransposeSquareMatrix()
        {
            var m = new Matrix(2, 2)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [1, 0] = 3,
                [1, 1] = 4
            };

            var expected = new Matrix(2, 2)
            {
                [0, 0] = 1,
                [0, 1] = 3,
                [1, 0] = 2,
                [1, 1] = 4
            };

            m.Transpose();

            Assert.IsTrue(expected == m);
        }

        [Test]
        public void TransposeSquareMatrixStaticVersion()
        {
            var m = new Matrix(2, 2)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [1, 0] = 3,
                [1, 1] = 4
            };

            var expected = new Matrix(2, 2)
            {
                [0, 0] = 1,
                [0, 1] = 3,
                [1, 0] = 2,
                [1, 1] = 4
            };

            var result = Matrix.Transpose(m);

            Assert.IsTrue(expected == result);
        }

        [Test]
        public void TransposeRectangleMatrix()
        {
            var m = new Matrix(3, 2)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [1, 0] = 3,
                [1, 1] = 4,
                [2, 0] = 5,
                [2, 1] = 6
            };

            var expected = new Matrix(2, 3)
            {
                [0, 0] = 1,
                [0, 1] = 3,
                [0, 2] = 5,
                [1, 0] = 2,
                [1, 1] = 4,
                [1, 2] = 6
            };

            m.Transpose();

            Assert.IsTrue(expected == m);
        }

        [Test]
        public void TransposeRectangleMatrixStaticVersion()
        {
            var m = new Matrix(3, 2)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [1, 0] = 3,
                [1, 1] = 4,
                [2, 0] = 5,
                [2, 1] = 6
            };

            var expected = new Matrix(2, 3)
            {
                [0, 0] = 1,
                [0, 1] = 3,
                [0, 2] = 5,
                [1, 0] = 2,
                [1, 1] = 4,
                [1, 2] = 6
            };

            var result = Matrix.Transpose(m);

            Assert.IsTrue(expected == result);

            // the original matrix should not have changed so test it !
            double count = 1;
            foreach (var d in m)
            {
                Assert.AreEqual(count++, d);
            }
        }

        [Test]
        public void EnumeratorGeneric()
        {
            var m = new Matrix(3, 2)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [1, 0] = 3,
                [1, 1] = 4,
                [2, 0] = 5,
                [2, 1] = 6
            };

            var counter = 1;
            IEnumerable<double> enumerable = m;
            IEnumerator<double> enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Assert.AreEqual(counter++, enumerator.Current);
            }
        }

        [Test]
        public void EnumeratorNonGeneric()
        {
            var m = new Matrix(3, 2)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [1, 0] = 3,
                [1, 1] = 4,
                [2, 0] = 5,
                [2, 1] = 6
            };

            var counter = 1;
            IEnumerable enumerable = m;
            IEnumerator enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Assert.AreEqual(counter++, (double)enumerator.Current);
            }
        }

        [Test, Ignore("Not yet implemented")]
        public void NegativeMatrix()
        {
            var m = new Matrix(3, 2)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [1, 0] = 3,
                [1, 1] = 4,
                [2, 0] = 5,
                [2, 1] = 6
            };

            //Matrix result = -m;
        }

        [Test]
        public void IsNonNegative()
        {
            var m = new Matrix(3, 2)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [1, 0] = 3,
                [1, 1] = 4,
                [2, 0] = 5,
                [2, 1] = 6
            };

            Assert.IsTrue(m.IsNonNegative);
        }

        [Test]
        public void NotIsNonNegative()
        {
            var m = new Matrix(3, 2)
            {
                [0, 0] = 1,
                [0, 1] = 2,
                [1, 0] = 3,
                [1, 1] = 4,
                [2, 0] = -5,
                [2, 1] = 6
            };

            Assert.IsFalse(m.IsNonNegative);
        }
    }

}
