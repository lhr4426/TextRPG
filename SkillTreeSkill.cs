using System;
using System.Text;
using System.Text.Json.Serialization;

public class SkillTreeSkill : Skill
{
	public enum LearnedStatus
	{
		NotLearnable = 0, // 배울 수 없음
		Learnable = 1, // 배울 수 있음
		Learned = 2 // 배움
    }

    
	public LearnedStatus IsLearned { get; set; } // 0: 배울 수 없음, 1: 배울 수 있음, 2: 배움

	public int NeedLevel { get; set; } // 스킬을 배우기 위한 최소 레벨

    
	public PlayerData.Jobs NeedJob { get; set; } // 스킬을 배우기 위한 직업

	public SkillTreeSkill() : base(){ }
    

    public SkillTreeSkill(string name, string description, int power, int cooldown, bool isMultiply, int NeedLevel, PlayerData.Jobs NeedJob, LearnedStatus IsLearned = LearnedStatus.NotLearnable) : base(name, description, cooldown, power, isMultiply)
    {
		this.NeedLevel = NeedLevel;
		this.NeedJob = NeedJob;
		this.IsLearned = IsLearned; // 0: 배울 수 없음, 1: 배울 수 있음, 2: 배움
		IsEquip = false; // 기본적으로 착용하지 않은 상태로 초기화
    }

    public void UpdateCanILearn()
	{
		if (GameManager.instance.playerData.Skills.Contains(this)) {
			IsLearned = LearnedStatus.Learned; // 이미 배운 스킬은 Learned로 설정
		}
		if (GameManager.instance.playerData.Level >= NeedLevel && GameManager.instance.playerData.Job == NeedJob)
		{
			IsLearned = LearnedStatus.Learnable; // 레벨과 직업이 조건을 만족하면 배울 수 있음
		}
		else
		{
			IsLearned = LearnedStatus.NotLearnable; // 조건을 만족하지 않으면 배울 수 없음
		}
	}
    public string ToSkillTreeString()
	{
		StringBuilder sb = new StringBuilder();
		sb.Append($"{Name}\t|");
		sb.Append($"{Description}\t|");	
		sb.Append($"쿨타임: {Cooldown}턴\t|");
		sb.Append($"공격력: {Power}\t|");
		if(isMultiply) sb.Append("곱연산\t|");
		else sb.Append("합연산\t|");
		switch(NeedJob)
		{
			case PlayerData.Jobs.Warrior:
				sb.Append("전사 전용\t|");
				break;
			case PlayerData.Jobs.Mage:
				sb.Append("마법사 전용\t|");
				break;
			default:
				sb.Append("모든 직업\t|");
				break;
        }
        switch (IsLearned)
		{
			case LearnedStatus.NotLearnable:
				sb.Append("배울 수 없음\t|");
				break;
			case LearnedStatus.Learnable:
				sb.Append("배울 수 있음\t|");
				break;
			case LearnedStatus.Learned:
				sb.Append("배움\t|");
				break;
        }

		return sb.ToString();
    }
}
