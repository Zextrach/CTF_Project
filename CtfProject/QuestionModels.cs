using System;

namespace CTFGame
{
    public interface IQuestion
    {
        int QuestionNumber { get; }
        string Title { get; }
        string ChallengeCipher { get; }
        string Hint { get; }
        char EasterEggLetter { get; }

        bool ValidateAnswer(string input);
        int CalculateScore(long elapsedSeconds);
    }

    // SOYUT SORU TABANI (ABSTRACT BASE CLASS)
   
    public abstract class QuestionBase : IQuestion
    {
        public int QuestionNumber { get; protected set; }
        public string Title { get; protected set; }
        public string ChallengeCipher { get; protected set; }
        public string CorrectAnswer { get; protected set; }
        public char EasterEggLetter { get; protected set; }
        public string Hint { get; protected set; }

        // Interface'den gelen cevap kontrol metodu
        public virtual bool ValidateAnswer(string input)
        {
            // Büyük/küçük harf duyarsız kontrol (Örn: 'Admin' == 'admin')
            return input != null && input.Equals(CorrectAnswer, StringComparison.OrdinalIgnoreCase);
        }

        // Interface'den gelen puan hesaplama metodu
        public virtual int CalculateScore(long elapsedSeconds)
        {
            // Zaman arttıkça puan azalır, ama en az 10 puan verilir.
            int score = 100 - (int)(elapsedSeconds / 2);
            return Math.Max(10, score);
        }
    }

    // SOMUT SORULAR (CONCRETE CLASSES)

    public class Question1 : QuestionBase
    {
        public Question1()
        {
            QuestionNumber = 1;
            Title = "Başlangıç";
            ChallengeCipher = "V2VsY29tZQ==";
            CorrectAnswer = "Welcome";
            EasterEggLetter = 'W';
            Hint = "Veriyi ASCII karakter dizilerine dönüştüren, genellikle e-posta eklerinde kullanılan ve çıktısı '=' dolgu karakteriyle bitebilen 64 tabanlı kodlama şeması.";
        }
    }

    public class Question2 : QuestionBase
    {
        public Question2()
        {
            QuestionNumber = 2;
            Title = "Julius'un Sırrı";
            ChallengeCipher = "Khur";
            CorrectAnswer = "Hero";
            EasterEggLetter = 'H';
            Hint = "Monoalfabetik bir yer değiştirme şifresi. Harfler, alfabedeki sıralarına göre sabit bir sayı (bu durumda 3) kadar kaydırılır.";
        }
    }

    public class Question3 : QuestionBase
    {
        public Question3()
        {
            QuestionNumber = 3;
            Title = "Makine Dili";
            ChallengeCipher = "49 6E 66 6F";
            CorrectAnswer = "Info";
            EasterEggLetter = 'I';
            Hint = "Bilişim sistemlerinde ikili (binary) veriyi daha okunabilir kılmak için kullanılan, 0-9 ve A-F sembollerinden oluşan 16 tabanlı sayı sistemi.";
        }
    }

    public class Question4 : QuestionBase
    {
        public Question4()
        {
            QuestionNumber = 4;
            Title = "Yolun Yarısı";
            ChallengeCipher = "Gvzr";
            CorrectAnswer = "Time";
            EasterEggLetter = 'T';
            Hint = "Sezar şifrelemesinin özel bir durumu. Latin alfabesi 26 harf içerir; bu algoritma karakterleri alfabenin tam ortasından (13 adım) döndürür.";
        }
    }

    public class Question5 : QuestionBase
    {
        public Question5()
        {
            QuestionNumber = 5;
            Title = "Ayna Ayna";
            ChallengeCipher = "gnireenignE";
            CorrectAnswer = "Engineering";
            EasterEggLetter = 'E';
            Hint = "Herhangi bir karmaşık algoritma içermez. Transpozisyon (yer değiştirme) şifrelemesinin en ilkel hali; dizenin sırasının bütünüyle ters çevrilmesi.";
        }
    }

    public class Question6 : QuestionBase
    {
        public Question6()
        {
            QuestionNumber = 6;
            Title = "Açık ve Kapalı";
            ChallengeCipher = "01001000 01101001";
            CorrectAnswer = "Hi";
            EasterEggLetter = 'H';
            Hint = "Modern bilgisayarların temel dili. Voltajın varlığı (1) ve yokluğu (0) ile temsil edilen 2 tabanlı sayı sistemi.";
        }
    }

    public class Question7 : QuestionBase
    {
        public Question7()
        {
            QuestionNumber = 7;
            Title = "Sadece Bir Adım";
            ChallengeCipher = "Benjo";
            CorrectAnswer = "Admin";
            EasterEggLetter = 'A';
            Hint = "Her karakterin sayısal ASCII değerinin aritmetik olarak 1 artırıldığı basit bir öteleme işlemi.";
        }
    }

    public class Question8 : QuestionBase
    {
        public Question8()
        {
            QuestionNumber = 8;
            Title = "Katmanlar";
            ChallengeCipher = "VkdWemRBPT0=";
            CorrectAnswer = "Test";
            EasterEggLetter = 'T';
            Hint = "Şifreleme değil, kodlama. Veri, 64 tabanlı kodlama şeması (Base64) ile kodlanmış, ardından elde edilen çıktı tekrar aynı işlemden geçirilmiştir (İteratif işlem).";
        }
    }

    public class Question9 : QuestionBase
    {
        public Question9()
        {
            QuestionNumber = 9;
            Title = "Sayısal Alfabe";
            ChallengeCipher = "8-1-3-11";
            CorrectAnswer = "Hack";
            EasterEggLetter = 'H';
            Hint = "A1Z26 olarak bilinen, harflerin alfabedeki sıra numaralarıyla (A=1, Z=26) temsil edildiği basit bir ikame şifresi.";
        }
    }

    public class Question10 : QuestionBase
    {
        public Question10()
        {
            QuestionNumber = 10;
            Title = "Alfa Omega";
            ChallengeCipher = "Zggzxp";
            CorrectAnswer = "Attack";
            EasterEggLetter = 'A';
            Hint = "İbrani kökenli monoalfabetik bir şifre. Alfabe tersine çevrilir; ilk harf son harfle, ikinci harf sondan ikinci harfle eşleşir.";
        }
    }

    public class Question11 : QuestionBase
    {
        public Question11()
        {
            QuestionNumber = 11;
            Title = "Zig Zag";
            ChallengeCipher = "CBRYE";
            CorrectAnswer = "Cyber";
            EasterEggLetter = 'C';
            Hint = "Transpozisyon şifresi. Metin, hayali bir çitin rayları üzerinde zikzak çizerek yazılır. Bu örnekte 2 ray (derinlik) kullanılmıştır.";
        }
    }

    public class Question12 : QuestionBase
    {
        public Question12()
        {
            QuestionNumber = 12;
            Title = "Noktalar ve Çizgiler";
            ChallengeCipher = "-.- . -.--";
            CorrectAnswer = "Key";
            EasterEggLetter = 'K';
            Hint = "Telekomünikasyon tarihinde metin bilgisini iletmek için kullanılan, kısa ve uzun sinyallerin standart dizilerine dayanan yöntem.";
        }
    }

    public class Question13 : QuestionBase
    {
        public Question13()
        {
            QuestionNumber = 13;
            Title = "Kırılamaz mı?";
            ChallengeCipher = "827ccb0eea8a706c4c34a16891f84e7b";
            CorrectAnswer = "12345";
            EasterEggLetter = 'E';
            Hint = "128-bitlik bir özet (digest) üreten kriptografik hash fonksiyonu. Tek yönlüdür ancak yaygın (weak) parolalar için 'Rainbow Table' veya sözlük saldırıları ile çarpışma bulunabilir.";
        }
    }

    public class Question14 : QuestionBase
    {
        public Question14()
        {
            QuestionNumber = 14;
            Title = "Sekizli Sistem";
            ChallengeCipher = "122 117 117 124";
            CorrectAnswer = "Root";
            EasterEggLetter = 'R';
            Hint = "0'dan 7'ye kadar olan rakamları kullanan 8 tabanlı sayı sistemi. Unix dosya izinlerinde sıkça karşılaşılır.";
        }
    }

    public class Question15 : QuestionBase
    {
        public Question15()
        {
            QuestionNumber = 15;
            Title = "USTA ANAHTAR (MASTER KEY)";
            ChallengeCipher = "Önceki 14 görevden topladığın harfleri birleştir.";
            CorrectAnswer = EasterEggManager.FINAL_PHRASE;
            EasterEggLetter = '!';
            Hint = "Elde ettiğin parçaları sırasıyla birleştirerek nihai bayrağı oluştur.";
        }
    }
}