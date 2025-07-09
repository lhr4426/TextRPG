using System;
using System.Text;

public class Monster
{
	public string Name { get; set; }
    public int ATK { get; set; } // 공격력
    public int DEF { get; set; } // 방어력
    public int HP { get; set; } // 체력

    public int MaxHP { get; set; } // 최대 체력, 현재 체력과 동일하게 설정

    public bool IsDead { get { return HP <= 0; }}


    public Monster()
	{
	}

    public Monster(string name, int atk, int def, int hp)
    {
        Name = name;
        ATK = atk;
        DEF = def;
        HP = hp;
        MaxHP = hp; 
    }

    public Monster MakeClone()
    {
        return new Monster(Name, ATK, DEF, HP);
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"{Name}\t|");
        sb.Append($"공격력: {ATK}\t|");
        sb.Append($"방어력: {DEF}\t|");
        sb.Append($"체력: {HP}\t|");
        return sb.ToString();
    }
}
