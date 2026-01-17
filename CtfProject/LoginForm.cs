using System;
using System.Drawing;
using System.Windows.Forms;

namespace CTFGame.Forms
{
    // Giriş Ekranı: İki farklı moda yönlendirme yapar.
    public class LoginForm : CyberForm
    {
        private TextBox txtName;

        public LoginForm()
        {
            this.Text = "CTF Giriş // Güvenli Erişim";
            this.Size = new Size(500, 450); // Boyutu biraz büyüttüm butonlar sığsın diye
            this.StartPosition = FormStartPosition.CenterScreen;

            Controls.Add(CreateStyledLabel("CTF SİBER SINAV SİSTEMİ", 110, 40, true));
            Controls.Add(CreateStyledLabel("Kimlik Doğrulaması Gerekli", 150, 80));

            Controls.Add(CreateStyledLabel("Kod Adı / İsim Giriniz:", 100, 140));

            txtName = CreateStyledTextBox(100, 170, 280, 30);
            Controls.Add(txtName);

            // 1. SEÇENEK: SINAVA BAŞLA
            Button btnExam = CreateStyledButton("SINAVA BAŞLA", 100, 250);
            btnExam.BackColor = Color.FromArgb(0, 100, 0); // Koyu yeşil
            btnExam.Click += BtnExam_Click;
            Controls.Add(btnExam);

            // 2. SEÇENEK: ŞİFRE ÇÖZÜCÜ (SANDBOX)
            Button btnSandbox = CreateStyledButton("ŞİFRE ÇÖZÜCÜ", 260, 250); // Yanına koyduk
            btnSandbox.Text = "ŞİFRE ÇÖZ\n(Sandbox)";
            btnSandbox.BackColor = Color.FromArgb(0, 0, 100); // Koyu lacivert
            btnSandbox.Click += BtnSandbox_Click;
            Controls.Add(btnSandbox);
            
            // Bilgilendirme notu
            Label lblInfo = new Label();
            lblInfo.Text = "* Sınav Modu: Puanlı ve süreli görevler.\n* Şifre Çözücü: Sadece araçları kullanmak için serbest mod.";
            lblInfo.Location = new Point(80, 320);
            lblInfo.AutoSize = true;
            lblInfo.ForeColor = Color.Gray;
            Controls.Add(lblInfo);
        }

        // Sınav Modu Butonu
        private void BtnExam_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Kimlik bilgisi zorunludur.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ScoreManager.PlayerName = txtName.Text;
            this.Hide();
            // İlk soruyla sınavı başlat
            try { new QuestionForm(new Question1()).Show(); }
            catch { MessageBox.Show("Soru dosyaları eksik!"); }
        }

        // Serbest Mod Butonu
        private void BtnSandbox_Click(object sender, EventArgs e)
        {
             if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Lütfen önce bir isim giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            ScoreManager.PlayerName = txtName.Text + " (Sandbox)";
            this.Hide();
            // Yeni Sandbox formunu aç
            new SandboxForm().Show();
        }
    }
}