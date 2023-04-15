using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SentimentAnalysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Загрузка файла словаря AFINN в формате json
            string afinnFilePath = "AFINN-ru.json";
            JObject afinn = JObject.Parse(File.ReadAllText(afinnFilePath));


            // Чтение текста из файла и его анализ
            string path = "text.txt";
            string text = ReadTextFromFile(path);
            string[] lines = text.Split('\n');
            foreach (string line in lines)
            {
                int score = GetSentimentScore(line, afinn);
                switch (score)
                {
                    case int n when (n > 0):
                        Console.WriteLine("Текст: {0}", line);
                        Console.WriteLine("Оценка: {0}", score);
                        Console.WriteLine("Настроение теста: положительное\n" + "-------------");
                        break;
                    case int n when (n < 0):
                        Console.WriteLine("Текст: {0}", line);
                        Console.WriteLine("Оценка: {0}", score);
                        Console.WriteLine("Настроение теста: отрицательное\n" + "-------------");
                        break;
                    default:
                        Console.WriteLine("Текст: {0}", line);
                        Console.WriteLine("Оценка: {0}", score);
                        Console.WriteLine("Настроение теста: нейтральное\n" + "-------------");
                        break;
                }
            }
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


        // Метод для чтения текста из файла
        public static string ReadTextFromFile(string path)
        {
            string text = "";
            using (StreamReader reader = new StreamReader(path))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }

    }
}
