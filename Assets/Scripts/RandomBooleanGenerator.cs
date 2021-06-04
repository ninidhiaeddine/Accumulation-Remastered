using System;

public class RandomBooleanGenerator
{
    /// <summary>
    /// Returns a boolean based on the probabilty percentage of returning "true".
    /// Example: the value of 0.5f returns exactly a randomized boolean with no bias.
    /// </summary>
    /// <param name="probability"></param>
    /// <returns></returns>
    public static bool GenerateRandomBoolean(float probability = 0.5f)
    {
        Random random = new Random();
        return (random.NextDouble() < probability);
    }
}
