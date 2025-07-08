using System;
using System.Numerics;
using System.Text.Json;

public class GameManager
{
    public enum SceneType
    {
        FirstScene = 1,
        TownScene,
        ProfileScene,
        InventoryScene,
        ShopScene,
        InnScene,
        DungeonScene,
    }
    public static GameManager instance { get; private set; }

    public Dictionary<SceneType, string> sceneString = new Dictionary<SceneType, string>()
    {
        { SceneType.FirstScene, "로그인" },
        { SceneType.TownScene, "마을로" },
        { SceneType.ProfileScene, "상태 확인" },
        { SceneType.InventoryScene, "인벤토리" },
        { SceneType.ShopScene, "상점으로" },
        { SceneType.InnScene, "여관으로" },
        { SceneType.DungeonScene, "던전으로" }
    };

    public Dictionary<SceneType, IGameScene> scenes = new Dictionary<SceneType, IGameScene>()
    {
        { SceneType.FirstScene, new FirstScene() },
        { SceneType.TownScene, new TownScene() },
        { SceneType.ProfileScene, new ProfileScene() },
        { SceneType.InventoryScene, new InventoryScene() },
        { SceneType.ShopScene, new ShopScene() },
        { SceneType.InnScene, new InnScene() },
        { SceneType.DungeonScene, new DungeonScene() }
    };

    public PlayerData playerData;

    public GameManager()
	{
        if(instance == null)
                    {
            instance = this;
        }
        else
        {
            throw new InvalidOperationException("GameManager 인스턴스는 하나만 생성할 수 있습니다.");
        }

        scenes[SceneType.FirstScene].StartScene();
    }

    public void GameExit()
    {
        Console.WriteLine("게임을 종료합니다.");
        Environment.Exit(0);
    }

    public void NewPlayerData(string playerName)
    {
        playerData = new PlayerData();
        playerData.Name = playerName;
        playerData.Job = "전사";
        playerData.Level = 1;
        playerData.ATK = 10;
        playerData.DEF = 5;
        playerData.HP = 100;
        playerData.Gold = 1500;
        playerData.Items = new List<IItem>();
        playerData.Skills = new List<ISkill>();
    }

    public void LoadPlayerData()
    {
        // Json 파일에서 불러오기
        Console.WriteLine("플레이어 데이터를 불러옵니다.");
        try
        {
            string json = File.ReadAllText("playerdata.json");
            playerData = JsonSerializer.Deserialize<PlayerData>(json);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("플레이어 데이터 파일이 없습니다. 새 플레이어를 생성합니다.");
            NewPlayerData("Player");
            return;
        }

        
    }

    public void SavePlayerData()
    {
        // Json으로 저장
        Console.WriteLine("플레이어 데이터를 저장합니다.");
        string json = JsonSerializer.Serialize(playerData);
        File.WriteAllText("playerdata.json", json);
    }

}
