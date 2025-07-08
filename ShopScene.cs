using System;
using System.Text.Json;

public class ShopScene : GameScene
{
    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "shopdata.json");
    List<ShopItem> shopItems = new List<ShopItem>();
    public ShopScene()
	{
		SettingShopItems();
    }

    public void SettingShopItems()
    {
        LoadShopItems();
        SaveShopItems();
    }

    public void LoadShopItems()
    {
        // Json 파일에서 불러오기
        Console.WriteLine("플레이어 데이터를 불러옵니다.");
        try
        {
            string json = File.ReadAllText(path);
            shopItems = JsonSerializer.Deserialize<List<ShopItem>>(json);
        }
        catch (FileNotFoundException)
        {
            NewShopItems();
            return;
        }
    }

    public void SaveShopItems()
    {
        string json = JsonSerializer.Serialize(shopItems);
        File.WriteAllText(path, json);
    }

    public void NewShopItems()
    {
        shopItems = new List<ShopItem>();

        AddShopItem(new ShopItem("수련자 투구", "초보자를 위한 기본 투구입니다.", 5, 500, IItem.ItemTypes.HeadArmor));
        AddShopItem(new ShopItem("무쇠 투구", "총알도 튕길 수 있을지도?", 9, 1000, IItem.ItemTypes.HeadArmor));
        AddShopItem(new ShopItem("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 5, 500, IItem.ItemTypes.BodyArmor));
        AddShopItem(new ShopItem("무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 9, 1000, IItem.ItemTypes.BodyArmor));
        AddShopItem(new ShopItem("수련자 바지", "가볍지만 좀 약한 바지입니다.", 5, 500, IItem.ItemTypes.LegArmor));
        AddShopItem(new ShopItem("무쇠 바지", "꽤나 무거울 겁니다.", 9, 1000, IItem.ItemTypes.LegArmor));

        SaveShopItems();
    }

    public void AddShopItem(ShopItem item)
    {
        if (item == null) return;
        shopItems.Add(item);
        SaveShopItems();
    }


    public override IGameScene? StartScene()
	{
		Console.Clear();
		while(true)
		{
            Console.WriteLine("상점에 오신 것을 환영합니다!");
            Console.WriteLine("구매할 아이템을 선택하세요.");
            PrintShopItems();
            Console.WriteLine("\n아이템 구매를 원하시면 해당 아이템의 번호를 입력하세요. (취소 : 0번)");
            Console.Write(">> ");
            string input = Console.ReadLine()?.Trim() ?? "0";
            if (int.TryParse(input, out int itemIndex))
            {
                if (itemIndex == 0)
                {
                    this.prevScene?.StartScene();
                }
                else if (itemIndex <= shopItems.Count)
                {
                    PurchaseItem(itemIndex - 1);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    StartScene();
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                StartScene();
            }
        }
    }

	public void PrintShopItems()
	{
        foreach(var item in shopItems)
        {
            Console.WriteLine($"{shopItems.IndexOf(item) + 1}: " + item.ToShopString());
        }
    }
    
    public void PurchaseItem(int itemIndex)
    {
        if(shopItems[itemIndex].Price <= GameManager.instance.playerData.Gold)
        {
            while (true)
            {
                Console.WriteLine($"{shopItems[itemIndex].Name}을 구매하시겠습니까?");
                Console.WriteLine($"가격: {shopItems[itemIndex].Price:C}G");
                Console.WriteLine("1: 예, 2: 아니오");
                Console.Write(">> ");
                string input = Console.ReadLine()?.Trim() ?? "2";
                if (input == "1")
                {
                    GameManager.instance.playerData.Gold -= shopItems[itemIndex].Price;
                    shopItems[itemIndex].isSoldOut = true; // 아이템 품절 처리
                    GameManager.instance.playerData.Items.Add((Item)shopItems[itemIndex]);
                    
                    SaveShopItems();
                    Console.WriteLine($"{shopItems[itemIndex].Name}을 구매했습니다!");
                    Console.ReadKey();
                    StartScene(); // 상점 다시 시작
                    return;
                }
                else if (input == "2")
                {
                    Console.WriteLine("구매를 취소하셨습니다.");
                    StartScene(); // 상점 다시 시작
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    continue;
                }
            }
        }
        else
        {
            Console.WriteLine("골드가 부족합니다. 다시 시도하세요.");
            StartScene();
        }
    }
}
