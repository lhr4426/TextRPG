using System;
using System.Diagnostics;
using System.Numerics;
using System.Text.Json;
using System.Xml.Linq;

public class GameManager
{
    public enum SceneType
    {
        FirstScene = 1,
        TownScene,
        ProfileScene,
        InventoryScene,
        SkillInventoryScene,
        ShopScene,
        InnScene,
        DungeonScene,
    }
    public static GameManager instance { get; private set; }

    public static Dictionary<SceneType, string> sceneString = new Dictionary<SceneType, string>()
    {
        { SceneType.FirstScene, "로그인" },
        { SceneType.TownScene, "마을로" },
        { SceneType.ProfileScene, "상태 확인" },
        { SceneType.InventoryScene, "인벤토리" },
        { SceneType.SkillInventoryScene, "스킬트리" },
        { SceneType.ShopScene, "상점으로" },
        { SceneType.InnScene, "여관으로" },
        { SceneType.DungeonScene, "던전으로" }
    };

    public static Dictionary<SceneType, IGameScene> scenes = new Dictionary<SceneType, IGameScene>()
    {
        { SceneType.FirstScene, new FirstScene() },
        { SceneType.TownScene, new TownScene() },
        { SceneType.ProfileScene, new ProfileScene() },
        { SceneType.InventoryScene, new InventoryScene() },
        { SceneType.SkillInventoryScene, new SkillInventoryScene() },
        { SceneType.ShopScene, new ShopScene() },
        { SceneType.InnScene, new InnScene() },
        { SceneType.DungeonScene, new DungeonScene() }
    };

    public static Dictionary<IItem.ItemTypes, string> itemTypeString = new Dictionary<IItem.ItemTypes, string>()
    {
        { IItem.ItemTypes.Weapon, "무기" },
        { IItem.ItemTypes.HeadArmor, "머리 방어구" },
        { IItem.ItemTypes.BodyArmor, "몸통 방어구" },
        { IItem.ItemTypes.LegArmor, "다리 방어구" }
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


        SceneSetting();

        scenes[SceneType.FirstScene].StartScene();
    }

    public void SceneSetting()
    {
        scenes[SceneType.FirstScene].SetNextScene(scenes[SceneType.TownScene]);

        scenes[SceneType.TownScene].SetNextScene(scenes[SceneType.ProfileScene]);
        scenes[SceneType.TownScene].SetNextScene(scenes[SceneType.InventoryScene]);
        scenes[SceneType.TownScene].SetNextScene(scenes[SceneType.ShopScene]);
        scenes[SceneType.TownScene].SetNextScene(scenes[SceneType.InnScene]);
        scenes[SceneType.TownScene].SetNextScene(scenes[SceneType.DungeonScene]);

        scenes[SceneType.ProfileScene].SetPrevScene(scenes[SceneType.TownScene]);
        scenes[SceneType.InventoryScene].SetPrevScene(scenes[SceneType.TownScene]);
        scenes[SceneType.ShopScene].SetPrevScene(scenes[SceneType.TownScene]);
        scenes[SceneType.InnScene].SetPrevScene(scenes[SceneType.TownScene]);
        scenes[SceneType.DungeonScene].SetPrevScene(scenes[SceneType.TownScene]);

    }

    public void GameExit()
    {
        Console.WriteLine("게임을 종료합니다.");
        Environment.Exit(0);
    }

    public int BaseATK = 10;
    public int BaseDEF = 5;
    public int MaxHP = 100;

    public void NewPlayerData(string playerName)
    {
        playerData = new PlayerData();
        playerData.Name = playerName;
        playerData.Job = "전사";
        playerData.Level = 1;
        playerData.ATK = BaseATK;
        playerData.DEF = BaseDEF;
        playerData.HP = 100;
        playerData.Gold = 1500;
        playerData.EquipItem = new List<Item?>(new Item[4]);

        playerData.Items = new List<Item?>();
        Item defaultSword = new Item(
            "낡은 검",
            "기본 무기",
            5,
            0,
            IItem.ItemTypes.Weapon
            );
        playerData.Items.Add(defaultSword);
        if (playerData.Items[0] != null) playerData.Items[0].IsEquip = true; // 기본 아이템을 착용 상태로 설정
        playerData.EquipItem[(int)IItem.ItemTypes.Weapon] = defaultSword;

        playerData.Skills = new List<Skill?>();
        /*
        Skill defaultSkill = new Skill(
            "기본 공격",
            "적에게 기본 공격을 합니다.",
            0,
            0,
            false
            );
        playerData.Skills.Add(defaultSkill);
        */
    }
    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "playerdata.json");

    public void LoadPlayerData()
    {
        // Json 파일에서 불러오기
        Console.WriteLine("플레이어 데이터를 불러옵니다.");
        try
        {
            string json = File.ReadAllText(path);
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
        File.WriteAllText(path, json);
    }

}
