using System;

namespace _01_text_rpg_hardcoding
{
    class Program
    {
        enum ClassType
        {
            None = 0,
            Knight = 1,
            Archer = 2,
            Mage = 3
        }
        struct Player
        {
            public int hp;
            public int attack;
        }
        enum MonsterType
        {
            None = 0,
            Slime = 1,
            Orc = 2,
            Skeleton = 3
        }
        struct Monster
        {
            public int hp;
            public int attack;
        }
        static void CreateRandomMonster(out Monster monster)
        {
            Random rand = new Random();
            int randMonster = rand.Next(1, 4);

            switch (randMonster)
            {
                case (int)MonsterType.Slime:
                    Console.WriteLine("슬라임 등장!");
                    monster.hp = 15;
                    monster.attack = 2;
                    break;
                case (int)MonsterType.Orc:
                    Console.WriteLine("오크 등장!");
                    monster.hp = 30;
                    monster.attack = 8;
                    break;
                case (int)MonsterType.Skeleton:
                    Console.WriteLine("스켈레톤 등장!");
                    monster.hp = 45;
                    monster.attack = 18;
                    break;
                default:
                    monster.hp = 0;
                    monster.attack = 0;
                    break;
            }
        }
        static ClassType ChooseClass()
        {
            ClassType choice = ClassType.None;

            Console.WriteLine("직업을 선택하세요!");
            Console.WriteLine("[1]기사");
            Console.WriteLine("[2]궁수");
            Console.WriteLine("[3]법사");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    choice = ClassType.Knight;
                    break;
                case "2":
                    choice = ClassType.Archer;
                    break;
                case "3":
                    choice = ClassType.Mage;
                    break;
            }

            return choice;
        }
        static void CreatePlayer(ClassType choice, out Player player)
        {
            switch (choice)
            {
                case ClassType.Knight:
                    player.hp = 100;
                    player.attack = 10;
                    break;
                case ClassType.Archer:
                    player.hp = 75;
                    player.attack = 12;
                    break;
                case ClassType.Mage:
                    player.hp = 50;
                    player.attack = 15;
                    break;
                default:
                    player.hp = 0;
                    player.attack = 0;
                    break;
            }
        }
        static void Battle(ref Player player, ref Monster monster)
        {
            while (true)
            {
                // 플레이어의 선공
                monster.hp -= player.attack;
                if (monster.hp <= 0)
                {
                    Console.WriteLine("몬스터가 쓰러졌습니다.");
                    Console.WriteLine($"남은 체력: {player.hp}");
                    break;
                }
                //몬스터의 반격
                player.hp -= monster.attack;
                if (player.hp <= 0)
                {
                    Console.WriteLine("패배했습니다.");
                    break;
                }
            }
        }
        static void EnterField(ref Player player)
        {
            while (true)
            {
                Console.WriteLine("필드에 접속했습니다!");

                // 랜덤으로 1~3 몬스터 중 하나를 리스폰
                Monster monster;
                CreateRandomMonster(out monster);

                // [1] 전투 모드로 돌입
                // [2] 일정 확률로 마을로 도망
                Console.WriteLine("[1] 전투 모드로 돌입");
                Console.WriteLine("[2] 일정 확률로 마을로 도망");

                string input = Console.ReadLine();
                if (input == "1")
                {
                    Battle(ref player, ref monster);
                }
                else if (input == "2")
                {
                    // 33% 확률로 도망
                    Random rand = new Random();
                    int escapeChance = rand.Next(0, 101);
                    if (escapeChance <= 33)
                    {
                        Console.WriteLine("도망치는데 성공했습니다!");
                        break;
                    }
                    else
                    {
                        Battle(ref player, ref monster);
                    }
                }
            }
        }
        static void EnterGame(ref Player player)
        {
            while (true)
            {
                Console.WriteLine("마을에 접속했습니다.");
                Console.WriteLine("[1] 필드로 간다");
                Console.WriteLine("[2] 로비로 돌아가기");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        EnterField(ref player);
                        break;
                    case "2":
                        return;
                }
            }

        }
        static void Main(string[] args)
        {
            while (true)
            {
                // 직업 선택
                ClassType choice = ChooseClass();
                if (choice == ClassType.None)
                    continue;

                //캐릭터 생성
                Player player;
                CreatePlayer(choice, out player);
                //게임 접속
                EnterGame(ref player);
            }

        }
    }
}