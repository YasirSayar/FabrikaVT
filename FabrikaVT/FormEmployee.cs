using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FabrikaVT
{
    public partial class FormEmployee : Form
    {
        public int customerId;
        public void SetCustomerId(int id)
        {
            customerId = id;
        }
        public FormEmployee()
        {
            InitializeComponent();
            customizeDesign();
        }
        private void customizeDesign()
        {
            panelStaff.Visible = false;
            panel_customer.Visible = false;
        }
        private void hideSubMenu()
        {
            if (panelStaff.Visible == true)
            {
                panelStaff.Visible = false;
            }
            if (panel_customer.Visible == true)
            {
                panel_customer.Visible = false;
            }
        }
        private void showSubMenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSubMenu();
                submenu.Visible = true;
            }
            else
                submenu.Visible = false;
        }
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm == null)
            {
                activeForm = new Form();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void FormEmployee_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new FormProductE());
        }

        private void buttoncstMain_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new FormPartE());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
                this.Hide();

                // Eğer aktif bir form varsa kapat
                if (activeForm != null)
                {
                    activeForm.Close();
                    activeForm = null; // Aktif formu temizle
                }

                // Ana menü formunu göster
                Form1 form1 = new Form1();
                form1.ShowDialog();
            
        }

        private void buttonOrder_Click(object sender, EventArgs e)
        {

        }
    }
}
