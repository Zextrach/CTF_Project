namespace CtfProject
{
    // ===================================================================================
    // INTERFACE (ARAYÜZ)
    // Bir "Soru"nun sahip olması gereken standart özellikleri belirler.
    // ===================================================================================
    public interface IQuestion
    {
        // Özellikler (Properties)
        int QuestionNumber { get; }     // Kaçıncı soru?
        string Title { get; }           // Başlığı ne?
        string ChallengeCipher { get; } // Şifreli metin ne?
        string Hint { get; }            // İpucu ne?
        char EasterEggLetter { get; }   // Gizli harf ne?

        // Metotlar (Methods - Yetenekler)
        bool ValidateAnswer(string input); // Cevabı kontrol etme yeteneği
        int CalculateScore(long seconds);  // Puan hesaplama yeteneği
    }
}