using System;

public class ProfileScene : GameScene
{
	public ProfileScene()
	{
        
    }
    public override void StartScene()
    {
        this.prevScene = GameManager.instance.scenes[GameManager.SceneType.TownScene];
        PrintProfile();

        Console.WriteLine("\n상태를 확인한 후, 다음으로 이동할 곳을 선택하세요.");
        PrintNextScenes();
    }

    public void PrintProfile()
    {
        Console.Clear();
        Console.WriteLine("플레이어 프로필:");
        Console.WriteLine($"레벨: {GameManager.instance.playerData.Level}");
        Console.WriteLine($"{GameManager.instance.playerData.Name} ( {GameManager.instance.playerData.Job} )");
        Console.WriteLine($"공격력 : {GameManager.instance.playerData.ATK}");
        Console.WriteLine($"방어력 : {GameManager.instance.playerData.DEF}");
        Console.WriteLine($"체력: {GameManager.instance.playerData.HP}");
        Console.WriteLine($"골드: {GameManager.instance.playerData.Gold} G");
        Console.WriteLine();
        Console.WriteLine("아이템 목록:");
        
        if (GameManager.instance.playerData.Items.Count == 0)
        {
            Console.WriteLine("아이템이 없습니다.");
        }
        else
        {
            foreach (var item in GameManager.instance.playerData.Items)
            {
                item.ToString();
            }
        }
        
        
    }

}
