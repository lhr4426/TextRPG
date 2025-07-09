using System;

public class InnScene : GameScene
{
	public InnScene()
	{
	}

	public override IGameScene? StartScene()
	{
		Console.Clear();
		
        while (true)
		{
            Console.WriteLine("여관에 도착했습니다.");
            Console.WriteLine("휴식을 취하시겠습니까? (50G 소모)");
            Console.WriteLine("1: 예 (체력 회복), 0: 아니오");
            Console.Write(">> ");
            string input = Console.ReadLine()?.Trim() ?? "0";
			if (int.TryParse(input, out int choice))
			{
				switch (choice)
				{
					case 1:
						if(!Rest()) continue;
						else return prevScene;
                    case 0:
						return prevScene;
					default:
						Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
						continue;
				}
			}
			else
			{
				Console.WriteLine("잘못된 입력입니다. 다시 시도하세요.");
			}
		}
    }

	public bool Rest()
	{
		if(GameManager.instance.playerData.Gold < 50)
		{
			Console.WriteLine("금액이 부족합니다.");
            Console.WriteLine("아무 키나 입력하여 돌아갑니다.");
            Console.ReadKey();
			return false;
        }
		else
		{
			Console.WriteLine("휴식을 취합니다...");
			GameManager.instance.playerData.Gold -= 50;
			GameManager.instance.playerData.HP = GameManager.instance.MaxHP; // 체력 회복
			GameManager.instance.SavePlayerData(); // 플레이어 데이터 저장
			Console.WriteLine("체력이 모두 회복되었습니다.");
            Console.WriteLine("아무 키나 입력하여 돌아갑니다.");
            Console.ReadKey();
            return true;
        }
    }
}
