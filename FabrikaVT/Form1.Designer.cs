namespace FabrikaVT
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxSifreyiGoster = new System.Windows.Forms.CheckBox();
            this.panelResim = new System.Windows.Forms.Panel();
            this.labelHosgeldiniz = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelSifre = new System.Windows.Forms.Label();
            this.labelKullanıcıAdı = new System.Windows.Forms.Label();
            this.buttonGiris = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelResim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(266, 103);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(138, 22);
            this.textBoxUserName.TabIndex = 0;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(266, 143);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(138, 22);
            this.textBoxPassword.TabIndex = 1;
            this.textBoxPassword.TextChanged += new System.EventHandler(this.textBoxPassword_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.checkBoxSifreyiGoster);
            this.panel1.Controls.Add(this.panelResim);
            this.panel1.Controls.Add(this.labelSifre);
            this.panel1.Controls.Add(this.labelKullanıcıAdı);
            this.panel1.Controls.Add(this.buttonGiris);
            this.panel1.Controls.Add(this.textBoxPassword);
            this.panel1.Controls.Add(this.textBoxUserName);
            this.panel1.Location = new System.Drawing.Point(390, 210);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.panel1.Size = new System.Drawing.Size(500, 300);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // checkBoxSifreyiGoster
            // 
            this.checkBoxSifreyiGoster.AutoSize = true;
            this.checkBoxSifreyiGoster.Location = new System.Drawing.Point(212, 182);
            this.checkBoxSifreyiGoster.Name = "checkBoxSifreyiGoster";
            this.checkBoxSifreyiGoster.Size = new System.Drawing.Size(109, 20);
            this.checkBoxSifreyiGoster.TabIndex = 9;
            this.checkBoxSifreyiGoster.Text = "Şifreyi Göster";
            this.checkBoxSifreyiGoster.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxSifreyiGoster.UseVisualStyleBackColor = true;
            this.checkBoxSifreyiGoster.CheckedChanged += new System.EventHandler(this.checkBoxSifreyiGoster_CheckedChanged);
            // 
            // panelResim
            // 
            this.panelResim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panelResim.Controls.Add(this.labelHosgeldiniz);
            this.panelResim.Controls.Add(this.pictureBox1);
            this.panelResim.Location = new System.Drawing.Point(0, 0);
            this.panelResim.Name = "panelResim";
            this.panelResim.Size = new System.Drawing.Size(141, 299);
            this.panelResim.TabIndex = 8;
            // 
            // labelHosgeldiniz
            // 
            this.labelHosgeldiniz.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelHosgeldiniz.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelHosgeldiniz.Location = new System.Drawing.Point(0, 143);
            this.labelHosgeldiniz.Name = "labelHosgeldiniz";
            this.labelHosgeldiniz.Size = new System.Drawing.Size(138, 89);
            this.labelHosgeldiniz.TabIndex = 8;
            this.labelHosgeldiniz.Text = "8 BigStars Sistemine Hoş Geldiniz";
            this.labelHosgeldiniz.Click += new System.EventHandler(this.labelHosgeldiniz_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ImageLocation = "E:\\VSC ÇIKTI\\kod\\FabrikaVT\\Logo.png";
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(135, 137);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // labelSifre
            // 
            this.labelSifre.AutoSize = true;
            this.labelSifre.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSifre.Location = new System.Drawing.Point(209, 146);
            this.labelSifre.Name = "labelSifre";
            this.labelSifre.Size = new System.Drawing.Size(47, 16);
            this.labelSifre.TabIndex = 6;
            this.labelSifre.Text = "Şifre :";
            // 
            // labelKullanıcıAdı
            // 
            this.labelKullanıcıAdı.AutoSize = true;
            this.labelKullanıcıAdı.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelKullanıcıAdı.Location = new System.Drawing.Point(151, 106);
            this.labelKullanıcıAdı.Margin = new System.Windows.Forms.Padding(0);
            this.labelKullanıcıAdı.Name = "labelKullanıcıAdı";
            this.labelKullanıcıAdı.Size = new System.Drawing.Size(100, 16);
            this.labelKullanıcıAdı.TabIndex = 5;
            this.labelKullanıcıAdı.Text = "Kullanıcı Adı :";
            this.labelKullanıcıAdı.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonGiris
            // 
            this.buttonGiris.Location = new System.Drawing.Point(281, 219);
            this.buttonGiris.Name = "buttonGiris";
            this.buttonGiris.Size = new System.Drawing.Size(75, 30);
            this.buttonGiris.TabIndex = 2;
            this.buttonGiris.Text = "Giriş Yap";
            this.buttonGiris.UseVisualStyleBackColor = true;
            this.buttonGiris.Click += new System.EventHandler(this.buttonGiris_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Giriş Ekranı";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelResim.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonGiris;
        private System.Windows.Forms.Label labelSifre;
        private System.Windows.Forms.Label labelKullanıcıAdı;
        private System.Windows.Forms.Panel panelResim;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelHosgeldiniz;
        private System.Windows.Forms.CheckBox checkBoxSifreyiGoster;
    }
}

