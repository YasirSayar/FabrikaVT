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
    public partial class FormStaff : Form
    {
        public int staffId;
        public void SetStaffId(int id)
        {
            staffId = id;
        }
        public FormStaff()
        {
            InitializeComponent();
            customizeDesign();
        }
        private void  customizeDesign()
        {
            panelStaff.Visible = false;
            panel_customer.Visible = false;
        }
        private void hideSubMenu()
        {
            if (panelStaff.Visible==true)
            {
                panelStaff.Visible=false;
            }
            if(panel_customer.Visible==true)
            {
                panel_customer.Visible=false;
            }
            if(panelProduct.Visible==true) 
            {
                panelProduct.Visible=false;
            }
            if(panelParça.Visible==true) 
            {
                panelParça.Visible = false;
            }
            if(panelEmployee.Visible==true)
            {
                panelEmployee.Visible=false;
            }
        }
        private void showSubMenu(Panel submenu) 
        {
            if (submenu.Visible==false)
            {
                hideSubMenu();
                submenu.Visible=true;
            }
            else
                submenu.Visible=false;
        }
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if(activeForm == null) 
            { 
            activeForm = new Form();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;  
            childForm.Dock=DockStyle.Fill;

            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void button1_Click(object sender, EventArgs e)/*STAFF ANA BUTON*/
        {
            showSubMenu(panelStaff);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new Form_StaffAdd());
          //  this.Text = this.Text + "(Staff Ekle)";
           // Form_StaffAdd form_staffadd = new Form_StaffAdd();
           // form_staffadd.ShowDialog();
        }

        private void buttonStUpdate_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new Form_StaffUpdate());
            
        }

        private void buttonStDelete_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            openChildForm(new Form_staffDelete());
           // openChildForm(new Form_StaffUpdate());

        }

        private void buttoncstMain_Click(object sender, EventArgs e)
        {
            showSubMenu(panel_customer);
        }

        private void button_cstAdd_Click(object sender, EventArgs e)
        {
            hideSubMenu(); //openChildForm(new CustomerAdd());
            CustomerAdd customerAddForm = new CustomerAdd();

            // Oluşturulan formun SetStaffId metodunu çağırarak staff_id değerini set et
            customerAddForm.SetStaffId(staffId);

            // Formu göster
            openChildForm(customerAddForm);
        }

        private void button_CstUpdate_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            FormCustomerUpdate formCustomerUpdateForm = new FormCustomerUpdate();
            formCustomerUpdateForm.SetStaffId(staffId);
            openChildForm(formCustomerUpdateForm);
        }

        private void button_cstDelete_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            FormCustomerDelete formCustomerDeleteForm = new FormCustomerDelete();
            formCustomerDeleteForm.SetStaffId(staffId);
            openChildForm(formCustomerDeleteForm);
        }

        private void buttonAnaMenu_Click(object sender, EventArgs e)
        {
            customizeDesign();
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_exit_Click(object sender, EventArgs e)//BUTTON ORDERR
        {
          
            //this.Hide();

            //// Eğer aktif bir form varsa kapat
            //if (activeForm != null)
            //{
            //    activeForm.Close();
            //    activeForm = null; // Aktif formu temizle
            //}

            //// Ana menü formunu göster
            //Form1 form1 = new Form1();
            //form1.ShowDialog();

            hideSubMenu();
            FormOrderEkranı formOrderEkranı = new FormOrderEkranı();
            formOrderEkranı.SetStaffId(staffId);
            openChildForm(formOrderEkranı);
        }

        private void FormStaff_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(staffId.ToString(), "Staff ID");
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            showSubMenu(panelProduct);
        }

        private void buttonProdGüncelle_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            FormStaffProduct formStaffProduct = new FormStaffProduct();
           // formOrderEkranı.SetStaffId(staffId);
            openChildForm(formStaffProduct);
        }

        private void buttonProdSil_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            FormProductStaffSil formProductStaffSil = new FormProductStaffSil();
            // formOrderEkranı.SetStaffId(staffId);
            openChildForm(formProductStaffSil);
        }

        private void buttonProdEkle_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            FormStfProductAdd formStfProductAdd = new FormStfProductAdd();
            // formOrderEkranı.SetStaffId(staffId);
            openChildForm(formStfProductAdd);
        }

        private void buttonParçalar_Click(object sender, EventArgs e)
        {
            showSubMenu(panelParça);
        }

        private void buttonPartAdd_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            FormPartAdd formPartAdd = new FormPartAdd();
            // formOrderEkranı.SetStaffId(staffId);
            openChildForm(formPartAdd);
        }

        private void buttonPartUpdate_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            FormPartUpdate formPartUpdate = new FormPartUpdate();
            // formOrderEkranı.SetStaffId(staffId);
            openChildForm(formPartUpdate);
        }

        private void buttonPartDelete_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            FormPartDelete formPartDelete = new FormPartDelete();
            // formOrderEkranı.SetStaffId(staffId);
            openChildForm(formPartDelete);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            showSubMenu(panelEmployee);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            FormStfEmpAdd formStfEmpAdd = new FormStfEmpAdd();
            // formOrderEkranı.SetStaffId(staffId);
            openChildForm(formStfEmpAdd);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            hideSubMenu();
            FormStfEmpUpdate formStfEmpUpdate = new FormStfEmpUpdate();
            // formOrderEkranı.SetStaffId(staffId);
            openChildForm(formStfEmpUpdate);
        }
        private void button4_Click(object sender, EventArgs e)
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

        private void panelSlide_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
