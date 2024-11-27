using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mayın_tarlası
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = 5; // Oyun alanı satır sayısı
            int cols = 5; // Oyun alanı sütun sayısı
            int mineCount = 5; // Mayın sayısı

            char[,] board = new char[rows, cols]; // Görünen tahta
            char[,] mines = new char[rows, cols]; // Mayınların olduğu tahta
            bool isGameOver = false;

            // Tahtaları başlat
            InitializeBoard(board, '-');
            InitializeBoard(mines, '-');
            PlaceMines(mines, mineCount);

            // Oyun döngüsü
            while (!isGameOver)
            {
                Console.Clear();
                PrintBoard(board);

                Console.Write("Bir hücre seçin (satır ve sütun): ");
                string input = Console.ReadLine();
                string[] parts = input.Split(' ');

                if (parts.Length != 2 || !int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col) || row < 0 || row >= rows || col < 0 || col >= cols)
                {
                    Console.WriteLine("Geçersiz giriş. Lütfen tekrar deneyin.");
                    continue;
                }

                if (mines[row, col] == '*')
                {
                    Console.Clear();
                    PrintBoard(mines);
                    Console.WriteLine("Mayına bastınız! Oyun bitti.");
                    isGameOver = true;
                }
                else
                {
                    board[row, col] = CountAdjacentMines(mines, row, col);
                    if (CheckWin(board, mines))
                    {
                        Console.Clear();
                        PrintBoard(board);
                        Console.WriteLine("Tebrikler! Tüm mayınları buldunuz.");
                        isGameOver = true;
                    }
                }
            }
        }

        static void InitializeBoard(char[,] board, char fill)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = fill;
                }
            }
        }

        static void PlaceMines(char[,] mines, int mineCount)
        {
            Random rand = new Random();
            int placedMines = 0;

            while (placedMines < mineCount)
            {
                int row = rand.Next(mines.GetLength(0));
                int col = rand.Next(mines.GetLength(1));

                if (mines[row, col] != '*')
                {
                    mines[row, col] = '*';
                    placedMines++;
                }
            }
        }

        static void PrintBoard(char[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        static char CountAdjacentMines(char[,] mines, int row, int col)
        {
            int count = 0;

            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < mines.GetLength(0) && j >= 0 && j < mines.GetLength(1) && mines[i, j] == '*')
                    {
                        count++;
                    }
                }
            }

            return count > 0 ? count.ToString()[0] : '0';
        }

        static bool CheckWin(char[,] board, char[,] mines)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == '-' && mines[i, j] != '*')
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}

