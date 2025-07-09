using System;

public class FirstScene : GameScene
{
    
    public FirstScene()
    {
        
    }

    public override IGameScene? StartScene()
    {
        IGameScene? nextScene = PrintNextScenes();
        return nextScene;
    }

    public void SettingCharacter()
    {
        while (true)
        {
            Console.Clear();
            StartOrEnd();
            Console.Clear();

            Console.WriteLine("당신은 어떤 마을에서 눈을 뜹니다...");
            Console.WriteLine("이 마을을 방문한 적이 있는 것 같습니까? (Y/N) : ");
            string input = Console.ReadLine()?.Trim().ToUpper() ?? "N";
            if (input == "N")
            {
                MakePlayer();
                break;
            }
            else if (input == "Y")
            {
                // 플레이어 데이터 불러오기
                GameManager.instance.LoadPlayerData();
                if (GameManager.instance.playerData == null)
                {
                    Console.WriteLine("플레이어 데이터가 없습니다. 새 플레이어를 생성합니다.");
                    MakePlayer();
                    break;
                }
                else
                {
                    Console.WriteLine($"{GameManager.instance.playerData.Name} 님, 당신의 모험이 다시 시작됩니다.");
                    break;
                }
            }
            else
            {
                continue;
            }
        }
    }

    public void MakePlayer()
    {
        // 새 플레이어 생성 
        Console.WriteLine("당신의 이름은 무엇입니까? (미입력 시 Player로 고정) : ");
        string inputName = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(inputName))
        {
            inputName = "Player";
        }
        GameManager.instance.NewPlayerData(inputName);
        GameManager.instance.SavePlayerData();
        Console.WriteLine($"{inputName} 님, 당신은 새로운 모험을 시작하게 됩니다...");
        Console.WriteLine("아무 키나 입력하여 마을로 들어갑니다.");
        Console.ReadKey();
    }

    public void StartOrEnd()
    {
        Console.Clear();
        Console.WriteLine("TextRPG 게임에 오신 것을 환영합니다!");
        Console.WriteLine("1. 게임 시작");
        Console.WriteLine("0. 게임 종료");
        Console.Write("옵션을 선택하세요 : ");
        string input = Console.ReadLine()?.Trim().ToUpper() ?? "0";
        if (input == "1")
        {
            return;
        }
        else if (input == "0")
        {
            Console.WriteLine("게임을 종료합니다.");
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
            StartOrEnd();
        }
    }
    
}
