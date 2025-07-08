using System;

public class Skill : ISkill
{

    public string Name { get; set; }
    public string Description { get; set; }
   
    public int Cooldown { get; set; } // 몇 턴 쿨타임인지
    public int Power { get; set; } // 스킬 공격력

    public bool isMultiply { get; set; } // 곱연산 / 합연산 구분

    public Skill(String name, String Desc, int cool, int power, bool isMultiply)    
	{
        Name = name;
        Description = Desc;
        Cooldown = cool;
        Power = power;
        this.isMultiply = isMultiply; 
    }

    
}
