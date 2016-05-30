using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro
{
    class Check
    {
        //KIỂM TRA 
        public bool five_row(int[,] array, int row, int column)//kiem tra tren 5 vi tri tren 1 hang
        {
            int count = 1;
            int temp_column = column - 1;
            if (array[row, column] == 0)
                return false;
            while (temp_column >= 0 && array[row, temp_column] == array[row, column])
            {
                count++;
                temp_column--;
                if (count == 5)
                {
                    return true;
                }
            }
            temp_column = column + 1;
            while (temp_column < 12 && array[row, temp_column] == array[row, column])
            {
                count++;
                temp_column++;
                if (count == 5)
                {
                    return true;
                }
            }
            return false;

        }


        public bool five_column(int[,] array, int row, int column)//kiem tra tren 5 vi tri tren 1 cot
        {
            int count = 1;
            int temp_row = row - 1;
            if (array[row, column] == 0)
                return false;
            while (temp_row >= 0 && array[temp_row, column] == array[row, column])
            {
                count++;
                temp_row--;
                if (count == 5)
                {
                    return true;
                }
            }
            temp_row = row + 1;
            while (temp_row < 12 && array[temp_row, column] == array[row, column])
            {
                count++;
                temp_row++;
                if (count == 5)
                {
                    return true;
                }
            }
            return false;

        }

        public bool five_diagonal_down(int[,] array, int row, int column)//đường chéo xuống
        {
            int count = 1;
            int temp_row = row + 1;
            int temp_column = column + 1;
            if (array[row, column] == 0)
                return false;
            while (temp_row < 12 && temp_column < 12 && array[temp_row, temp_column] == array[row, column])
            {
                count++;
                temp_row++;
                temp_column++;
                if (count == 5)
                {
                    return true;
                }
            }
            temp_row = row - 1;
            temp_column = column - 1;
            while (temp_row >= 0 && temp_column >= 0 && array[temp_row, temp_column] == array[row, column])//kiem tra 5 vi tri tren duong cheo truong hop 1
            {
                count++;
                temp_row--;
                temp_column--;
                if (count == 5)
                {
                    return true;
                }
            }
            return false;
        }


        public bool five_diagonal_up(int[,] array, int row, int column)//đường chéo lên
        {
            int count = 1;
            int temp_row = row + 1;
            int temp_column = column - 1;
            if (array[row, column] == 0)
                return false;
            while (temp_row < 12 && temp_column > 0 && array[temp_row, temp_column] == array[row, column])
            {
                count++;
                temp_row++;
                temp_column--;
                if (count == 5)
                {
                    return true;
                }
            }
            temp_row = row - 1;
            temp_column = column + 1;
            while (temp_row >= 0 && temp_column < 12 && array[temp_row, temp_column] == array[row, column])
            {
                count++;
                temp_row--;
                temp_column++;
                if (count == 5)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
