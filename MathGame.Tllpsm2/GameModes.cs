using System.Diagnostics;

namespace MathGame.Tllpsm2;

public class GameModes(List<GameResult> matchHistory)
{
    private int Score { get; set; }
    private const int PointsToGain = 1;
    private const int PointsToLose = 1;
    private const int TotalRounds = 5;

    private readonly Random random = new();
    private readonly List<GameResult> _matchHistory = matchHistory;

    public void RunGame(GameType type, DifficultyLevel difficulty)
    {
        Score = 0;
        Console.Clear();
        Console.WriteLine($"--- Starting New Game! ---");
        Console.WriteLine($"Game Mode: {type} | Difficulty: {difficulty}\n");

        Stopwatch stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < TotalRounds; i++)
        {
            if (type == GameType.Random)
            {
                GameType currentRoundType = (GameType)random.Next(0, 4);    // Randomly selects a game type
                PlayRound(currentRoundType, difficulty);
            }
            else
            {
                PlayRound(type, difficulty);                            // Uses the selected game type
            }
        }

        stopwatch.Stop();
        _matchHistory.Add(new GameResult(DateTime.Now, Score, type, difficulty, stopwatch.Elapsed));
        EndGame(stopwatch.Elapsed);
    }

    private void PlayRound(GameType type, DifficultyLevel difficulty)
    {
        // Generates random numbers based on difficulty level
        var (min, max) = difficulty.GetRange(type);

        switch (type)
        {
            case GameType.Summation:
                int add1 = random.Next(min, max);
                int add2 = random.Next(min, max);
                AskAndValidate($"What is {add1} + {add2}?", add1 + add2);
                break;
            case GameType.Subtraction:
                int sub1 = random.Next(min, max);
                int sub2 = random.Next(min, sub1);
                AskAndValidate($"What is {sub1} - {sub2}?", sub1 - sub2);
                break;
            case GameType.Division:
                int safeMinLimit = min <= 0 ? 1 : min;          // Prevents division by 0
                int divisor = random.Next(safeMinLimit, max);
                int quotient = random.Next(2, max);             // Generates the quotient (Minimum 2 to avoid 0 or 1 results)
                int dividend = divisor * quotient;
                AskAndValidate("What is " + dividend + " / " + divisor + "?", quotient);
                break;
            case GameType.Multiplication:
                int mult1 = random.Next(min, max);
                int mult2 = random.Next(min, max);
                AskAndValidate($"What is {mult1} * {mult2}?", mult1 * mult2);
                break;
        }
    }

    private void AskAndValidate(string question, int correctAnswer)
    {
        Console.Write(question + " ");

        if (!int.TryParse(Console.ReadLine(), out int userAnswer))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Invalid input. You missed your turn!");
            Console.ResetColor();
        }

        else if (userAnswer == correctAnswer)
        {
            Score += PointsToGain;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Correct! Your score is now: " + Score);
            Console.ResetColor();
        }
        else
        {
            Score -= PointsToLose;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nWrong! The correct answer was: {correctAnswer}. Your score is now: {Score}");
            Console.ResetColor();
        }
    }

    public void EndGame(TimeSpan timePlayed)
    {
        Console.Clear();
        Console.WriteLine("--- Game Over! ---");
        Console.WriteLine($"Your final score is: {Score}");
        Console.WriteLine($"Time played: {timePlayed.TotalSeconds:F2} s");
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }
}
