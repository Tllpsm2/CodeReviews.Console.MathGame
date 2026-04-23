namespace MathGame.Tllpsm2;

public enum GameType
{
    Summation,
    Subtraction,
    Division,
    Multiplication,
    Random
}

public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard,
}

public record GameResult(DateTime Date, int Score, GameType GameType, DifficultyLevel DifficultyLevel, TimeSpan TimePlayed);

