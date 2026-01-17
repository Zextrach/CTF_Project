using System;
using System.Collections.Generic;

namespace CTFGame
{
    // PUAN VE OYUNCU YÖNETİCİSİ
    public static class ScoreManager
    {
        public static int TotalScore { get; private set; }
        public static string PlayerName { get; set; }

        public static void Reset()
        {
            TotalScore = 0;
            PlayerName = "Misafir";
        }

        public static void AddScore(int points)
        {
            TotalScore += points;
        }
    }

    // GİZLİ MESAJ (EASTER EGG) YÖNETİCİSİ

    public static class EasterEggManager
    {
        private static List<char> collectedLetters = new List<char>();
        public const string FINAL_PHRASE = "WHITEHATHACKER";

        public static void Reset()
        {
            collectedLetters.Clear();
        }

        public static void AddLetter(char letter)
        {
            collectedLetters.Add(letter);
        }

        public static string GetCollectedString()
        {
            return new string(collectedLetters.ToArray());
        }
    }
}