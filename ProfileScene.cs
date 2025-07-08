using System;
using System.Security.Cryptography.X509Certificates;

public class ProfileScene : GameScene
{
	public ProfileScene()
	{
        
    }
    public override IGameScene? StartScene()
    {
        
        PrintProfile();
        PrintEquipItems();
        Console.WriteLine("\n상태를 확인한 후, 다음으로 이동할 곳을 선택하세요.");
        return PrintNextScenes();
    }

    public void PrintProfile()
    {
        Console.Clear();
        Console.WriteLine("플레이어 프로필:");
        Console.WriteLine($"레벨: {GameManager.instance.playerData.Level}");
        Console.WriteLine($"{GameManager.instance.playerData.Name} ( {GameManager.instance.playerData.Job} )");
        CheckStats();
        Console.WriteLine($"체력: {GameManager.instance.playerData.HP}");
        Console.WriteLine($"골드: {GameManager.instance.playerData.Gold} G");
        Console.WriteLine();
        Console.WriteLine("아이템 목록:");
    }

    public void PrintEquipItems()
    {
        var equipItems = GameManager.instance.playerData.EquipItem.FindAll(item => item != null);

        if (equipItems.Count == 0)
        {
            Console.WriteLine("장착한 아이템이 없습니다.");
        }
        else
        {
            foreach (var item in equipItems)
            {
                if (item != null) Console.WriteLine(item.ToString());
            }
        }
    }

    public void CheckStats()
    {
        Item? weapon = GameManager.instance.playerData.EquipItem[(int)IItem.ItemTypes.Weapon];
        if (weapon != null)
        {
            Console.WriteLine($"공격력 : {GameManager.instance.playerData.ATK}\t|(+{weapon.Stat})");
        }
        else
        {
            Console.WriteLine($"공격력 : {GameManager.instance.playerData.ATK}");
        }
        // Console.Write($"방어력 : {GameManager.instance.playerData.DEF}");
        int stat = 0;
        bool hasArmor = false;
        for(int i = (int)IItem.ItemTypes.HeadArmor; i <= (int)IItem.ItemTypes.LegArmor; i++)
        {
            Item? armor = GameManager.instance.playerData.EquipItem[i];
            if (armor != null)
            {
                stat += armor.Stat;
                hasArmor = true;
            }
        }   
        Console.Write($"방어력 : {GameManager.instance.playerData.DEF}");
        if (hasArmor)
        {
            Console.WriteLine($"\t|(+{stat})");
        }
        else
        {
            Console.WriteLine();
        }


    }

}
