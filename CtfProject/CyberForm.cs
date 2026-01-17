using System;
using System.Drawing;
using System.Windows.Forms;

namespace CTFGame
{
    // ===================================================================================
    // TEMEL FORM (BASE FORM - INHERITANCE PARENT)
    // ===================================================================================
    public class CyberForm : Form
    {
        public CyberForm()
        {
            this.BackColor = Color.FromArgb(32, 33, 36); // Modern Koyu Gri
            this.ForeColor = Color.FromArgb(0, 255, 65); // Cyber Yeşili
            this.Font = new Font("Segoe UI", 10F);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        // Tasarım kod tekrarını önlemek için ortak metotlar
        protected Button CreateStyledButton(string text, int x, int y)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(140, 45);
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = Color.FromArgb(45, 45, 48);
            btn.ForeColor = Color.Cyan;
            btn.FlatAppearance.BorderColor = Color.Cyan;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 60, 65);
            btn.Cursor = Cursors.Hand;
            return btn;
        }

        protected TextBox CreateStyledTextBox(int x, int y, int width, int height = 30, bool multiline = false, bool readOnly = false)
        {
            TextBox tb = new TextBox();
            tb.Location = new Point(x, y);
            tb.Width = width;
            tb.Height = height;
            tb.Multiline = multiline;
            if (multiline) tb.ScrollBars = ScrollBars.Vertical;

            tb.BackColor = Color.FromArgb(20, 20, 20);
            tb.ForeColor = Color.White;
            tb.BorderStyle = BorderStyle.FixedSingle;
            tb.ReadOnly = readOnly;

            if (readOnly)
            {
                tb.BackColor = Color.FromArgb(30, 30, 30);
                tb.ForeColor = Color.LightGray;
            }

            return tb;
        }

        protected Label CreateStyledLabel(string text, int x, int y, bool large = false)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Location = new Point(x, y);
            lbl.AutoSize = true;
            lbl.ForeColor = Color.FromArgb(0, 255, 65);
            if (large) lbl.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            return lbl;
        }
    }
}