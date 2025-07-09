using System;

public class GuildScene : GameScene
{
	public GuildScene()
	{

	}

    public override IGameScene? StartScene()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("모험가 길드에 오신 것을 환영합니다!");
            Console.WriteLine("이곳에서는 직업을 변경하실 수 있습니다.");
            PrintClasses();
            Console.WriteLine("직업을 변경하시려면 해당 직업의 번호를 입력하세요. (마을로 : 0번)");
            Console.Write(">> ");

            string input = Console.ReadLine()?.Trim() ?? "0";
            if (int.TryParse(input, out int choice))
            {
                if (choice == 0)
                {
                    return prevScene; // 마을로 돌아가기
                }
                else if (Enum.IsDefined(typeof(PlayerData.Jobs), choice - 1))
                {
                    ChangeClass((PlayerData.Jobs)(choice - 1));
                    continue;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                    continue;
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
                continue;
            }
        }
    }

    public void PrintClasses()
    {
        Console.WriteLine("직업 목록:");
        foreach (PlayerData.Jobs job in Enum.GetValues(typeof(PlayerData.Jobs)))
        {
            Console.WriteLine($"{((int)job) + 1} : {PlayerData.jobString[job]}");
        }
    }

    public void ChangeClass(PlayerData.Jobs job)
    {
        if(GameManager.instance.playerData.Job == job)
        {
            Console.WriteLine("이미 해당 직업입니다.");
            Console.ReadKey();
            return;
        }
        else
        {
            GameManager.instance.playerData.Job = job;
            Console.WriteLine($"{PlayerData.jobString[job]} 직업으로 변경되었습니다.");
            Console.ReadKey();
        }
    }
}
