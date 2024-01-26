namespace FabrikaVT
{
    partial class FormEmployee
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelSlide = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonOrder = new System.Windows.Forms.Button();
            this.panel_customer = new System.Windows.Forms.Panel();
            this.button_cstDelete = new System.Windows.Forms.Button();
            this.button_CstUpdate = new System.Windows.Forms.Button();
            this.button_cstAdd = new System.Windows.Forms.Button();
            this.buttoncstMain = new System.Windows.Forms.Button();
            this.panelStaff = new System.Windows.Forms.Panel();
            this.buttonStDelete = new System.Windows.Forms.Button();
            this.buttonStUpdate = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonAnaMenu = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.panelSlide.SuspendLayout();
            this.panel_customer.SuspendLayout();
            this.panelStaff.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMain.Location = new System.Drawing.Point(220, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1042, 673);
            this.panelMain.TabIndex = 3;
            // 
            // panelSlide
            // 
            this.panelSlide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(15)))), ((int)(((byte)(210)))));
            this.panelSlide.Controls.Add(this.button3);
            this.panelSlide.Controls.Add(this.buttonOrder);
            this.panelSlide.Controls.Add(this.panel_customer);
            this.panelSlide.Controls.Add(this.buttoncstMain);
            this.panelSlide.Controls.Add(this.panelStaff);
            this.panelSlide.Controls.Add(this.button1);
            this.panelSlide.Controls.Add(this.buttonAnaMenu);
            this.panelSlide.Controls.Add(this.panelLogo);
            this.panelSlide.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSlide.Location = new System.Drawing.Point(0, 0);
            this.panelSlide.Name = "panelSlide";
            this.panelSlide.Size = new System.Drawing.Size(220, 673);
            this.panelSlide.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Top;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button3.ForeColor = System.Drawing.Color.Yellow;
            this.button3.Location = new System.Drawing.Point(0, 631);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(220, 50);
            this.button3.TabIndex = 10;
            this.button3.Text = "Çıkış";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonOrder
            // 
            this.buttonOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOrder.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonOrder.ForeColor = System.Drawing.Color.Yellow;
            this.buttonOrder.Location = new System.Drawing.Point(0, 581);
            this.buttonOrder.Name = "buttonOrder";
            this.buttonOrder.Size = new System.Drawing.Size(220, 50);
            this.buttonOrder.TabIndex = 9;
            this.buttonOrder.Text = "Siparişler";
            this.buttonOrder.UseVisualStyleBackColor = true;
            this.buttonOrder.Visible = false;
            // 
            // panel_customer
            // 
            this.panel_customer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(13)))), ((int)(((byte)(181)))));
            this.panel_customer.Controls.Add(this.button_cstDelete);
            this.panel_customer.Controls.Add(this.button_CstUpdate);
            this.panel_customer.Controls.Add(this.button_cstAdd);
            this.panel_customer.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_customer.Location = new System.Drawing.Point(0, 429);
            this.panel_customer.Name = "panel_customer";
            this.panel_customer.Size = new System.Drawing.Size(220, 152);
            this.panel_customer.TabIndex = 6;
            // 
            // button_cstDelete
            // 
            this.button_cstDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(13)))), ((int)(((byte)(181)))));
            this.button_cstDelete.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_cstDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cstDelete.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_cstDelete.ForeColor = System.Drawing.Color.Yellow;
            this.button_cstDelete.Location = new System.Drawing.Point(0, 100);
            this.button_cstDelete.Name = "button_cstDelete";
            this.button_cstDelete.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.button_cstDelete.Size = new System.Drawing.Size(220, 50);
            this.button_cstDelete.TabIndex = 6;
            this.button_cstDelete.Text = "Müşteri Sil";
            this.button_cstDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_cstDelete.UseVisualStyleBackColor = false;
            // 
            // button_CstUpdate
            // 
            this.button_CstUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(13)))), ((int)(((byte)(181)))));
            this.button_CstUpdate.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_CstUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_CstUpdate.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_CstUpdate.ForeColor = System.Drawing.Color.Yellow;
            this.button_CstUpdate.Location = new System.Drawing.Point(0, 50);
            this.button_CstUpdate.Name = "button_CstUpdate";
            this.button_CstUpdate.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.button_CstUpdate.Size = new System.Drawing.Size(220, 50);
            this.button_CstUpdate.TabIndex = 5;
            this.button_CstUpdate.Text = "Müşteri Güncelle";
            this.button_CstUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_CstUpdate.UseVisualStyleBackColor = false;
            // 
            // button_cstAdd
            // 
            this.button_cstAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(13)))), ((int)(((byte)(181)))));
            this.button_cstAdd.Dock = System.Windows.Forms.DockStyle.Top;
            this.button_cstAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cstAdd.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_cstAdd.ForeColor = System.Drawing.Color.Yellow;
            this.button_cstAdd.Location = new System.Drawing.Point(0, 0);
            this.button_cstAdd.Name = "button_cstAdd";
            this.button_cstAdd.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.button_cstAdd.Size = new System.Drawing.Size(220, 50);
            this.button_cstAdd.TabIndex = 4;
            this.button_cstAdd.Text = "Müşteri Ekle";
            this.button_cstAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_cstAdd.UseVisualStyleBackColor = false;
            // 
            // buttoncstMain
            // 
            this.buttoncstMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttoncstMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttoncstMain.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttoncstMain.ForeColor = System.Drawing.Color.Yellow;
            this.buttoncstMain.Location = new System.Drawing.Point(0, 379);
            this.buttoncstMain.Name = "buttoncstMain";
            this.buttoncstMain.Size = new System.Drawing.Size(220, 50);
            this.buttoncstMain.TabIndex = 5;
            this.buttoncstMain.Text = "Parça";
            this.buttoncstMain.UseVisualStyleBackColor = true;
            this.buttoncstMain.Click += new System.EventHandler(this.buttoncstMain_Click);
            // 
            // panelStaff
            // 
            this.panelStaff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(13)))), ((int)(((byte)(181)))));
            this.panelStaff.Controls.Add(this.buttonStDelete);
            this.panelStaff.Controls.Add(this.buttonStUpdate);
            this.panelStaff.Controls.Add(this.button2);
            this.panelStaff.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStaff.Location = new System.Drawing.Point(0, 227);
            this.panelStaff.Name = "panelStaff";
            this.panelStaff.Size = new System.Drawing.Size(220, 152);
            this.panelStaff.TabIndex = 4;
            // 
            // buttonStDelete
            // 
            this.buttonStDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(13)))), ((int)(((byte)(181)))));
            this.buttonStDelete.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonStDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStDelete.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonStDelete.ForeColor = System.Drawing.Color.Yellow;
            this.buttonStDelete.Location = new System.Drawing.Point(0, 100);
            this.buttonStDelete.Name = "buttonStDelete";
            this.buttonStDelete.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.buttonStDelete.Size = new System.Drawing.Size(220, 50);
            this.buttonStDelete.TabIndex = 6;
            this.buttonStDelete.Text = "Personel Sil";
            this.buttonStDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonStDelete.UseVisualStyleBackColor = false;
            // 
            // buttonStUpdate
            // 
            this.buttonStUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(13)))), ((int)(((byte)(181)))));
            this.buttonStUpdate.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonStUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStUpdate.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonStUpdate.ForeColor = System.Drawing.Color.Yellow;
            this.buttonStUpdate.Location = new System.Drawing.Point(0, 50);
            this.buttonStUpdate.Name = "buttonStUpdate";
            this.buttonStUpdate.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.buttonStUpdate.Size = new System.Drawing.Size(220, 50);
            this.buttonStUpdate.TabIndex = 5;
            this.buttonStUpdate.Text = "Personel Güncelle";
            this.buttonStUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonStUpdate.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(13)))), ((int)(((byte)(181)))));
            this.button2.Dock = System.Windows.Forms.DockStyle.Top;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.ForeColor = System.Drawing.Color.Yellow;
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.button2.Size = new System.Drawing.Size(220, 50);
            this.button2.TabIndex = 4;
            this.button2.Text = "Personel Ekle";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.ForeColor = System.Drawing.Color.Yellow;
            this.button1.Location = new System.Drawing.Point(0, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(220, 50);
            this.button1.TabIndex = 3;
            this.button1.Text = "Ürün";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonAnaMenu
            // 
            this.buttonAnaMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonAnaMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAnaMenu.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonAnaMenu.ForeColor = System.Drawing.Color.Yellow;
            this.buttonAnaMenu.Location = new System.Drawing.Point(0, 127);
            this.buttonAnaMenu.Name = "buttonAnaMenu";
            this.buttonAnaMenu.Size = new System.Drawing.Size(220, 50);
            this.buttonAnaMenu.TabIndex = 1;
            this.buttonAnaMenu.Text = "Ana Menü";
            this.buttonAnaMenu.UseVisualStyleBackColor = true;
            // 
            // panelLogo
            // 
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(220, 127);
            this.panelLogo.TabIndex = 0;
            // 
            // FormEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelSlide);
            this.Name = "FormEmployee";
            this.Text = "FormEmployee";
            this.Load += new System.EventHandler(this.FormEmployee_Load);
            this.panelSlide.ResumeLayout(false);
            this.panel_customer.ResumeLayout(false);
            this.panelStaff.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelSlide;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonOrder;
        private System.Windows.Forms.Panel panel_customer;
        private System.Windows.Forms.Button button_cstDelete;
        private System.Windows.Forms.Button button_CstUpdate;
        private System.Windows.Forms.Button button_cstAdd;
        private System.Windows.Forms.Button buttoncstMain;
        private System.Windows.Forms.Panel panelStaff;
        private System.Windows.Forms.Button buttonStDelete;
        private System.Windows.Forms.Button buttonStUpdate;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonAnaMenu;
        private System.Windows.Forms.Panel panelLogo;
    }
}