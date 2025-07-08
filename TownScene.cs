using System;

public class TownScene : GameScene
{
	public TownScene()
	{
        
    }

    public override void StartScene()
    {
        this.SetNextScene(GameManager.instance.scenes[GameManager.SceneType.ProfileScene]);
        this.SetNextScene(GameManager.instance.scenes[GameManager.SceneType.InventoryScene]);
        this.SetNextScene(GameManager.instance.scenes[GameManager.SceneType.ShopScene]);

        Console.Clear();
        Console.WriteLine("당신은 마을에 도착했습니다.");
        Console.WriteLine("마을에서 무엇을 하시겠습니까?");
        Console.WriteLine();

        PrintNextScenes();
    }

    
}
