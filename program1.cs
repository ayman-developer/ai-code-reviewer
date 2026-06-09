using System;

class SentimentAnalysis
{
    static void Main()
    {
        string review = "The food was excellent and service was amazing";

        string[] positiveWords = { "excellent", "amazing", "good", "great", "delicious" };
        string[] negativeWords = { "bad", "poor", "terrible", "awful", "slow" };

        int score = 0;

        foreach (string word in positiveWords)
        {
            if (review.ToLower().Contains(word))
                score++;
        }

        foreach (string word in negativeWords)
        {
            if (review.ToLower().Contains(word))
                score--;
        }

        if (score > 0)
            Console.WriteLine("Positive Review");
        else if (score < 0)
            Console.WriteLine("Negative Review");
        else
            Console.WriteLine("Neutral Review");
    }
}
