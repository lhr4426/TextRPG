using System;

public class TownScene : GameScene
{
	public TownScene()
	{
        
    }

    public override IGameScene? StartScene()
    {
        StatCheck();
        GameManager.instance.SavePlayerData();
        Console.Clear();
        Console.WriteLine("당신은 마을에 도착했습니다.");
        Console.WriteLine("마을에서 무엇을 하시겠습니까?");
        Console.WriteLine();

        return PrintNextScenes();
    }

    public void StatCheck()
    {
        // 무기가 장착 되어있는지 확인
        Item? weapon = GameManager.instance.playerData.EquipItem[(int)IItem.ItemTypes.Weapon];
        if (weapon == null)
        {
            GameManager.instance.playerData.ATK = GameManager.instance.BaseATK;
        }
        else
        {
            GameManager.instance.playerData.ATK = GameManager.instance.BaseATK + weapon.Stat;
        }
        GameManager.instance.playerData.DEF = GameManager.instance.BaseDEF; // 기본 방어력 초기화
        // 방어구도 장착 되어있는지 확인
        for (int i = (int)IItem.ItemTypes.HeadArmor; i <= (int)IItem.ItemTypes.LegArmor; i++)
        {
            Item? armor = GameManager.instance.playerData.EquipItem[i];
            if (armor != null)
            {
                GameManager.instance.playerData.DEF += armor.Stat;
            }
        }
    }
}
