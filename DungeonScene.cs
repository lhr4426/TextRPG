using System;

public class DungeonScene : GameScene
{
	List<Dungeon> dungeons = new List<Dungeon>();

	List<int> YouCanUseSkillThisTurn;

    Random random = new Random();
    int turn;

    public DungeonScene()
	{
		SettingDungeons();
		
    }

	public void SettingDungeons()
	{
		Dungeon dungeon1 = new Dungeon("근처 숲", "귀여운 슬라임이 나오는 작은 숲입니다.", 100, 20, 0);
        Dungeon dungeon2 = new Dungeon("작은 동굴", "들어가면 목소리가 울립니다.", 150, 30, 5);
        Dungeon dungeon3 = new Dungeon("바닷가", "소문으로는 무서운 세이렌이 나온다고 합니다.", 200, 40, 20);
        Dungeon dungeon4 = new Dungeon("마리아나 해구", "에구궁...", 250, 50, 40);


        Monster slime = new Monster("슬라임", 5, 5, 10);
		Monster bat = new Monster("박쥐", 10, 10, 20);
        Monster siren = new Monster("세이렌", 15, 10, 20);
		Monster spirit = new Monster("물의 정령", 20, 20, 100);
		

        dungeon1.monsters.Add(slime);
        dungeon1.monsters.Add(slime.MakeClone());
        dungeon1.monsters.Add(slime.MakeClone());
        dungeon2.monsters.Add(bat);
        dungeon2.monsters.Add(bat.MakeClone());
		dungeon2.monsters.Add(bat.MakeClone());
        dungeon2.monsters.Add(bat.MakeClone());
        dungeon3.monsters.Add(siren);
        dungeon3.monsters.Add(siren.MakeClone());
        dungeon4.monsters.Add(siren.MakeClone());
        dungeon4.monsters.Add(siren.MakeClone());
        dungeon4.monsters.Add(siren.MakeClone());
        dungeon4.monsters.Add(spirit);

        dungeons.Add(dungeon1);
		dungeons.Add(dungeon2);
		dungeons.Add(dungeon3);
		dungeons.Add(dungeon4);
    }

	public void ResetDungeon(Dungeon dungeon)
	{
		foreach(var monster in dungeon.monsters)
		{
			monster.HP = monster.MaxHP;
        }
    }

    public override IGameScene? StartScene()
	{
		Console.Clear();
        YouCanUseSkillThisTurn = new List<int>();
        turn = 1;
        for (int i = 0; i < GameManager.instance.playerData.Skills.Count; i++)
        {
            YouCanUseSkillThisTurn.Add(turn);
        }
        Console.WriteLine("던전 입구에 입장했습니다.");
		Console.WriteLine("입장할 던전을 선택해 주세요. (취소 : 0번)");
		PrintDungeonInfo();
		Console.Write(">> ");
		string input = Console.ReadLine()?.Trim() ?? "0";
		if(int.TryParse(input, out int dungeonIndex))
		{
			if (dungeonIndex == 0)
			{
				return prevScene;
			}
			else if (dungeonIndex <= dungeons.Count)
			{
				ResetDungeon(dungeons[dungeonIndex - 1]);
                if (ExploreDungeon(dungeons[dungeonIndex - 1])) return prevScene;
			}
			else
			{
				Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
			}
		}
		else
		{
			Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
		}
        return this;
    }

	/// <summary>
	/// 플레이어가 마을에 가야하면 true 반환
	/// </summary>
	/// <param name="dungeon"></param>
	/// <returns></returns>
	public bool ExploreDungeon(Dungeon dungeon)
	{
		
		while(true)
		{
            Console.Clear();
            Console.WriteLine($"{dungeon.Name}에 입장했습니다.");
            PrintMonsterInfo(dungeon);
			Console.WriteLine($"공격할 몬스터의 번호를 입력해 주세요. (던전 입구로 돌아가기 : 0번)");
			string str = Console.ReadLine()?.Trim() ?? "0";
			if(int.TryParse(str, out int monsterIndex))
			{
				if(monsterIndex == 0)
				{
					return true ;
				}
				else if (monsterIndex > 0 && monsterIndex <= dungeon.monsters.Count)
				{
					Console.WriteLine($"{turn} 번째 턴");
					Console.WriteLine($"현재 플레이어 체력: {GameManager.instance.playerData.HP} / {GameManager.instance.MaxHP}");
                    bool isPlayerTurn = random.Next(0, 2) == 0; // 플레이어 턴과 몬스터 턴을 랜덤으로 결정
					if (isPlayerTurn)
					{
						Console.WriteLine($"{GameManager.instance.playerData.Name} 님이 먼저 공격합니다.");
						if (PlayerAttackMonster(dungeon, monsterIndex - 1)) return true;
                        if (CheckMonsterHP(dungeon)) return true;
                        MonsterAttackPlayer(dungeon);
                        if (CheckHP()) return true;
                    }
                    else
                    {
						Console.WriteLine($"{dungeon.monsters[monsterIndex - 1].Name}이(가) 먼저 공격합니다.");
                        MonsterAttackPlayer(dungeon);
						if (CheckHP()) return true;
                        if (PlayerAttackMonster(dungeon, monsterIndex - 1)) return true;
						if (CheckMonsterHP(dungeon)) return true ;
                    }
					turn++;
                    Console.WriteLine("아무 키나 입력하여 턴을 마칩니다.");
                    Console.ReadKey(); // 턴 종료 후 대기
					if (CheckHP()) return true;
                }
			}
			else
			{
				Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
				continue;
			}
        }
    }

	/// <summary>
	/// 플레이어가 죽으면 true 반환
	/// </summary>
	/// <returns></returns>
	public bool CheckHP()
	{
		if(GameManager.instance.playerData.HP <= 0)
		{
            Console.WriteLine("당신은 죽었습니다. 마을로 돌아갑니다.");
            Console.WriteLine("아무 키나 입력하여 돌아갑니다.");
            Console.ReadKey();
			GameManager.instance.playerData.HP = (int)(GameManager.instance.MaxHP * 0.1);
            return true; // 플레이어가 죽음
        }
		return false;
    }

	public bool CheckMonsterHP(Dungeon dungeon)
	{
		foreach(var monster in dungeon.monsters)
		{
			if (monster.HP > 0) return false; // 몬스터가 살아있으면 false 
		}
		ClearDungeon(dungeon); // 몬스터가 죽었으면 던전 클리어	
        return true; // 몬스터가 다 죽었으면 true
    }

	public void ClearDungeon(Dungeon dungeon)
	{
        Console.WriteLine($"{dungeon.Name} 던전을 클리어 했습니다!");
        Console.WriteLine($"골드 보상: {dungeon.GoldReward}G | 경험치 보상: {dungeon.ExperienceReward}EXP");
        GameManager.instance.playerData.Gold += dungeon.GoldReward;
        GameManager.instance.playerData.Exp += dungeon.ExperienceReward;
        if (GameManager.instance.playerData.Exp >= 100)
        {
            GameManager.instance.playerData.Level += (GameManager.instance.playerData.Exp / 100);
            GameManager.instance.playerData.Exp %= 100; // 경험치 초기화
            Console.WriteLine($"레벨이 올랐습니다! 현재 레벨 : {GameManager.instance.playerData.Level}");
        }
        Console.WriteLine("던전 입구로 돌아갑니다.");
        Console.WriteLine("아무 키나 입력하여 돌아갑니다.");
        Console.ReadKey();
    }

    public void MonsterAttackPlayer(Dungeon dungeon)
	{
		foreach(var monster in dungeon.monsters)
		{
			if (monster.HP <= 0) continue; // 몬스터가 이미 죽었으면 공격하지 않음
			int damage = monster.ATK - GameManager.instance.playerData.DEF;
			if (damage < 0) damage = 0; // 방어력이 공격력을 초과할 경우 데미지 0
			GameManager.instance.playerData.HP -= damage;
			Console.WriteLine($"{monster.Name}이(가) 당신에게 {damage}의 피해를 입혔습니다.");
        }
    }

	/// <summary>
	/// 집에 돌아가야되면 true 반환
	/// </summary>
	/// <param name="dungeon"></param>
	/// <param name="monsterIndex"></param>
	/// <returns></returns>
    public bool PlayerAttackMonster(Dungeon dungeon, int monsterIndex)
	{
		PrintSkill();
		while (true)
		{
            Console.WriteLine("사용할 스킬의 번호를 입력하세요. (던전 입구로 돌아가기 : 0번)");
			Console.Write(">> ");
			string input = Console.ReadLine()?.Trim() ?? "0";
			if (int.TryParse(input, out int skillIndex))
			{
				if (skillIndex == 0) return true;
				else if(skillIndex > 0 && skillIndex <= GameManager.instance.playerData.Skills.Count)
				{
					if (!UseSkill(skillIndex - 1, turn, dungeon, monsterIndex)) continue; ;
					return false;
                }
				else
				{
					Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                }
			}
        }
		

    }

	public void PrintSkill()
	{
		foreach(var skill in GameManager.instance.playerData.Skills)
		{
			Console.WriteLine($"{GameManager.instance.playerData.Skills.IndexOf(skill) + 1} : " + skill.ToString());
        }
    }

	/// <summary>
	/// 스킬 사용이 성공했으면 true, 실패했으면 false
	/// </summary>
	/// <param name="skillIndex"></param>
	/// <param name="turn"></param>
	/// <param name="dungeon"></param>
	/// <param name="monsterIndex"></param>
	/// <returns></returns>
	public bool UseSkill(int skillIndex, int turn, Dungeon dungeon, int monsterIndex)
	{
		if (skillIndex < 0 || skillIndex >= GameManager.instance.playerData.Skills.Count)
		{
			Console.WriteLine("잘못된 스킬 인덱스입니다.");
			return false;
		}
		var skill = GameManager.instance.playerData.Skills[skillIndex];
		if (turn >= YouCanUseSkillThisTurn[skillIndex])
		{
			YouCanUseSkillThisTurn[skillIndex] = turn + skill.Cooldown; // 현재 턴으로 업데이트
			Console.WriteLine($"{skill.Name} 스킬을 사용했습니다.");
			// 스킬 사용 로직 추가
			Monster targetMonster = dungeon.monsters[monsterIndex]; 
			int playerPower = GameManager.instance.playerData.ATK;
            if (skill.isMultiply)
			{
                playerPower *= skill.Power;
			}
			else
			{
                playerPower += skill.Power;
            }
			int damage = playerPower - targetMonster.DEF;
			if(damage < 0) damage = 0; 
            dungeon.monsters[monsterIndex].HP -= damage;
			return true;
        }
		else
		{
			Console.WriteLine($"{skill.Name} 스킬은 아직 쿨타임 중입니다. {YouCanUseSkillThisTurn[skillIndex] - turn}턴 후에 사용할 수 있습니다.");
			return false;
		}
    }

    public void CooltimeCheck(int turn, ref List<int> YouCanUseSkillThisTurn)
	{
		for(int i = 0; i < YouCanUseSkillThisTurn.Count; i++)
		{
			if(turn - YouCanUseSkillThisTurn[i] >= GameManager.instance.playerData.Skills[i].Cooldown)
			{
				YouCanUseSkillThisTurn[i] = 0; // 쿨타임이 끝났으므로 초기화
			}
        }
    }

	public void PrintDungeonInfo()
	{
		foreach (var dungeon in dungeons)
		{
			Console.WriteLine($"{dungeons.IndexOf(dungeon) + 1} : " + dungeon.ToString());
        }
    }

	public void PrintMonsterInfo(Dungeon dungeon)
	{
		for (int i = 0; i < dungeon.monsters.Count; i++)
		{
			if(dungeon.monsters[i].IsDead)
			{
				continue;
            }
            Console.WriteLine($"{i + 1} : " + dungeon.monsters[i].ToString());
        }
	}
}
