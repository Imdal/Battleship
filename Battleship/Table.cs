using System;
using System.Diagnostics;

namespace Battleship
{
    public class Table
    {

        public int[,] table = new int[10,10];
        public Ship[] ships = new Ship[5];


        public void traceShips()
        {
            for(int i=0;i<5;i++)
            {
                Trace.WriteLine("\n"+i+" (" + ships[i].beginX + "," + ships[i].beginY + ")(" + ships[i].endX + "," + ships[i].endY + ")");
            }
        }
        public Table()
        {
            initTable();
        }

        public void initTable()
        {
            for(int i=0;i<10;i++)
            {
                for(int j=0;j<10;j++)
                {
                    table[i, j] = 0;
                }
            }
            for(int i=0;i<5;i++)
            {
                ships[i].exists = false;
                ships[i].shunken = false;
            }
        }

        private void shunkenShip()
        {
            bool isShunken;
            for (int i = 0; i < 5; i++)
            {
                isShunken = true;
                if (ships[i].shunken)
                    continue;
                if (ships[i].beginY == ships[i].endY)
                {
                    for (int j = ships[i].beginX; j <= ships[i].endX; j++)
                    {
                        if (table[j, ships[i].beginY] != 3)
                        {
                            isShunken = false;
                        }
                    }
                }
                else if (ships[i].beginX == ships[i].endX)
                {
                    for (int j = ships[i].beginY; j <= ships[i].endY; j++)
                    {
                        if (table[ships[i].beginX, j] != 3)
                        {
                            isShunken = false;
                            break;
                        }
                    }
                }
                if (isShunken)
                {
                    ships[i].shunken = true;
                    return;
                }
            }
        }

        public void randomPlaceShips()
        {
            Random random = new Random();
            int row;
            int col;
            bool horizontal;
            for(int i=4;i>=0;i--)
            {
                horizontal = random.Next() % 2 == 0;
                while (true)
                {
                    row = random.Next() % 10;
                    col = random.Next() % 10;
                    if(row<10-i && horizontal)
                    {
                        if(placeShip(row,col,row+i,col))
                        {
                            break;
                        }
                    }
                    else if(col<10-i && !horizontal)
                    {
                        if(placeShip(row,col,row,col+i))
                        {
                            break;
                        }
                    }
                }
            }
        }

        public bool placeShip(int row1, int col1, int row2, int col2)
        {
            if (row1 == row2 && Math.Abs(col1 - col2) < 5)
            {
                if (ships[Math.Abs(col1 - col2)].exists == false)
                {
                    for (int i = Math.Min(col1, col2); i <= Math.Max(col1, col2); i++)
                    {
                        for(int x=0;x<3;x++)
                        {
                            for(int y=0;y<3;y++)
                            {
                                if (row1+1 <= 9 && row1-1>=0 && i-1>=0 && i+1<=9 && table[row1 - 1 + x, i - 1 + y] == 1)
                                    return false;
                            }
                        }
                    }
                    for (int i = Math.Min(col1, col2); i <= Math.Max(col1, col2); i++)
                    {
                        table[row1, i] = 1;
                    }
                    ships[Math.Abs(col1 - col2)].setPlace(true, row1, col1, row2, col2);
                    return true;
                }
            }
            else if (col1 == col2 && Math.Abs(row1 - row2) < 5)
            {
                if (ships[Math.Abs(row1 - row2)].exists == false)
                {
                    for (int i = Math.Min(row1, row2); i <= Math.Max(row1, row2); i++)
                    {
                        for (int x = 0; x < 3; x++)
                        {
                            for (int y = 0; y < 3; y++)
                            {
                                if (i - 1 >= 0 && i + 1 <= 9 && col1 - 1 >= 0 && col1 + 1 <= 9 && table[i - 1 + x, col1 - 1 + y] == 1)
                                    return false;
                            }
                        }
                    }
                    for (int i = Math.Min(row1, row2); i <= Math.Max(row1, row2); i++)
                    {
                        table[i, col1] = 1;
                    }
                    ships[Math.Abs(row1 - row2)].setPlace(true, row1, col1, row2, col2);
                    return true;
                }
            }
            return false;
        }

        public bool allShipsPlaced()
        {
            for(int i=0;i<5;i++)
            {
                if(ships[i].exists==false)
                {
                    return false;
                }
            }
            return true;
        }

        public void randomShoot()
        {
            for(int i=0;i<10;i++)
            {
                for(int j=0;j<10;j++)
                {
                    if(i!=9 && table[i,j]==3 && table[i+1,j]==3)
                    {
                        if (i!=0 && shoot(i - 1, j))
                            return;
                        else if (i<8 && shoot(i + 2, j))
                            return;
                    }
                    if(j!=9 && table[i,j]==3 && table[i,j+1]==3)
                    {
                        if (j!=0 && shoot(i, j - 1))
                            return;
                        else if (j<8 && shoot(i, j + 2))
                            return;
                    }
                    if(table[i,j]==3)
                    {
                        if (i!=0 && table[i - 1, j] != 2 && table[i - 1, j] != 3)
                        {
                            shoot(i - 1, j);
                            return;
                        }
                        else if (i!=9 && table[i + 1, j] != 2 && table[i + 1, j] != 3)
                        {
                            shoot(i + 1, j);
                            return;
                        }
                        else if (j!=0 && table[i, j - 1] != 2 && table[i, j - 1] != 3)
                        {
                            shoot(i, j - 1);
                            return;
                        }
                        else if (j!=9 && table[i, j + 1] != 2 && table[i, j + 1] != 3)
                        {
                            shoot(i, j + 1);
                            return;
                        }
                    }
                }
            }
            Random random = new Random();
            int row;
            int col;
            do
            {
                row = random.Next() % 10;
                col = random.Next() % 10;
            } while (!this.shoot(row, col));
        }

        public bool shoot(int row, int col)
        {
            if (table[row, col] == 0)
            {
                table[row, col] = 2;
                return true;
            }
            else if (table[row, col] == 1)
            {
                table[row, col] = 3;
                shunkenShip();
                return true;
            }
            else 
                return false;
        }

        public bool allShooted()
        {
            for(int i=0;i<5;i++)
            {
                if (!ships[i].shunken)
                    return false;
                    
            }
            return true;
        }

        public int numberOfShipsShunken()
        {
            int sum = 0;
            for (int i = 0; i < 5; i++)
            {
                if (ships[i].shunken)
                    sum++;

            }
            return sum;
        }
        
    }
}
