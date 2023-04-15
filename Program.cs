using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace SentimentAnalysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Загрузка файла словаря AFINN в формате json
            string afinnFilePath = "AFINN-ru.json";
            JObject afinn = JObject.Parse(File.ReadAllText(afinnFilePath));
            // Тестирование метода для определения тональности текста на основе словаря AFINN
            string text1 = "Да. Этот фильм был просто ужасный и жалобный";
            string text2 = "Я очень люблю этот ресторан";
            int score1 = GetSentimentScore(text1, afinn);
            int score2 = GetSentimentScore(text2, afinn); 
            Console.WriteLine(score1);
            Console.WriteLine(score2);
        }

        public static int GetSentimentScore(string text, JObject afinn)
        {
            // Удаление знаков препинания и других ненужных символов из текста
            Regex regex = new Regex(@"[^\w\s]+");
            string cleanedText = regex.Replace(text, "");


            int score = 0;
            string[] words = cleanedText.Split(' ');

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
