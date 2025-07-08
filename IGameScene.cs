using System;


public interface IGameScene
{
    void SetNextScene(IGameScene nextScene);
    void SetPrevScene(IGameScene prevScene);
    IGameScene? StartScene();

    IGameScene? PrintNextScenes();
    IGameScene? EndScene(int nextSceneIdx);

    
}
