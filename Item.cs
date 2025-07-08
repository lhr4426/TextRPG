using System;
using System.Text;

public class Item : IItem
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Stat { get; set; }
    public int Price { get; set; }
    public bool IsEquip { get; set; }
    public IItem.ItemTypes ItemType { get; set; }

    public Item(string name, string description, int stat, int price, IItem.ItemTypes itemType)
    {
        Name = name;
        Description = description;
        Stat = stat;
        Price = price;
        this.ItemType = itemType;
        IsEquip = false; // 기본적으로 착용하지 않은 상태로 초기화
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        if (IsEquip) // 착용했으면
        {
            sb.Append("[E] "); // 착용 표시
        }

        sb.Append($"{Name}\t|");
        if (this.ItemType > IItem.ItemTypes.Weapon) // 방어구인 경우
        {
            sb.Append($"방어력: +{Stat}\t|");
        }
        else // 무기인 경우
        {
            sb.Append($"공격력: +{Stat}\t|");
        }
        sb.Append($"{Description}\t|"); // 설명 표시
        sb.Append($"가격: {Price:C}G\t|"); // 가격 표시
        return sb.ToString();
    }
}

