# TextRPG
스파르타 내일배움캠프 유니티 11기 2주차 TextRPG 제작

```mermaid
classDiagram
direction TB
    class IGameScene {
	    +SetNextScene(IGameScene nextScene)
	    +SetPrevScene(IGameScene prevScene)
	    +StartScene()
	    +PrintNextScenes()
	    +EndScene(int nextSceneIdx)
    }

    class PlayerData {
	    +Name
	    +Level
	    +Job
	    +ATK
	    +DEF
	    +HP
	    +Gold
	    +List EquipItems
	    +List Items
	    +List Skills
    }

    class GameScene {
	    List nextScenes
	    IGameScene prevScene
	    sceneIndex
	    +SetNextScene(IGameScene nextScene)
	    +SetPrevScene(IGameScene prevScene)
	    +StartScene()
	    +PrintNextScenes()
	    +EndScene()
    }

    class FirstScene {
    }

    class TownScene {
    }

    class ProfileScene {
    }

    class InventoryScene {
    }

    class InnScene {
    }

    class Item {
	    +Name
	    +Description
	    +Stat
	    +Price
	    +IsEquip
	    +ItemType
    }

    class Skill {
	    +Name
	    +Description
	    +Cooldown
	    +Power
	    +isMultiply
    }

    class GameManager {
	    +enum SceneType
	    +GameManager instance
	    +Dictionary sceneString
	    +Dictionary scenes
	    +Dictionary itemTypeString
	    +PlayerData playerData
	    +string path
	    +SceneSetting()
	    +GameExit()
	    +NewPlayerData(string playerName)
	    +LoadPlayerData()
	    +SavePlayerData()
    }

    class Dungeon {
	    +Name
	    +Description
	    +GoldReward
	    +ExperienceReward
	    +NeedDEF
	    +List monsters
    }

    class Monster {
	    +Name
	    +ATK
	    +DEF
	    +HP
	    +MaxHP
	    +IsDead
    }

    class ShopItem {
	    +IsSoldOut
	    +string ToShopString()
    }

    class SkillTreeSkill {
	    +IsLearned
	    +NeedLevel
	    +NeedJob
	    +UpdateCanILearn()
	    +ToSkillTreeString()
    }

    class SkillInventoryScene {
	    +List skillTreeSkills
    }

    class ShopScene {
	    +List shopItems
    }

    class DungeonScene {
	    +List dungeons
    }

	<<Interface>> IGameScene
	<<Class>> PlayerData
	<<Class>> GameScene
	<<Class>> FirstScene
	<<Class>> TownScene
	<<Class>> ProfileScene
	<<Class>> InventoryScene
	<<Class>> InnScene
	<<Class>> Item
	<<Class>> Skill
	<<Class>> GameManager
	<<Class>> Dungeon
	<<Class>> Monster
	<<Class>> ShopItem
	<<Class>> SkillTreeSkill
	<<Class>> SkillInventoryScene
	<<Class>> ShopScene
	<<Class>> DungeonScene

    GameManager o-- IGameScene
    GameManager o-- PlayerData
    IGameScene <|.. GameScene
    GameScene <|-- FirstScene
    GameScene <|-- TownScene
    GameScene <|-- ProfileScene
    GameScene <|-- InventoryScene
    GameScene <|-- SkillInventoryScene
    GameScene <|-- ShopScene
    GameScene <|-- InnScene
    GameScene <|-- DungeonScene
    PlayerData o-- Item
    PlayerData o-- Skill
    DungeonScene o-- Dungeon
    Dungeon o-- Monster
    Item -- ShopItem
    ShopScene -- ShopItem
    Skill -- SkillTreeSkill
    SkillInventoryScene -- SkillTreeSkill



```
