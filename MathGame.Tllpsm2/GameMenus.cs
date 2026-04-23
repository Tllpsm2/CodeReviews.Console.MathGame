namespace MathGame.Tllpsm2;

public class GameMenus
{
    private readonly List<GameResult> matchHistory = [];
    private readonly GameModes gameModes;
    public GameMenus()
    {
        gameModes = new GameModes(matchHistory);
    }

    public void ShowMainMenu()
    {
        bool keepPlaying = true;

        while (keepPlaying)
        {
            Console.Clear();
            Console.WriteLine("\n--- Welcome to the Math Game! ---\n");
            Console.WriteLine("1. Start New Game");
            Console.WriteLine("2. View Match History");
            Console.WriteLine("3. Exit");
            Console.Write("Please select an option: ");

            if (int.TryParse(Console.ReadLine(), out int option) && option >= 1 && option <= 3)
            {
                switch (option)
                {
                    case 1:
                        ShowGameModesMenu();
                        break;
                    case 2:
                        ViewMatchHistory();
                        break;
                    case 3:
                        keepPlaying = false;
                        Console.WriteLine("Thanks for playing!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
                Console.ReadKey();
            }
        }
    }

    public void ShowGameModesMenu()
    {
        Console.Clear();
        Console.WriteLine("\n--- Select Game Mode ---\n");
        Console.WriteLine("1. Summation");
        Console.WriteLine("2. Subtraction");
        Console.WriteLine("3. Division");
        Console.WriteLine("4. Multiplication");
        Console.WriteLine("5. Random");
        Console.Write("Please select a game mode: ");

        if (int.TryParse(Console.ReadLine(), out int option) && option >= 1 && option <= 5)
        {
            DifficultyLevel difficulty = DifficultyMenu();
            GameType selectedGameType = (GameType)(option - 1); // Converts the option to the GameType enum
            gameModes.RunGame(selectedGameType, difficulty);
        }
        else
        {
            Console.WriteLine("Invalid option. Returning to main menu.");
            Console.ReadKey();
        }
    }

    public void ViewMatchHistory()
    {
        Console.Clear();
        if (matchHistory.Count == 0)
        {
            Console.WriteLine("No match history available.");
        }
        else
        {
            Console.WriteLine("--- Match History ---\n");
            foreach (var result in matchHistory)
            {
                Console.WriteLine($"Date: {result.Date:g} | Score: {result.Score} | Game Type: {result.GameType} | Difficulty Level: {result.DifficultyLevel} | Time Played: {result.TimePlayed.TotalSeconds:F2} s");
            }
        }

        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }

    public static DifficultyLevel DifficultyMenu()
    {
        Console.Clear();
        Console.WriteLine("\n--- Select Difficulty Level ---\n");
        Console.WriteLine("1. Easy");
        Console.WriteLine("2. Medium");
        Console.WriteLine("3. Hard");
        Console.Write("Please select a difficulty level: ");

        if (int.TryParse(Console.ReadLine(), out int option) && option >= 1 && option <= 3)
        {
            return (DifficultyLevel)(option - 1);
        }
        else
        {
            Console.WriteLine("Invalid option. Returning to main menu.");
            Console.ReadKey();

        }
        return DifficultyLevel.Easy;
    }
}
