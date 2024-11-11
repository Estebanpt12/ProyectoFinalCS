namespace ProyectoFinalCS.Algoritmos;

using System;
using System.Collections.Generic;

class StrassenNaiv : IMatrixMultiplication {

    public static void Print(string display, int[, ] matrix,int start_row, int start_column, int end_row,int end_column)
    {
        Console.WriteLine(display + " =>\n");
        for (int i = start_row; i <= end_row; i++) {
        for (int j = start_column; j <= end_column; j++) {
            Console.Write(matrix[i, j]+" ");
        }
        Console.WriteLine("");
        }
        Console.WriteLine("");
    }

    public static void Add_matrix(int[, ] matrix_A,int[, ] matrix_B,int[, ] matrix_C, int split_index)
    {
        for (int i = 0; i < split_index; i++){
        for (int j = 0; j < split_index; j++){
            matrix_C[i, j] = matrix_A[i, j] + matrix_B[i, j];
        }
        }
    }

    public static void InitWithZeros(int[, ] a, int r, int c){
        for(int i=0;i<r;i++){
        for(int j=0;j<c;j++){
            a[i, j]=0;
        }
        }
    }

    public int[, ] Multiply(int[, ] matrix_A,int[, ] matrix_B)
    {
        int col_1 = matrix_A.GetLength(1);
        int row_1 = matrix_A.GetLength(0);
        int col_2 = matrix_B.GetLength(1);
        int row_2 = matrix_B.GetLength(0);

        if (col_1 != row_2) {
        Console.WriteLine("\nError: The number of columns in Matrix A must be equal to the number of rows in Matrix B\n");
        int[, ] temp = new int[1, 1];
        temp[0, 0]=0;
        return temp;
        }

        int[] result_matrix_row = new int[col_2];
        Array.Fill(result_matrix_row,0);
        int[, ] result_matrix = new int[row_1, col_2];
        InitWithZeros(result_matrix,row_1,col_2);

        if (col_1 == 1){
        result_matrix[0, 0] = matrix_A[0, 0] * matrix_B[0, 0]; 
        }else {
        int split_index = col_1 / 2;

        int[] row_vector = new int[split_index];
        Array.Fill(row_vector,0);

        int[, ] result_matrix_00 = new int[split_index, split_index];
        int[, ] result_matrix_01 = new int[split_index, split_index];
        int[, ] result_matrix_10 = new int[split_index, split_index];
        int[, ] result_matrix_11 = new int[split_index, split_index];
        InitWithZeros(result_matrix_00,split_index,split_index);
        InitWithZeros(result_matrix_01,split_index,split_index);
        InitWithZeros(result_matrix_10,split_index,split_index);
        InitWithZeros(result_matrix_11,split_index,split_index);

        int[, ] a00 = new int[split_index, split_index];
        int[, ] a01 = new int[split_index, split_index];
        int[, ] a10 = new int[split_index, split_index];
        int[, ] a11 = new int[split_index, split_index];
        int[, ] b00 = new int[split_index, split_index];
        int[, ] b01 = new int[split_index, split_index];
        int[, ] b10 = new int[split_index, split_index];
        int[, ] b11 = new int[split_index, split_index];
        InitWithZeros(a00,split_index,split_index);
        InitWithZeros(a01,split_index,split_index);
        InitWithZeros(a10,split_index,split_index);
        InitWithZeros(a11,split_index,split_index);
        InitWithZeros(b00,split_index,split_index);
        InitWithZeros(b01,split_index,split_index);
        InitWithZeros(b10,split_index,split_index);
        InitWithZeros(b11,split_index,split_index);


        for (int i = 0; i < split_index; i++){
            for (int j = 0; j < split_index; j++) {
            a00[i, j] = matrix_A[i, j];
            a01[i, j] = matrix_A[i, j + split_index];
            a10[i, j] = matrix_A[split_index + i, j];
            a11[i, j] = matrix_A[i + split_index, j + split_index];
            b00[i, j] = matrix_B[i, j];
            b01[i, j] = matrix_B[i, j + split_index];
            b10[i, j] = matrix_B[split_index + i, j];
            b11[i, j] = matrix_B[i + split_index, j + split_index];
            }
        }

        Add_matrix(Multiply(a00, b00),Multiply(a01, b10),result_matrix_00, split_index);
        Add_matrix(Multiply(a00, b01),Multiply(a01, b11),result_matrix_01, split_index);
        Add_matrix(Multiply(a10, b00),Multiply(a11, b10),result_matrix_10, split_index);
        Add_matrix(Multiply(a10, b01),Multiply(a11, b11),result_matrix_11, split_index);

        for (int i = 0; i < split_index; i++){
            for (int j = 0; j < split_index; j++) {
            result_matrix[i, j] = result_matrix_00[i, j];
            result_matrix[i, j + split_index] = result_matrix_01[i, j];
            result_matrix[split_index + i, j] = result_matrix_10[i, j];
            result_matrix[i + split_index, j + split_index] = result_matrix_11[i, j];
            }
        }
        }
        return result_matrix;
    }
}

// Time Complexity: O(n^3)
//This code is contributed by phasing17
