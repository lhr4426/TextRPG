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

    class SkillInventoryScene {
    }

    class ShopScene {
    }

    class InnScene {
    }

    class DungeonScene {
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

	<<Interface>> IGameScene
	<<Class>> PlayerData
	<<Class>> GameScene
	<<Class>> FirstScene
	<<Class>> TownScene
	<<Class>> ProfileScene
	<<Class>> InventoryScene
	<<Class>> SkillInventoryScene
	<<Class>> ShopScene
	<<Class>> InnScene
	<<Class>> DungeonScene
	<<Class>> Item
	<<Class>> Skill
	<<Class>> GameManager

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


```
