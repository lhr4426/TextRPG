using System;
using static GameManager;

public class GameScene : IGameScene
{
    protected List<IGameScene> nextScenes;
    protected IGameScene prevScene;
    protected int sceneIndex = 0;

    public GameScene()
    {
        nextScenes = new List<IGameScene>();
    }

    public void SetNextScene(IGameScene nextScene)
    {
        nextScenes.Add(nextScene);
    }

    public void SetPrevScene(IGameScene prevScene)
    {
        this.prevScene = prevScene;
    }

    public virtual void StartScene()
    {
        throw new NotImplementedException();
    }

    public void PrintNextScenes()
    {
        for(int i = 0; i < nextScenes.Count; i++)
        {
            GameManager.SceneType sceneType = GameManager.scenes.FirstOrDefault(x => x.Value == nextScenes[i]).Key;
            string sceneString = GameManager.sceneString[sceneType];
            Console.WriteLine($"{i + 1}: {sceneString}");
        }
        if(prevScene != null)
        {
            GameManager.SceneType prevType = GameManager.scenes.FirstOrDefault(x => x.Value == prevScene).Key;
            string sceneString = GameManager.sceneString[prevType];
            Console.WriteLine($"0: {sceneString}");
        }
        else
        {
            Console.WriteLine("0: 게임 종료");
        }
        Console.WriteLine("이동할 곳을 선택하세요. (숫자 입력)");
        GameManager.instance.SavePlayerData();
        Console.Write(">> ");
        string input = Console.ReadLine()?.Trim() ?? "-1";
        if(int.TryParse(input, out int nextSceneIdx))
        {
            EndScene(nextSceneIdx);
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다. 숫자를 입력해주세요.");
            PrintNextScenes();
        }
    }

    public void EndScene(int nextSceneIdx)
    {
        if(nextSceneIdx == 0)
        {
            if(prevScene == null) GameManager.instance.GameExit();
            prevScene?.StartScene();
        }
        if (nextSceneIdx > 0 && nextSceneIdx <= nextScenes.Count)
        {
            nextScenes[nextSceneIdx-1].StartScene();
        }
        else
        {
            PrintNextScenes();
        }
    }

    
}
