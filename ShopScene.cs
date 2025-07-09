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
        AddShopItem(new ShopItem("쓸만해 보이는 칼", "기본 검보단 나아요.", 10, 1500, IItem.ItemTypes.Weapon));
        AddShopItem(new ShopItem("나무 지팡이", "땅 짚는용 아닙니다", 10, 1500, IItem.ItemTypes.Weapon));

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
        while (true)
        {
            Console.WriteLine("상점에 오신 것을 환영합니다!");
            Console.WriteLine("1. 아이템 구매\t2. 아이템 판매\t0. 마을가기");
            Console.Write(">> ");
            string input = Console.ReadLine()?.Trim() ?? "0";
            if (int.TryParse(input, out int choice))
            {
                switch (choice)
                {
                    case 1:
                        return BuyItem();
                    case 2:
                        return SellItem();
                    case 0:
                        SaveShopItems(); // 상점 아이템 저장
                        return prevScene;
                    default:
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

    public IGameScene? SellItem()
    {
        Console.Clear();
        while (true)
        {
            if (GameManager.instance.playerData.Items.Count == 0)
            {
                Console.WriteLine("판매할 아이템이 없습니다.");
                Console.WriteLine("아무 키나 입력하여 마을로 돌아갑니다.");
                Console.ReadKey();
                SaveShopItems(); // 상점 아이템 저장
                return prevScene;
            }
            Console.WriteLine("판매할 아이템을 선택하세요.");
            for (int i = 0; i < GameManager.instance.playerData.Items.Count; i++)
            {
                Item item = GameManager.instance.playerData.Items[i];
                if (item != null && !item.IsEquip)
                {
                    Console.WriteLine($"{i + 1}: {item.ToString()}");
                }
            }
            Console.WriteLine("\n아이템 판매를 원하시면 해당 아이템의 번호를 입력하세요. (취소 : 0번)");
            Console.Write(">> ");
            string input = Console.ReadLine()?.Trim() ?? "0";
            if( int.TryParse(input, out int itemIndex))
            {
                if (itemIndex == 0)
                {
                    SaveShopItems(); // 상점 아이템 저장
                    return prevScene;
                }
                else if (itemIndex <= GameManager.instance.playerData.Items.Count)
                {
                    SaleItem(itemIndex - 1);
                    Console.Clear();
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

    public IGameScene? BuyItem()
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine("구매할 아이템을 선택하세요.");
            PrintShopItems();
            Console.WriteLine("\n아이템 구매를 원하시면 해당 아이템의 번호를 입력하세요. (취소 : 0번)");
            Console.Write(">> ");
            string input = Console.ReadLine()?.Trim() ?? "0";
            if (int.TryParse(input, out int itemIndex))
            {
                if (itemIndex == 0)
                {
                    SaveShopItems(); // 상점 아이템 저장
                    return prevScene;
                }
                else if (itemIndex <= shopItems.Count)
                {
                    PurchaseItem(itemIndex - 1);
                    Console.Clear();
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

	public void PrintShopItems()
	{
        foreach(var item in shopItems)
        {
            Console.WriteLine($"{shopItems.IndexOf(item) + 1}: " + item.ToShopString());
        }
    }

    public void SaleItem(int itemIndex)
    {
        Item item = GameManager.instance.playerData.Items[itemIndex];
        if (item == null)
        {
            Console.WriteLine("판매할 수 없는 아이템입니다.");
            Console.WriteLine("아무 키나 입력하여 돌아갑니다.");
            Console.ReadKey();
            return;
        }
        if (item.IsEquip)
        {
            Console.WriteLine("장착하고 있는 아이템은 판매할 수 없습니다.");
            Console.WriteLine("아무 키나 입력하여 돌아갑니다.");
            Console.ReadKey();
            return;
        }

        while(true)
        {
            Console.WriteLine($"{item.Name} 아이템을 판매하시겠습니까?");
            int sellPrice = item.Price / 2; // 판매 가격은 절반으로
            Console.WriteLine($"판매 가격: {sellPrice}G"); // 판매가격은 절반으로
            Console.WriteLine("1: 예, 2: 아니오");
            Console.Write(">> ");
            string input = Console.ReadLine()?.Trim() ?? "2";
            if (input == "1")
            {
                GameManager.instance.playerData.Gold += sellPrice;
                GameManager.instance.playerData.Items.RemoveAt(itemIndex); // 아이템 리스트에서 제거
                shopItems.Find(i => i.Name == item.Name).isSoldOut = false; // 판매하면 품절 아님
                SaveShopItems(); // 상점 아이템 저장

                Console.WriteLine($"{item.Name}을(를) 판매했습니다!");
                Console.WriteLine("아무 키나 입력하여 돌아갑니다.");
                Console.ReadKey();
                return;
            }
            else if (input == "2")
            {
                Console.WriteLine("판매를 취소하셨습니다.");
                return;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                continue;
            }
        }
    }
    
    public void PurchaseItem(int itemIndex)
    {
        if (shopItems[itemIndex].isSoldOut)
        {
            Console.WriteLine($"{shopItems[itemIndex].Name}은(는) 품절되었습니다.");
            Console.WriteLine("아무 키나 입력하여 돌아갑니다.");
            Console.ReadKey();
            return;
        }
        
        if (shopItems[itemIndex].Price <= GameManager.instance.playerData.Gold)
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
                    Console.WriteLine("아무 키나 입력하여 돌아갑니다.");
                    Console.ReadKey();
                    return;
                }
                else if (input == "2")
                {
                    Console.WriteLine("구매를 취소하셨습니다.");
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
            Console.WriteLine("아무 키나 입력하여 돌아갑니다.");
            Console.ReadKey();
            return;
        }
    }
}
