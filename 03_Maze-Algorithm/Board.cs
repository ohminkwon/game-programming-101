using System;
using System.Collections.Generic;
using System.Text;

namespace _03_Maze_Algorithm
{
    // 맵 생성을 위한 방식 후보들
    // 어떤 방식이 효율적인지 장/단점을 생각해보자!!

    /*
    public int[] _data = new int[25]; // 배열 -> **지형 지물이 게임 중간에 변화하지 않기에**
    public List<int> _data2 = new List<int>(); // 동적 배열        
    public LinkedList<int> _data3 = new LinkedList<int>(); // 연결 리스트
    */
    class Board
    {
        const char CIRCLE = '\u25cf';
        public TileType[,] Tile { get; private set; }
        public int Size { get; private set; }
        public int DestY { get; set; }
        public int DestX { get; set; }

        Player _player;

        public enum TileType
        {
            Empty,
            Wall
        }
        public void Initialize(int size, Player player)
        {
            if (size % 2 == 0) return;

            _player = player;

            Tile = new TileType[size, size];
            Size = size;

            DestY = size - 2;
            DestX = size - 2;

            // 참조: Mazes for Programmers
            // GenerateByBinaryTree();
            GenerateBySideWinder();
        }

        void GenerateByBinaryTree()
        {
            // 맵의 길을 다막는 기본 세팅, 
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                    {
                        Tile[y, x] = TileType.Wall;
                    }
                    else
                    {
                        Tile[y, x] = TileType.Empty;
                    }
                }
            }
            // 랜덤으로 우측 혹은 아래로 길을 뚫는 작업
            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0) continue;

                    // 외곽 처리
                    if (y == Size - 2 && x == Size - 2) continue;
                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }
                    // 50% 확률로 우측 or 아래 길 생성 (BinaryTree)
                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                    }
                    else
                    {
                        Tile[y + 1, x] = TileType.Empty;
                    }
                }
            }
        }
        void GenerateBySideWinder()
        {
            // 맵의 길을 다막는 기본 세팅, 
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                    {
                        Tile[y, x] = TileType.Wall;
                    }
                    else
                    {
                        Tile[y, x] = TileType.Empty;
                    }
                }
            }
            // 랜덤으로 우측 혹은 아래로 길을 뚫는 작업
            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {
                int count = 1;
                for (int x = 0; x < Size; x++)
                {
                    // 외곽 처리
                    if (x % 2 == 0 || y % 2 == 0) continue;
                    if (y == Size - 2 && x == Size - 2) continue;
                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    // 50% 확률로 우측과 연계된 아래 길 생성 (SideWinder)
                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        count++;
                    }
                    else
                    {
                        int randomIndex = rand.Next(0, count);
                        Tile[y + 1, x - randomIndex * 2] = TileType.Empty;
                        count = 1;
                    }
                }
            }
        }
        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    // 플레이어 좌표를 파란색으로 표시
                    if (y == _player.PosY && x == _player.PosX)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if (y == DestY && x == DestX)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    // 벽은 빨간색, 길은 녹색으로 표시
                    else
                    {
                        Console.ForegroundColor = GetTileColor(Tile[y, x]);
                    }
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor;
        }
        ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Green;
            }
        }
    }
}
