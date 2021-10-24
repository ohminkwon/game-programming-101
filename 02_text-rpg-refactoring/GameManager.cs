using System;
using System.Collections.Generic;
using System.Text;

namespace _02_text_rpg_refactoring
{
    public enum WorldMode
    {
        None,
        Lobby,
        Town,
        Field
    }
    class GameManager
    {
        private Random rand = new Random();

        private WorldMode world = WorldMode.Lobby;
        private Player player = null;
        private Monster monster = null;

        public void Process()
        {
            switch (world)
            {
                case WorldMode.Lobby:
                    ProcessLobby();
                    break;
                case WorldMode.Town:
                    ProcessTown();
                    break;
                case WorldMode.Field:
                    ProcessField();
                    break;
            }
        }
        public void ProcessLobby()
        {
            Console.WriteLine("직업을 선택하세요");
            Console.WriteLine("[1] 기사");
            Console.WriteLine("[2] 궁수");
            Console.WriteLine("[3] 법사");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    player = new Knight();
                    world = WorldMode.Town;
                    break;
                case "2":
                    player = new Archer();
                    world = WorldMode.Town;
                    break;
                case "3":
                    player = new Mage();
                    world = WorldMode.Town;
                    break;
            }
        }
        public void ProcessTown()
        {
            Console.WriteLine("마을에 입장했습니다.");
            Console.WriteLine("[1] 필드로 가기");
            Console.WriteLine("[2] 로비로 돌아가기");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    world = WorldMode.Field;
                    break;
                case "2":
                    world = WorldMode.Lobby;
                    break;
            }
        }
        public void ProcessField()
        {
            Console.WriteLine("필드에 입장했습니다.\n");

            CreateRandomMonster();

            Console.WriteLine("[1] 싸우기");
            Console.WriteLine("[2] 일정 확률로 마을 돌아가기");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ProcessFight();
                    break;
                case "2":
                    TryEscape();
                    break;
            }

        }
        private void TryEscape()
        {
            int escapeChance = rand.Next(1, 101);
            if (escapeChance <= 33)
            {
                Console.WriteLine("도망 성공!!!");
                world = WorldMode.Town;
            }
            else
            {
                Console.WriteLine("도망 실패!");
                ProcessFight();
            }
        }
        private void ProcessFight()
        {
            Console.WriteLine("전투 시작!!");
            while (true)
            {
                int damage = player.GetAttack();
                monster.OnDamaged(damage);
                if (monster.IsDead())
                {
                    Console.WriteLine(" 승리!");
                    Console.WriteLine($"남은체력: {player.GetHp()}\n");
                    break;
                }

                damage = monster.GetAttack();
                player.OnDamaged(damage);
                if (player.IsDead())
                {
                    Console.WriteLine(" 사망!");
                    Console.WriteLine("로비로 이동합니다.\n");
                    world = WorldMode.Lobby;
                    break;
                }
            }
        }
        private void CreateRandomMonster()
        {
            int monsterTypeChance = rand.Next(0, 3);
            switch (monsterTypeChance)
            {
                case 0:
                    monster = new Slime();
                    Console.WriteLine("슬라임이 발견되었습니다.");
                    break;
                case 1:
                    monster = new Orc();
                    Console.WriteLine("오크가 발견되었습니다.");
                    break;
                case 2:
                    monster = new Skeleton();
                    Console.WriteLine("해골병사가 발견되었습니다.");
                    break;
            }
        }
    }
}
