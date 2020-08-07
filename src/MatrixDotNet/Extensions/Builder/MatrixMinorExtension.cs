using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Extensions.Determinant;
using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions.Builder
{
    /// <summary>
    /// Represents the functional of bit operations with a matrix
    /// </summary>
    public static partial class BuildMatrix
    {
        /// <summary>
        /// Gets Minor`s matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type,</typeparam>
        /// <returns>Minor`s matrix.</returns>
        /// <exception cref="MatrixDotNetException">Throws exception if matrix is not square</exception>
        public static Matrix<T> GetMinorMatrix<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if(!matrix.IsSquare)
                throw new MatrixDotNetException("matrix is not square");
            
            Matrix<T> minor = new Matrix<T>(matrix.Rows,matrix.Columns);
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    if (matrix.GetMinorMatrix(i,j).Length == 1)
                    {
                        minor[i, j] = matrix.GetMinorMatrix(i,j)[0,0];
                    }
                    else
                    {
                        var res = matrix.GetMinorMatrix(i,j).GetDeterminant();
                        minor[i, j] = res;
                    }
                }
            }
            
            return minor;
        }
        
        /// <summary>
        /// Gets Minor`s matrix by row and column.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="row">the row index.</param>
        /// <param name="col">the column index.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Minor`s matrix by row and column index.</returns>
        public static Matrix<T> GetMinorMatrix<T>(this Matrix<T> matrix, int row, int col) where T : unmanaged
        {
            Matrix<T> result = new Matrix<T>(matrix.Rows - 1, matrix.Columns - 1);
            int m = 0, k;
            for (int i = 0; i < matrix.Rows; i++)
            {
                if (i == row) continue;
                k = 0;
                for (int j = 0; j < matrix.Columns; j++)
                {
                    if (j == col) continue;
                    result[m, k++] = matrix[i, j];
                }
                m++;
            }
            
            return result;
        }
        
        /// <summary>
        /// Gets minor of matrix by row.
        /// </summary>
        /// <param name="matrix">matrix</param>
        /// <param name="n">n</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Minor`s matrix by row index</returns>
        public static Matrix<T> GetMinor<T>(this Matrix<T> matrix,int n)
            where T : unmanaged
        {
            T[,] result = new T[matrix.Rows - 1, matrix.Rows - 1];

            for (int i = 1; i < matrix.Rows; i++)
            {
                for (int j = 0, col = 0; j < matrix.Columns; j++)
                {
                    if (j == n)
                        continue;
                    result[i - 1, col] = matrix[i, j];
                    col++;
                }
            }
            return result.ToMatrix();
        }

        
        /// <summary>
        /// Gets algebraic complement.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns></returns>
        public static Matrix<T> AlgebraicComplement<T>(this Matrix<T> matrix) where T : unmanaged
        {
            var res = matrix.GetMinorMatrix();
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    if ((i + j & 0b01) != 0)
                    {
                        res[i, j] = MathExtension.Negate(res[i, j]);
                    }
                }
            }

            return res;
        }
    }
}