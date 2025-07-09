using System;
using System.Text.Json;

public class SkillInventoryScene : GameScene
{
    public event Action? OnSkillTreeUpdated;

    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "skilltreedata.json");
    List<SkillTreeSkill> skillTreeSkills = new List<SkillTreeSkill>();

    public SkillInventoryScene()
	{
        
        
    }

    public void SettingSkillTree()
    {
        LoadSkillTree();
        SaveSkillTree();
    }
    public override IGameScene? StartScene()
    {
        Console.Clear();
        OnSkillTreeUpdated?.Invoke(); // 초기화 시 이벤트 호출
        while(true)
        {
            Console.WriteLine("스킬트리 : ");
            PrintSkillTree();
            Console.WriteLine("스킬을 배우고 싶다면 해당 스킬의 번호를 입력하세요. (취소 : 0번)");
            Console.Write(">> ");
            string input = Console.ReadLine()?.Trim() ?? "0";
            if (int.TryParse(input, out int skillIndex))
            {
                if (skillIndex == 0)
                {
                    SaveSkillTree();
                    return prevScene;
                }
                else if (skillIndex <= skillTreeSkills.Count)
                {
                    LearnSkill(skillIndex - 1);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    continue;
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                continue;
            }
        }
    }

    public void PrintSkillTree()
    {
        foreach(var skill in skillTreeSkills)
        {
            Console.WriteLine($"{skillTreeSkills.IndexOf(skill) + 1} : " + skill.ToSkillTreeString());
        }
    }

    public void LearnSkill(int skillIndex)
    {
        if (skillTreeSkills[skillIndex].IsLearned == SkillTreeSkill.LearnedStatus.Learnable)
        {
            Console.WriteLine($"{skillTreeSkills[skillIndex].Name} 스킬을 배우시겠습니까?");
            Console.WriteLine("1: 네, 2: 아니오");
            Console.Write(">> ");
            string input = Console.ReadLine()?.Trim() ?? "2";
            if (input == "1")
            {
                if (GameManager.instance.playerData.Level >= skillTreeSkills[skillIndex].NeedLevel &&
                    GameManager.instance.playerData.Job == skillTreeSkills[skillIndex].NeedJob)
                {
                    GameManager.instance.playerData.Skills.Add(skillTreeSkills[skillIndex]);
                    skillTreeSkills[skillIndex].IsLearned = SkillTreeSkill.LearnedStatus.Learned;
                    Console.WriteLine($"{skillTreeSkills[skillIndex].Name} 스킬을 배웠습니다.");
                    OnSkillTreeUpdated?.Invoke(); // 스킬 트리 업데이트 이벤트 호출
                    Console.WriteLine("아무 키나 입력하여 돌아갑니다.");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("스킬을 배우기 위한 조건을 만족하지 않습니다.");
                }
            }
            else
            {
                return;
            }
        }
        else if (skillTreeSkills[skillIndex].IsLearned == SkillTreeSkill.LearnedStatus.Learned)
        {
            Console.WriteLine("이미 배운 스킬입니다.");
            return;
        }
        else
        {
            Console.WriteLine("배울 수 없는 스킬입니다.");
            return;
        }
    }


    public void LoadSkillTree()
    {
        // Json 파일에서 불러오기
        Console.WriteLine("스킬 트리를 불러옵니다.");
        try
        {
            string json = File.ReadAllText(path);
            skillTreeSkills = JsonSerializer.Deserialize<List<SkillTreeSkill>>(json);
        }
        catch (FileNotFoundException)
        {
            NewSkillTree();
            return;
        }
    }

    public void SaveSkillTree()
    {
        string json = JsonSerializer.Serialize(skillTreeSkills);
        File.WriteAllText(path, json);
    }

    public void NewSkillTree()
    {
        OnSkillTreeUpdated = null; // 이벤트 초기화
        skillTreeSkills = new List<SkillTreeSkill>();
        // 기본 스킬 트리 설정

        AddSkillTree(new SkillTreeSkill("열파참", "열! 파! 참!", 5, 2, false, 2, PlayerData.Jobs.Warrior));
        AddSkillTree(new SkillTreeSkill("파이어볼", "빠이어!", 5, 2, false, 2, PlayerData.Jobs.Mage));
        AddSkillTree(new SkillTreeSkill("일섬", "최후의 공격", 3, 3, true, 5, PlayerData.Jobs.Warrior));
        AddSkillTree(new SkillTreeSkill("메테오", "파이어볼과는 다르다", 3, 3, true, 5, PlayerData.Jobs.Mage));

        SaveSkillTree();
    }

    public void AddSkillTree(SkillTreeSkill skill)
    {
        if (skill == null) return;
        skillTreeSkills.Add(skill);
        OnSkillTreeUpdated += skill.UpdateCanILearn;
        SaveSkillTree();
    }
}
