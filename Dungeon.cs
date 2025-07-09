using System;
using System.Text;

public class Dungeon
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int GoldReward { get; set; } // 던전을 클리어했을 때 얻는 골드
    public int ExperienceReward { get; set; } // 던전을 클리어했을 때 얻는 경험치
    public int NeedDEF { get; set; } // 던전을 입장하기 위한 최소 방어력

    public List<Monster> monsters { get; set; }

    public Dungeon() { }

    public Dungeon(string name, string description, int goldReward, int experienceReward, int needDEF)
    {
        Name = name;
        Description = description;
        GoldReward = goldReward;
        ExperienceReward = experienceReward;
        NeedDEF = needDEF;
        monsters = new List<Monster>();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"{Name}\t|");
        sb.Append($"{Description}\t|");
        sb.Append($"골드 보상: {GoldReward}G\t|");
        sb.Append($"입장 방어력 : {NeedDEF}\t|");

        return sb.ToString();
    }

    public string GetMonsterList()
    {
        if (monsters == null || monsters.Count == 0)
        {
            return "몬스터가 없습니다.";
        }
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("몬스터 목록:");
        foreach (var monster in monsters)
        {
            sb.AppendLine($"{monsters.IndexOf(monster) + 1} : "+monster.ToString());
        }
        return sb.ToString();
    }

    public void AttackToMonster()
    {

    }

    public void ClearDungeon()
    {
        // 던전 클리어 로직
        Console.WriteLine($"{Name} 던전을 클리어했습니다!");
        Console.WriteLine($"골드 {GoldReward}G와 경험치 {ExperienceReward}를 획득했습니다.");
        GameManager.instance.playerData.Gold += GoldReward;
        GameManager.instance.playerData.Exp += ExperienceReward;
        GameManager.instance.SavePlayerData(); // 플레이어 데이터 저장
    }

    public void AddMonster(Monster monster)
    {
        if (monsters == null)
        {
            monsters = new List<Monster>();
        }
        monsters.Add(monster);
    }
}
