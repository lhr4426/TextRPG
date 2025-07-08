using System;


public interface IGameScene
{
    void SetNextScene(IGameScene nextScene);
    void SetPrevScene(IGameScene prevScene);
    void StartScene();

    void PrintNextScenes();
    void EndScene(int nextSceneIdx);

    
}
