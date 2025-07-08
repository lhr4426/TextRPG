using System;

public interface IItem
{
    public enum ItemTypes
    {
        Weapon,
        HeadArmor,
        BodyArmor,
        LegArmor
    }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Stat { get; set; } // 아이템의 능력치. 종류에 따라 다름 (방어구는 방어력, 무기는 공격력)
    public decimal Price { get; set; }
    public bool IsEquip { get; set; } // 착용한 아이템인지 여부

    public ItemTypes ItemType { get; set; } // 아이템 종류 (무기, 방어구 등)
}
