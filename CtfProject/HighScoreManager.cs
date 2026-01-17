using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CTFGame
{
    // Skor Veri Yapısı
    public class ScoreEntry
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }

    public static class HighScoreManager
    {
        private static string filePath = "highscores.json";

        // Skorları Kaydet
        public static void SaveScore(string name, int score)
        {
            List<ScoreEntry> scores = LoadScores();

            // Yeni skoru ekle
            scores.Add(new ScoreEntry { Name = name, Score = score });

            // Puana göre büyükten küçüğe sırala ve sadece ilk 5'i tut
            scores = scores.OrderByDescending(s => s.Score).Take(5).ToList();

            // JSON formatına çevir (Basit el yapımı JSON oluşturucu)
            StringBuilder json = new StringBuilder();
            json.Append("[\n");
            for (int i = 0; i < scores.Count; i++)
            {
                json.Append(string.Format("  {{\"Name\": \"{0}\", \"Score\": {1}}}", scores[i].Name, scores[i].Score));
                if (i < scores.Count - 1) json.Append(",");
                json.Append("\n");
            }
            json.Append("]");

            File.WriteAllText(filePath, json.ToString());
        }

        // Skorları Oku
        public static List<ScoreEntry> LoadScores()
        {
            var list = new List<ScoreEntry>();
            if (!File.Exists(filePath)) return list;

            try
            {
                string json = File.ReadAllText(filePath);
                // Basit JSON parse işlemi (Regex veya kütüphane kullanmadan)
                string[] parts = json.Split(new char[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var part in parts)
                {
                    if (part.Contains("Name"))
                    {
                        string nameVal = ExtractValue(part, "Name");
                        string scoreVal = ExtractValue(part, "Score");

                        if (!string.IsNullOrEmpty(nameVal) && int.TryParse(scoreVal, out int sVal))
                        {
                            list.Add(new ScoreEntry { Name = nameVal, Score = sVal });
                        }
                    }
                }
            }
            catch { /* Hata olursa boş liste dön */ }

            return list.OrderByDescending(s => s.Score).ToList();
        }

        private static string ExtractValue(string source, string key)
        {
            int keyIndex = source.IndexOf("\"" + key + "\"");
            if (keyIndex == -1) return "";

            int colonIndex = source.IndexOf(":", keyIndex);
            int commaIndex = source.IndexOf(",", colonIndex);
            if (commaIndex == -1) commaIndex = source.Length;

            string raw = source.Substring(colonIndex + 1, commaIndex - colonIndex - 1).Trim();
            return raw.Replace("\"", "").Trim();
        }
    }
}