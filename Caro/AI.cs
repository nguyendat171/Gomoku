using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace Caro
{
    class AI
    {
        public int[,] Array = new int[12, 12];//mảng đánh dấu vị trí đánh cờ

        public long[] ArrayAttack = new long[7] { 0, 9, 54, 162, 1458, 13112, 118008 };//mảng điểm tấn công

        public long[] ArrayDefense = new long[7] { 0, 3, 27, 99, 729, 6561, 59049 };//mảng điểm phòng ngự

        public Point Find_moveoptimal()//tìm vị trí tối ưu nhất trên bàn cờ để máy đánh
        {
            Point point = new Point();
            long Max_Value = 0;
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (Array[i, j] == 0)
                    {
                        long ResultAttack = Attack_Row(i, j) + Attack_Column(i, j) + Attack_Diagonal_Up(i, j) + Attack_Diagonal_Down(i, j);//diem tan cong
                        long ResultDefense = Defense_Row(i, j) + Defense_Column(i, j) + Defense_Diagonal_Up(i, j) + Defense_Diagonal_Down(i, j);//diem phong ngu
                        long Temp_Value;
                        if (ResultAttack > ResultDefense)
                        {
                            Temp_Value = ResultAttack;
                        }
                        else
                            Temp_Value = ResultDefense;
                        if (Temp_Value > Max_Value)
                        {
                            Max_Value = Temp_Value;
                            point.X = i;
                            point.Y = j;
                        }
                    }
                }
            }
            return point;
        }

        public long Defense_Diagonal_Down(int row, int column)//điểm phòng ngự trên đường chéo xuống
        {
            long result = 0;
            int Count_us = 0;//so luong quan ta
            int Count_competitor = 0;//so luong quan dich
            for (int count = 1; count < 6 && column + count < 12 && row + count < 12; count++)
            {
                if (Array[row + count, column + count] == 3)//neu la quan ta
                {
                    Count_us++;
                    break;
                }
                else if (Array[row + count, column + count] == 2)//neu la quan dich 
                {
                    Count_competitor++;

                }
                else
                    break;

            }
            for (int count = 1; count < 6 && column - count >= 0 && row - count >= 0; count++)
            {
                if (Array[row - count, column - count] == 3)//neu la quan ta
                {
                    Count_us++;
                    break;

                }
                else if (Array[row - count, column - count] == 2)//neu la quan dich 
                {
                    Count_competitor++;

                }
                else
                    break;

            }
            if (Count_us == 2)//nếu ta chặn 2 đầu
            {
                return 0;
            }

            result += ArrayDefense[Count_competitor];//diem phong ngu
            return result;
        }

        public long Defense_Diagonal_Up(int row, int column)//điểm phòng ngự trên đường chéo lên
        {
            long result = 0;
            int Count_us = 0;//so luong quan ta
            int Count_competitor = 0;//so luong quan dich
            for (int count = 1; count < 6 && column - count >= 0 && row + count < 12; count++)
            {
                if (Array[row + count, column - count] == 3)//neu la quan ta
                {
                    Count_us++;
                    break;
                }
                else if (Array[row + count, column - count] == 2)//neu la quan dich 
                {
                    Count_competitor++;

                }
                else
                    break;

            }
            for (int count = 1; count < 6 && column + count < 12 && row - count >= 0; count++)
            {
                if (Array[row - count, column + count] == 3)//neu la quan ta
                {
                    Count_us++;
                    break;
                }
                else if (Array[row - count, column + count] == 2)//neu la quan dich 
                {
                    Count_competitor++;

                }
                else
                    break;

            }
            if (Count_us == 2)//nếu ta chặn 2 đầu
            {
                return 0;
            }

            result += ArrayDefense[Count_competitor];//diem phong ngu
            return result;
        }

        public long Defense_Column(int row, int column)//điểm phòng ngự trên 1 cột
        {
            long result = 0;
            int Count_us = 0;//so luong quan ta
            int Count_competitor = 0;//so luong quan dich
            for (int count = 1; count < 6 && row + count < 12; count++)
            {
                if (Array[row + count, column] == 3)//neu la quan ta
                {
                    Count_us++;
                    break;
                }
                else if (Array[row + count, column] == 2)//neu la quan dich 
                {
                    Count_competitor++;

                }
                else
                    break;

            }
            for (int count = 1; count < 6 && row - count >= 0; count++)
            {
                if (Array[row - count, column] == 3)//neu la quan ta
                {
                    Count_us++;
                    break;
                }
                else if (Array[row - count, column] == 2)//neu la quan dich 
                {
                    Count_competitor++;

                }
                else
                    break;

            }
            if (Count_us == 2)//nếu ta chặn 2 đầu
            {
                return 0;
            }

            result += ArrayDefense[Count_competitor];//diem phong ngu
            return result;
        }

        public long Defense_Row(int row, int column)//điểm phòng ngự trên hàng
        {
            long result = 0;
            int Count_us = 0;//so luong quan ta
            int Count_competitor = 0;//so luong quan dich
            for (int count = 1; count < 6 && column + count < 12; count++)
            {
                if (Array[row, column + count] == 3)//neu la quan ta
                {
                    Count_us++;
                    break;
                }
                else if (Array[row, column + count] == 2)//neu la quan dich 
                {
                    Count_competitor++;

                }
                else
                    break;

            }
            for (int count = 1; count < 6 && column - count >= 0; count++)
            {
                if (Array[row, column - count] == 3)//neu la quan ta
                {
                    Count_us++;
                    break;
                }
                else if (Array[row, column - count] == 2)//neu la quan dich 
                {
                    Count_competitor++;

                }
                else
                    break;

            }
            if (Count_us == 2)//nếu ta chặn 2 đầu
            {
                return 0;
            }
            result += ArrayDefense[Count_competitor];//diem phong ngu
            return result;
        }

        public long Attack_Diagonal_Down(int row, int column)//điểm tấn công trên đường chéo xuống
        {
            long result = 0;
            int Count_us = 0;//so luong quan ta
            int Count_competitor = 0;//so luong quan dich
            for (int count = 1; count < 6 && column + count < 12 && row + count < 12; count++)
            {
                if (Array[row + count, column + count] == 3)//neu la quan ta
                {
                    Count_us++;
                }
                else if (Array[row + count, column + count] == 2)//neu la quan dich 
                {
                    Count_competitor++;
                    break;
                }
                else
                    break;

            }
            for (int count = 1; count < 6 && column - count >= 0 && row - count >= 0; count++)
            {
                if (Array[row - count, column - count] == 3)//neu la quan ta
                {
                    Count_us++;
                }
                else if (Array[row - count, column - count] == 2)//neu la quan dich 
                {
                    Count_competitor++;
                    break;
                }
                else
                    break;

            }
            if (Count_competitor == 2)//nếu bị chặn 2 đầu
            {
                return 0;
            }
            result += ArrayAttack[Count_us];//diem tan cong
            result -= ArrayDefense[Count_competitor + 1] * 2;//diem phong ngu
            return result;
        }

        public long Attack_Diagonal_Up(int row, int column)//điểm tấn công đường chéo lên
        {
            long result = 0;
            int Count_us = 0;//so luong quan ta
            int Count_competitor = 0;//so luong quan dich
            for (int count = 1; count < 6 && column - count >= 0 && row + count < 12; count++)
            {
                if (Array[row + count, column - count] == 3)//neu la quan ta
                {
                    Count_us++;
                }
                else if (Array[row + count, column - count] == 2)//neu la quan dich 
                {
                    Count_competitor++;
                    break;
                }
                else
                    break;

            }
            for (int count = 1; count < 6 && column + count < 12 && row - count >= 0; count++)
            {
                if (Array[row - count, column + count] == 3)//neu la quan ta
                {
                    Count_us++;
                }
                else if (Array[row - count, column + count] == 2)//neu la quan dich 
                {
                    Count_competitor++;
                    break;
                }
                else
                    break;

            }
            if (Count_competitor == 2)//nếu bị chặn 2 đầu
            {
                return 0;
            }
            result += ArrayAttack[Count_us];//diem tan cong
            result -= ArrayDefense[Count_competitor + 1] * 2;//diem phong ngu
            return result;
        }

        public long Attack_Row(int row, int column)//điểm tấn công trên dòng
        {
            long result = 0;
            int Count_us = 0;//so luong quan ta
            int Count_competitor = 0;//so luong quan dich
            for (int count = 1; count < 6 && column + count < 12; count++)
            {
                if (Array[row, column + count] == 3)//neu la quan ta
                {
                    Count_us++;
                }
                else if (Array[row, column + count] == 2)//neu la quan dich 
                {
                    Count_competitor++;
                    break;
                }
                else
                    break;

            }
            for (int count = 1; count < 6 && column - count >= 0; count++)
            {
                if (Array[row, column - count] == 3)//neu la quan ta
                {
                    Count_us++;
                }
                else if (Array[row, column - count] == 2)//neu la quan dich 
                {
                    Count_competitor++;
                    break;
                }
                else
                    break;

            }
            if (Count_competitor == 2)//nếu bị chặn 2 đầu
            {
                return 0;
            }
            result += ArrayAttack[Count_us];//diem tan cong
            result -= ArrayDefense[Count_competitor + 1] * 2;//diem phong ngu
            return result;
        }

        public long Attack_Column(int row, int column)//điểm tấn công trên cột
        {
            long result = 0;
            int Count_us = 0;//so luong quan ta
            int Count_competitor = 0;//so luong quan dich
            for (int count = 1; count < 6 && row + count < 12; count++)
            {
                if (Array[row + count, column] == 3)//neu la quan ta
                {
                    Count_us++;
                }
                else if (Array[row + count, column] == 2)//neu la quan dich 
                {
                    Count_competitor++;
                    break;
                }
                else
                    break;

            }
            for (int count = 1; count < 6 && row - count >= 0; count++)
            {
                if (Array[row - count, column] == 3)//neu la quan ta
                {
                    Count_us++;
                }
                else if (Array[row - count, column] == 2)//neu la quan dich 
                {
                    Count_competitor++;
                    break;
                }
                else
                    break;

            }
            if (Count_competitor == 2)//nếu bị chặn 2 đầu
            {
                return 0;
            }
            result += ArrayAttack[Count_us];//diem tan cong
            result -= ArrayDefense[Count_competitor + 1] * 2;//diem phong ngu
            return result;
        }
    }
}
