using System;

public class InventoryScene : GameScene
{
	public InventoryScene()
	{
	}

	public override IGameScene? StartScene()
	{
        Console.Clear();
        while(true)
        {
            Console.WriteLine("인벤토리 :");
            Console.WriteLine();
            PrintInventoryItems();
            Console.WriteLine("\n장비를 착용하거나 벗으시려면 해당 아이템의 번호를 기입하세요. (취소 : 0번)");
            Console.Write(">> ");
            string input = Console.ReadLine()?.Trim() ?? "0";
            if (int.TryParse(input, out int itemIndex))
            {
                if(itemIndex== 0)
                {
                    return this.prevScene;
                }
                else if (itemIndex <= GameManager.instance.playerData.Items.Count)
                {
                    if (GameManager.instance.playerData.Items[itemIndex-1].IsEquip)
                    {
                        EquipmentChanged(itemIndex - 1, false); // 아이템 벗기기
                    }
                    else
                    {
                        EquipmentChanged(itemIndex - 1, true); // 아이템 착용하기
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    continue;
                }
            }
        }	
    }

	public void PrintInventoryItems()
	{
		if(GameManager.instance.playerData.Items.Count == 0)
		{
			Console.WriteLine("인벤토리에 아이템이 없습니다.");
			return;
        }
		for(int i = 0; i < GameManager.instance.playerData.Items.Count; i++)
		{
			Console.WriteLine($"{i + 1}: {GameManager.instance.playerData.Items[i].ToString()}");
        }
        return;
    }

    public void EquipmentChanged(int itemIndex, bool isEquip)
    {

        Item item = GameManager.instance.playerData.Items[itemIndex];
        Console.Clear();
        if (isEquip)
        {
            Console.WriteLine($"{item.Name} 아이템을 장착했습니다.");
            item.IsEquip = true;
            Item alreadyEquip = GameManager.instance.playerData.EquipItem[(int)item.ItemType];
            if (alreadyEquip != null && alreadyEquip.IsEquip)
            {
                var alreadyEquipInItems = GameManager.instance.playerData.Items.FirstOrDefault(x => x.Name == alreadyEquip.Name);
                if (alreadyEquipInItems != null) alreadyEquipInItems.IsEquip = false;
            }
            GameManager.instance.playerData.EquipItem[(int)item.ItemType] = item;

        }
        else
        {
            Console.WriteLine($"{item.Name} 아이템을 해제했습니다.");
            item.IsEquip = false;
            GameManager.instance.playerData.EquipItem[(int)item.ItemType] = null;
        }
    }
}
