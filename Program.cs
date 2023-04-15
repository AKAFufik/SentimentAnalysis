using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;

namespace SentimentAnalysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Загрузка файла словаря AFINN в формате json
            string afinnFilePath = "D:\\Документы\\Program\\SentimentAnalysis\\AFINN-ru.json";
            JObject afinn = JObject.Parse(File.ReadAllText(afinnFilePath));
            // Тестирование метода для определения тональности текста на основе словаря AFINN
            string text1 = "Да .Этот фильм был просто ужасный и жалобный";
            string text2 = "Я очень люблю этот ресторан";
            int score1 = GetSentimentScore(text1, afinn); // score1 = -3
            int score2 = GetSentimentScore(text2, afinn); // score2 = 3
            Console.WriteLine(score1);
            Console.WriteLine(score2);
        }

        public static int GetSentimentScore(string text, JObject afinn)
        {
            int score = 0;
            string[] words = text.Split(' ');

            foreach (string word in words)
            {
                if (afinn.TryGetValue(word, out JToken value))
                {
                    score += value.Value<int>();
                }
            }

            return score;
        }
    }
}
