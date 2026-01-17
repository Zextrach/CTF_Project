using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace CTFGame.Forms
{
    public class QuestionForm : CyberForm
    {
        private QuestionBase _currentQuestion;
        private Stopwatch _stopwatch;
        private Timer _uiTimer;

        // UI Elemanları
        private Label lblTimer;
        private TextBox txtInput;
        private TextBox txtToolInput;
        private TextBox txtToolOutput;
        private ComboBox cmbAlgo;

        // Yeni: Lider Tablosu Listesi
        private ListBox lstHighScores;

        public QuestionForm(QuestionBase question)
        {
            _currentQuestion = question;
            _stopwatch = new Stopwatch();
            _stopwatch.Start();

            // Tam Ekran Ayarları
            this.WindowState = FormWindowState.Maximized;
            this.Text = $"CTF Sınavı - Seviye {_currentQuestion.QuestionNumber} - Ajan: {ScoreManager.PlayerName}";

            this.FormClosed += (s, e) => Application.Exit();

            InitializeUI();
        }

        private void InitializeUI()
        {
            Panel mainPanel = new Panel();
            mainPanel.Size = new Size(1100, 650); // Genişliği artırdım (Lider tablosu için)
            mainPanel.BackColor = Color.Transparent;
            this.Controls.Add(mainPanel);

            this.Load += (s, e) => CenterPanel(mainPanel);
            this.Resize += (s, e) => CenterPanel(mainPanel);

            // --- LİDER TABLOSU (SOL ÜST KÖŞE) ---
            GroupBox grpScoreboard = new GroupBox();
            grpScoreboard.Text = " // TOP 5 AJANLAR ";
            grpScoreboard.ForeColor = Color.Yellow; // Dikkat çeksin diye sarı
            grpScoreboard.Location = new Point(20, 60); // En sol
            grpScoreboard.Size = new Size(200, 150);

            lstHighScores = new ListBox();
            lstHighScores.Location = new Point(10, 20);
            lstHighScores.Size = new Size(180, 120);
            lstHighScores.BackColor = Color.FromArgb(30, 30, 30);
            lstHighScores.ForeColor = Color.Yellow;
            lstHighScores.BorderStyle = BorderStyle.None;
            lstHighScores.Font = new Font("Consolas", 9F);

            // Skorları Yükle ve Listeye Ekle
            LoadHighScoresToList();

            grpScoreboard.Controls.Add(lstHighScores);
            mainPanel.Controls.Add(grpScoreboard);


            // --- ÜST BİLGİ (Puan ve Süre) ---
            // Puanı biraz sağa kaydırdık lider tablosuyla çakışmasın diye
            mainPanel.Controls.Add(CreateStyledLabel($"Puan: {ScoreManager.TotalScore}", 240, 20));

            lblTimer = CreateStyledLabel("Süre: 0s", 900, 20);
            mainPanel.Controls.Add(lblTimer);

            _uiTimer = new Timer();
            _uiTimer.Interval = 1000;
            _uiTimer.Tick += (s, e) => lblTimer.Text = $"Süre: {_stopwatch.Elapsed.TotalSeconds:F0}s";
            _uiTimer.Start();

            // --- ORTA PANEL: GÖREV ALANI ---
            GroupBox grpQuest = new GroupBox();
            grpQuest.Text = $" // GÖREV {_currentQuestion.QuestionNumber}: {_currentQuestion.Title} ";
            grpQuest.ForeColor = Color.Cyan;
            grpQuest.Location = new Point(240, 60); // Lider tablosunun sağına aldık
            grpQuest.Size = new Size(450, 350);
            grpQuest.FlatStyle = FlatStyle.Flat;

            grpQuest.Controls.Add(CreateStyledLabel("Şifreli Metin (Kopyalamak için seçin):", 20, 30));
            TextBox txtQuestionText = CreateStyledTextBox(20, 55, 410, 100, true, true);
            txtQuestionText.Text = _currentQuestion.ChallengeCipher;
            txtQuestionText.Font = new Font("Consolas", 11F);
            grpQuest.Controls.Add(txtQuestionText);

            grpQuest.Controls.Add(CreateStyledLabel("Teknik İpucu:", 20, 170));
            TextBox txtHintText = CreateStyledTextBox(20, 195, 410, 80, true, true);
            txtHintText.Text = _currentQuestion.Hint;
            txtHintText.ForeColor = Color.Gray;
            grpQuest.Controls.Add(txtHintText);

            mainPanel.Controls.Add(grpQuest);

            // --- CEVAP GİRİŞ ALANI ---
            mainPanel.Controls.Add(CreateStyledLabel("Bayrağı (Flag) Giriniz:", 240, 430));
            txtInput = CreateStyledTextBox(240, 460, 300, 30);
            mainPanel.Controls.Add(txtInput);

            Button btnSubmit = CreateStyledButton("BAYRAK GÖNDER", 550, 452);
            btnSubmit.BackColor = Color.FromArgb(0, 100, 0);
            btnSubmit.ForeColor = Color.White;
            btnSubmit.Click += BtnSubmit_Click;
            mainPanel.Controls.Add(btnSubmit);

            // --- SAĞ PANEL: ARAÇLAR ---
            GroupBox grpTools = new GroupBox();
            grpTools.Text = " // SİBER İSVİÇRE ÇAKISI ";
            grpTools.ForeColor = Color.Orange;
            grpTools.Location = new Point(710, 60);
            grpTools.Size = new Size(380, 520); // Biraz daralttık sığması için

            grpTools.Controls.Add(CreateStyledLabel("Çözüm Algoritması Seç:", 20, 30));
            cmbAlgo = new ComboBox();
            cmbAlgo.Items.AddRange(new string[] {
                "Base64 Decode", "Base64 Encode", "Caesar -3 (Decrypt)", "Caesar +3 (Encrypt)",
                "Hex to String", "ROT13", "Reverse String", "Binary Decode",
                "ASCII Shift -1 (Decrypt)", "A1Z26 Decode", "Rail Fence Decode", "Octal to String",
                "Atbash Decode", "Morse Decode", "MD5 Hash Çözücü", "MD5 Hash Oluştur"
            });
            cmbAlgo.SelectedIndex = 0;
            cmbAlgo.Location = new Point(20, 55);
            cmbAlgo.Width = 340;
            cmbAlgo.BackColor = Color.FromArgb(40, 40, 40);
            cmbAlgo.ForeColor = Color.Orange;
            cmbAlgo.DropDownStyle = ComboBoxStyle.DropDownList;
            grpTools.Controls.Add(cmbAlgo);

            grpTools.Controls.Add(CreateStyledLabel("Girdi:", 20, 100));
            txtToolInput = CreateStyledTextBox(20, 125, 340, 120, true);
            grpTools.Controls.Add(txtToolInput);

            Button btnProcess = CreateStyledButton("ÇÖZ / İŞLE", 20, 260);
            btnProcess.Width = 340;
            btnProcess.ForeColor = Color.Orange;
            btnProcess.Click += BtnProcess_Click;
            grpTools.Controls.Add(btnProcess);

            grpTools.Controls.Add(CreateStyledLabel("Çıktı:", 20, 320));
            txtToolOutput = CreateStyledTextBox(20, 345, 340, 120, true, true);
            txtToolOutput.ForeColor = Color.Lime;
            grpTools.Controls.Add(txtToolOutput);

            mainPanel.Controls.Add(grpTools);

            // ÇIKIŞ BUTONU
            Button btnExit = CreateStyledButton("ÇIKIŞ", 950, 600);
            btnExit.Size = new Size(100, 30);
            btnExit.BackColor = Color.Maroon;
            btnExit.Click += (s, e) => Application.Exit();
            mainPanel.Controls.Add(btnExit);
        }

        private void LoadHighScoresToList()
        {
            lstHighScores.Items.Clear();
            var scores = HighScoreManager.LoadScores();
            if (scores.Count == 0)
            {
                lstHighScores.Items.Add("Henüz kayıt yok...");
            }
            else
            {
                int rank = 1;
                foreach (var score in scores)
                {
                    lstHighScores.Items.Add($"{rank}. {score.Name}: {score.Score}");
                    rank++;
                }
            }
        }

        private void CenterPanel(Panel pnl)
        {
            pnl.Location = new Point(
                (this.ClientSize.Width - pnl.Width) / 2,
                (this.ClientSize.Height - pnl.Height) / 2
            );
        }

        private void BtnProcess_Click(object sender, EventArgs e)
        {
            // Araç mantığı aynı (Kısaltıldı)
            string input = txtToolInput.Text;
            string output = "";
            string selected = cmbAlgo.SelectedItem.ToString();
            try
            {
                if (selected.Contains("Base64 Decode")) output = CryptoHelper.Base64Decode(input);
                else if (selected.Contains("Base64 Encode")) output = CryptoHelper.Base64Encode(input);
                else if (selected.Contains("Caesar -3")) output = CryptoHelper.CaesarCipher(input, -3);
                else if (selected.Contains("Caesar +3")) output = CryptoHelper.CaesarCipher(input, 3);
                else if (selected.Contains("Hex to String")) output = CryptoHelper.HexToString(input);
                else if (selected.Contains("ROT13")) output = CryptoHelper.Rot13(input);
                else if (selected.Contains("Reverse String")) output = CryptoHelper.ReverseString(input);
                else if (selected.Contains("Binary Decode")) output = CryptoHelper.BinaryToString(input);
                else if (selected.Contains("ASCII Shift -1")) output = CryptoHelper.AsciiShift(input, -1);
                else if (selected.Contains("A1Z26 Decode")) output = CryptoHelper.A1Z26Decode(input);
                else if (selected.Contains("Rail Fence Decode")) output = CryptoHelper.RailFenceDecode(input);
                else if (selected.Contains("Octal to String")) output = CryptoHelper.OctalToString(input);
                else if (selected.Contains("Atbash")) output = CryptoHelper.Atbash(input);
                else if (selected.Contains("Morse")) output = CryptoHelper.MorseDecode(input);
                else if (selected.Contains("MD5 Hash Çözücü")) output = CryptoHelper.CrackMD5(input);
                else if (selected.Contains("MD5 Hash Oluştur")) output = CryptoHelper.CalculateMD5(input);
            }
            catch (Exception ex) { output = "Hata: " + ex.Message; }
            txtToolOutput.Text = output;
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (_currentQuestion.ValidateAnswer(txtInput.Text))
            {
                _stopwatch.Stop();
                _uiTimer.Stop();

                int pts = _currentQuestion.CalculateScore(_stopwatch.ElapsedMilliseconds / 1000);
                ScoreManager.AddScore(pts);

                // --- OYUN SONU KONTROLÜ (Skor Kaydetme) ---
                if (_currentQuestion.EasterEggLetter == '!')
                {
                    // Oyun Bitti! Skoru Kaydet.
                    HighScoreManager.SaveScore(ScoreManager.PlayerName, ScoreManager.TotalScore);

                    MessageBox.Show($"GÖREV TAMAMLANDI!\n\nGizli Şifre: {EasterEggManager.FINAL_PHRASE}\nToplam Puan: {ScoreManager.TotalScore}\n\nSkorun Lider Tablosuna Kaydedildi!", "ZAFER", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Application.Exit();
                    return;
                }

                // Normal Soru Geçişi
                EasterEggManager.AddLetter(_currentQuestion.EasterEggLetter);
                MessageBox.Show($"DOĞRU CEVAP!\n\nVeri Parçası: [{_currentQuestion.EasterEggLetter}]\nPuan: {pts}", "SİSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                QuestionBase nextQ = GetNextQuestion(_currentQuestion.QuestionNumber + 1);
                if (nextQ != null) new QuestionForm(nextQ).Show();
                else Application.Exit();
            }
            else
            {
                MessageBox.Show("Hatalı Bayrak!", "BAŞARISIZ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private QuestionBase GetNextQuestion(int number)
        {
            // Basit Fabrika
            switch (number)
            {
                case 1: return new Question1();
                case 2: return new Question2();
                case 3: return new Question3();
                case 4: return new Question4();
                case 5: return new Question5();
                case 6: return new Question6();
                case 7: return new Question7();
                case 8: return new Question8();
                case 9: return new Question9();
                case 10: return new Question10();
                case 11: return new Question11();
                case 12: return new Question12();
                case 13: return new Question13();
                case 14: return new Question14();
                case 15: return new Question15();
                default: return null;
            }
        }
    }
}