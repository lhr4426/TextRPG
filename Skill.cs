using System;

public class Skill : ISkill
{

    public string Name { get; set; }
    public string Description { get; set; }
   
    public int Cooldown { get; set; } // 몇 턴 쿨타임인지
    public int Power { get; set; } // 스킬 공격력

    public bool isMultiply { get; set; } // 곱연산 / 합연산 구분

    public bool IsEquip { get; set; }

    public Skill() { }

    public Skill(String name, String Desc, int cool, int power, bool isMultiply)    
	{
        Name = name;
        Description = Desc;
        Cooldown = cool;
        Power = power;
        this.isMultiply = isMultiply; 
        IsEquip = false; // 기본적으로 착용하지 않은 상태로 초기화
    }

    public override string ToString()
    {
        return $"{Name}\t|{Description}\t|쿨타임: {Cooldown}턴\t|공격력: {Power}\t|{(isMultiply ? "곱연산" : "합연산")}\t|";
    }

}
