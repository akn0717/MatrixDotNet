﻿namespace MatrixDotNet.Extensions.Conversion
{
    /// <summary>
    /// Represents converter which can change matrix.
    /// </summary>
    public static partial class MatrixConverter
    {
        /// <summary>
        /// Copy to matrix.
        /// </summary>
        /// <param name="matrix1">copy from matrix.</param>
        /// <param name="dimension1">dimension matrix1.</param>
        /// <param name="start">start index which start copy.</param>
        /// <param name="matrix2">copy to matrix.</param>
        /// <param name="dimension2">dimension matrix2</param>
        /// <param name="destinationIndex">start index.</param>
        /// <param name="length">length copy.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        public static void CopyTo<T>(Matrix<T> matrix1,int dimension1, int start,Matrix<T> matrix2,int dimension2,int destinationIndex,int length) 
            where T : unmanaged
        {
            for (int i = start, k = destinationIndex; k < length; i++,k++)
            {
                matrix2[dimension2,k] = matrix1[dimension1,i];
            }
        }
        
        /// <summary>
        /// Copy to matrix by row or column.
        /// </summary>
        /// <param name="state">row or column.</param>
        /// <param name="matrix1">copy from matrix.</param>
        /// <param name="dimension1">dimension matrix1.</param>
        /// <param name="start">start index which start copy.</param>
        /// <param name="matrix2">copy to matrix.</param>
        /// <param name="dimension2">dimension matrix2</param>
        /// <param name="destinationIndex">start index.</param>
        /// <param name="length">length copy.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        public static void CopyTo<T>(State state,Matrix<T> matrix1,int dimension1, int start,Matrix<T> matrix2,int dimension2,int destinationIndex,int length) 
            where T : unmanaged
        {
            if (state == State.Row)
            {
                for (int i = start, k = destinationIndex; k < length; i++,k++)
                {
                    matrix2[dimension2,k] = matrix1[dimension1,i];
                }    
            }

            if (state == State.Column)
            {
                for (int i = start, k = destinationIndex; k < length; i++,k++)
                {
                    matrix2[k,dimension2] = matrix1[i,dimension1];
                }    
            }
        }
    }
}