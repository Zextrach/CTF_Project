using System;
using System.Windows.Forms;
using CTFGame.Forms;

namespace CTFGame
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Global değişkenleri sıfırla
            ScoreManager.Reset();
            EasterEggManager.Reset();

            // Giriş Formunu başlat
            Application.Run(new LoginForm());
        }
    }
}