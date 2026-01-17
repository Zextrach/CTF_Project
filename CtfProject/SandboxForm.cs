using System;
using System.Drawing;
using System.Windows.Forms;

namespace CTFGame.Forms
{
    // SANDBOX FORM: Soru yok, süre yok, puan yok. Sadece araçlar var.
    public class SandboxForm : CyberForm
    {
        private TextBox txtToolInput;
        private TextBox txtToolOutput;
        private ComboBox cmbAlgo;

        public SandboxForm()
        {
            this.Text = $"CTF Şifre Çözücü (Sandbox Modu) - Ajan: {ScoreManager.PlayerName}";

            // --- TAM EKRAN AYARLARI ---
            this.WindowState = FormWindowState.Maximized;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosed += (s, e) => Application.Exit();

            InitializeUI();
        }

        private void InitializeUI()
        {
            // İçerik Paneli
            Panel mainPanel = new Panel();
            mainPanel.Size = new Size(600, 650);
            mainPanel.BackColor = Color.Transparent;
            this.Controls.Add(mainPanel);

            // Paneli Ortala
            this.Load += (s, e) => CenterPanel(mainPanel);
            this.Resize += (s, e) => CenterPanel(mainPanel);

            // --- SAĞ ÜST KÖŞEYE KAPATMA BUTONU EKLE (İSTEK ÜZERİNE) ---
            Button btnCloseApp = new Button();
            btnCloseApp.Text = "X";
            btnCloseApp.Size = new Size(40, 40);
            btnCloseApp.Location = new Point(this.ClientSize.Width - 50, 10); // En sağ üst
            btnCloseApp.Anchor = AnchorStyles.Top | AnchorStyles.Right; // Ekran değişirse sağda kalsın
            btnCloseApp.BackColor = Color.Red;
            btnCloseApp.ForeColor = Color.White;
            btnCloseApp.FlatStyle = FlatStyle.Flat;
            btnCloseApp.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnCloseApp.Cursor = Cursors.Hand;
            btnCloseApp.Click += (s, e) => Application.Exit(); // Uygulamayı kapat
            this.Controls.Add(btnCloseApp); // Doğrudan forma ekliyoruz, panele değil

            // Başlık
            Label lblTitle = CreateStyledLabel("SERBEST ÇALIŞMA ALANI", 180, 20, true);
            lblTitle.ForeColor = Color.Orange;
            mainPanel.Controls.Add(lblTitle);

            // --- ARAÇLAR PANELİ ---
            GroupBox grpTools = new GroupBox();
            grpTools.Text = " // SİBER İSVİÇRE ÇAKISI ";
            grpTools.ForeColor = Color.Orange;
            grpTools.Location = new Point(50, 60);
            grpTools.Size = new Size(480, 520);

            // Algoritma Seçimi
            grpTools.Controls.Add(CreateStyledLabel("İşlem Seçiniz:", 20, 30));
            cmbAlgo = new ComboBox();
            cmbAlgo.Items.AddRange(new string[] {
                "Base64 Decode", "Base64 Encode", "Caesar -3 (Decrypt)", "Caesar +3 (Encrypt)",
                "Hex to String", "ROT13", "Reverse String", "Binary Decode",
                "ASCII Shift -1 (Decrypt)", "A1Z26 Decode", "Rail Fence Decode", "Octal to String",
                "Atbash Decode", "Morse Decode", "MD5 Hash Çözücü", "MD5 Hash Oluştur"
            });
            cmbAlgo.SelectedIndex = 0;
            cmbAlgo.Location = new Point(20, 55);
            cmbAlgo.Width = 440;
            cmbAlgo.BackColor = Color.FromArgb(40, 40, 40);
            cmbAlgo.ForeColor = Color.Orange;
            cmbAlgo.DropDownStyle = ComboBoxStyle.DropDownList;
            grpTools.Controls.Add(cmbAlgo);

            // Girdi Alanı
            grpTools.Controls.Add(CreateStyledLabel("Girdi (Metni Buraya Yapıştır):", 20, 100));
            txtToolInput = CreateStyledTextBox(20, 125, 440, 120, true);
            grpTools.Controls.Add(txtToolInput);

            // İşlem Butonu
            Button btnProcess = CreateStyledButton("ÇÖZÜMLE / ÇALIŞTIR", 20, 260);
            btnProcess.Width = 440;
            btnProcess.ForeColor = Color.Orange;
            btnProcess.Click += BtnProcess_Click;
            grpTools.Controls.Add(btnProcess);

            // Çıktı Alanı
            grpTools.Controls.Add(CreateStyledLabel("Sonuç:", 20, 320));
            txtToolOutput = CreateStyledTextBox(20, 345, 440, 150, true, true);
            txtToolOutput.ForeColor = Color.Lime;
            grpTools.Controls.Add(txtToolOutput);

            mainPanel.Controls.Add(grpTools);

            // ÇIKIŞ BUTONU (ALT)
            Button btnExit = CreateStyledButton("ANA MENÜYE DÖN", 240, 600);
            btnExit.Size = new Size(140, 30);
            btnExit.BackColor = Color.Gray;
            btnExit.ForeColor = Color.White;
            // Çıkış yerine Giriş formuna dönmek daha mantıklı olabilir Sandbox'ta
            btnExit.Click += (s, e) => {
                this.Hide();
                new LoginForm().Show();
            };
            mainPanel.Controls.Add(btnExit);
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
            catch (Exception ex)
            {
                output = "Hata: " + ex.Message;
            }
            txtToolOutput.Text = output;
        }
    }
}