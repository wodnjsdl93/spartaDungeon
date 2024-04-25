using System;
using System.ComponentModel.Design;
using System.Net.Security;

namespace ConsoleApp3
{
    internal class Program
    {
        // 클래스 수준에 변수 선언
        public static int gold = 1500; // 보유골드
        public static int[] number = { 1, 2, 3, 4, 5, 6, 7 };
        public static string[] itemList =
        {
            "- 수련자 갑옷    | 방어력 +5  | 수련에 도움을 주는 갑옷입니다.             | ",
            "- 무쇠갑옷      | 방어력 +9  | 무쇠로 만들어져 튼튼한 갑옷입니다.           | ",
            "- 스파르타의 갑옷 | 방어력 +15 | 스파르타의 전사들이 사용했다는 전설의 갑옷입니다.| ",
            "- 낡은 검      | 공격력 +2  | 쉽게 볼 수 있는 낡은 검 입니다.            | ",
            "- 청동 도끼     | 공격력 +5  |  어디선가 사용됐던거 같은 도끼입니다.        | ",
            "- 스파르타의 창  | 공격력 +7  | 스파르타의 전사들이 사용했다는 전설의 창입니다. | ",
            "- 초보자용 책    | 공격력 + 13   | 초보한테 주어지는 특권용 책입니다.   |"
        };
        private static bool[] isItemPurchased = new bool[itemList.Length]; // 아이템 구매 여부 추적하는 배열
        private static bool[] isItemEquipped = new bool[itemList.Length]; // 아이템 장착 여부 추적하는 배열
        public static int[] itemPrices = { 1000, 1500, 3500, 600, 1500, 2500, 0 };
        static void Main(string[] args)
        {
            ShowMainMenu();
        }
        static void ShowMainMenu()
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

            Console.WriteLine("\n1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    CharacterStatus();
                    break;
                case "2":
                    Ineventory();
                    break;
                case "3":
                    MainShop();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }
        static void CharacterStatus() //캐릭터 창
        {
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");

            Console.WriteLine("\nLv. 01");
            Console.WriteLine("chad (전사)");
            Console.WriteLine("공격력 : 10");
            Console.WriteLine("방어력 : 5");
            Console.WriteLine("체 력 : 100");
            Console.WriteLine("Gold : 1500 G");

            Console.WriteLine("\n0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            string userInput = Console.ReadLine();

            if (userInput == "0")
            {
                ShowMainMenu();
            }
        }

        static void Ineventory() //인벤토리
        {
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");

            Console.WriteLine("\n[아이템 목록]");

            int purchasedItemCount = 0;

            for (int i = 0; i < itemList.Length; i++)
            {
                if (isItemPurchased[i]) // 해당 아이템을 구매했는지 확인
                {
                    purchasedItemCount++;
                    string equippedStatus = isItemEquipped[i] ? "[E]" : "[ ]"; // 장착 여부에 따라 표시를 선택

                    Console.WriteLine($"{purchasedItemCount}. {equippedStatus} {itemList[i]}");
                }
            }

            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            string userInput = Console.ReadLine();


            if (userInput == "1" && purchasedItemCount > 0)  // 인벤토리 1번 장착 관리를 눌렀을 때
            {
                ManageEquippedItems();
            }
            else if (userInput == "0")
            {
                ShowMainMenu();
            }
        }

        static void ManageEquippedItems()
        {
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("인벤토리 - 보유 중인 아이템을 관리할 수 있습니다.");

            Console.WriteLine("\n[아이템 목록]");

            for (int i = 0; i < itemList.Length; i++)
            {
                if (isItemPurchased[i]) // 해당 아이템을 구매했는지 확인
                {
                    string equippedStatus = isItemEquipped[i] ? "[E]" : "[ ]"; // 장착 여부에 따라 표시를 선택

                    Console.WriteLine($"{i + 1}. {equippedStatus} {itemList[i]}");
                }
            }

            Console.WriteLine("\n0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            string userInput = Console.ReadLine();

            if (userInput == "0")
            {
                Ineventory();
            }
            else
            {
                int selectedItemIndex = int.Parse(userInput) - 1;

                if (selectedItemIndex >= 0 && selectedItemIndex < itemList.Length)
                {
                    isItemEquipped[selectedItemIndex] = !isItemEquipped[selectedItemIndex]; // 장착 상태를 변경
                    Console.WriteLine($"{itemList[selectedItemIndex]}을(를) 장착하였습니다.");
                    Ineventory();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    ManageEquippedItems();
                }
            }
        }



        private static int selectedItemIndex;
        public static void MainShop()
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");

            Console.WriteLine("\n [보유골드]");
            Console.WriteLine(gold);

            Console.WriteLine("\n [아이템 목록]");

            string[] combinedList = new string[itemList.Length];

            for (int i = 0; i < itemList.Length; i++)
            {
                Console.WriteLine(itemList[i] + itemPrices[i] + " G");
            }

            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기");
            string userInput = Console.ReadLine();

            if (userInput == "1")
            {
                BuyShop();
                isItemPurchased[selectedItemIndex] = true; // 아이템 구매 여부 업데이트
            }
            else if (userInput == "0")
            {
                
                ShowMainMenu();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        public static void BuyShop()
        {
            
                int selectedItemIndex = -1; // 선택한 아이템 인덱스 초기화

            while (true)
            {
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");

                Console.WriteLine("\n [보유골드]");
                Console.WriteLine(gold);

                Console.WriteLine("\n [아이템 목록]");

                for (int i = 0; i < itemList.Length; i++)
                {
                    if (itemPrices[i] == -1) // 이미 구매한 아이템인 경우
                    {
                        Console.WriteLine(number[i] + itemList[i] + "구매완료");
                    }
                    else
                    {
                        Console.WriteLine(number[i] + itemList[i] + itemPrices[i] + " G");
                    }
                }
                Console.WriteLine("0. 나가기");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                string userInput = Console.ReadLine();

                if (userInput == "0")
                {
                    ShowMainMenu();
                    return; // 상점에서 나가면 메서드 종료
                }
                else
                {
                    selectedItemIndex = int.Parse(userInput) - 1;
                    if (selectedItemIndex >= 0 && selectedItemIndex < itemList.Length)
                    {
                        if (itemPrices[selectedItemIndex] == -1)
                        {
                            Console.WriteLine("이미 구매한 아이템입니다.");
                        }
                        else if (gold >= itemPrices[selectedItemIndex])
                        {
                            gold -= itemPrices[selectedItemIndex];
                            itemPrices[selectedItemIndex] = -1; // 구매 완료 표시
                            isItemPurchased[selectedItemIndex] = true; // 아이템 구매 완료 표시
                            Console.WriteLine($"{itemList[selectedItemIndex]}을(를) 구매하였습니다.");
                            //return; // 구매 완료 후 메서드 종료
                        }
                        else
                        {
                            Console.WriteLine("골드가 부족합니다.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
            }    
        }
    }
}
