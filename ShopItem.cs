using System;
using System.Text;

public class ShopItem : Item
{
    public bool IsSoldOut { get; set; }

    public ShopItem(string name, string description, int stat, int price, IItem.ItemTypes itemType) : base(name, description, stat, price, itemType)
    {
        this.IsSoldOut = false; // 기본적으로 판매 중인 상태로 초기화
    }

    public string ToShopString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"{Name}\t|");
        if (this.ItemType > IItem.ItemTypes.Weapon) // 방어구인 경우
        {
            sb.Append($"방어력: +{Stat}\t|");
        }
        else // 무기인 경우
        {
            sb.Append($"공격력: +{Stat}\t|");
        }
        sb.Append($"{Description}\t\t|"); // 설명 표시
        sb.Append($"가격: {Price:C}G\t|"); // 가격 표시
        if (IsSoldOut)
        {
            sb.Append("[품절]"); // 품절 표시
        }
        return sb.ToString();
    }
}
