namespace MathGame.Tllpsm2;

public static class DifficultyLevelLimits
{
    public static (int Min, int Max) GetRange(this DifficultyLevel difficulty, GameType gameType)
    {
        int minLimit = 3;
        int maxLimit = 13;

        // Base limits for each difficulty level
        switch (difficulty)
        {
            case DifficultyLevel.Easy:
                minLimit = 3; maxLimit = 13; break;
            case DifficultyLevel.Medium:
                minLimit = 13; maxLimit = 53; break;
            case DifficultyLevel.Hard:
                minLimit = 53; maxLimit = 93; break;
        }

        // Summation and Substraction scaled limits
        if (gameType == GameType.Summation || gameType == GameType.Subtraction)
        {
            int scaleMultiplier = 1;
            if (difficulty == DifficultyLevel.Medium) scaleMultiplier = 7;
            if (difficulty == DifficultyLevel.Hard) scaleMultiplier = 14;

            minLimit *= scaleMultiplier;
            maxLimit *= scaleMultiplier;
        }

        return (minLimit, maxLimit);
    }
}