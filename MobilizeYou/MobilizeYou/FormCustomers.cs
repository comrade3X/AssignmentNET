using System;
using System.Linq;
using System.Windows.Forms;
using MobilizeYou.Properties;


namespace MobilizeYou
{
    using DTO;
    using BLL;

    public partial class FormCustomers : Form
    {
        CustomersSvervices _customersSvervices = new CustomersSvervices();

        public FormCustomers()
        {
            InitializeComponent();
        }

        private void FormCustomers_Load(object sender, EventArgs e)
        {
            FillData();
        }

        private void FillData()
        {
            var listCutomers = _customersSvervices.GetAll();

            var linq = from s in listCutomers
                       select new
                       {
                           s.Id,
                           s.FullName,
                           IdentityCard = s.IdentityCardNo,
                           DriveLicence = s.DriveLicenceNo,
                           s.PhoneNumber
                       };

            dataGridViewCustomers.DataSource = linq.ToList();
            dataGridViewCustomers.Columns[0].HeaderText = Resources.FormCustomers_FillData_Id;
            dataGridViewCustomers.Columns[1].HeaderText = Resources.FormCustomers_FillData_Full_Name;
            dataGridViewCustomers.Columns[2].HeaderText = Resources.FormCustomers_FillData_Identity_Card;
            dataGridViewCustomers.Columns[3].HeaderText = Resources.FormCustomers_FillData_Drive_Licence;
            dataGridViewCustomers.Columns[4].HeaderText = Resources.FormCustomers_FillData_Phone_Number;
        }

        public override void Refresh()
        {
            textBoxId.Text = string.Empty;
            textBoxFullName.Text = string.Empty;
            textBoxIdentityCard.Text = string.Empty;
            textBoxDriveLicence.Text = string.Empty;
            textBoxPhoneNumber.Text = string.Empty;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var id = textBoxId.Text;
                var customer = _customersSvervices.GetById(Convert.ToInt32(id));
                customer.FullName = textBoxFullName.Text;
                customer.IdentityCardNo = textBoxIdentityCard.Text;
                customer.DriveLicenceNo = textBoxDriveLicence.Text;
                customer.PhoneNumber = textBoxPhoneNumber.Text;

                _customersSvervices.Update(customer);
                Refresh();
                FillData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var customer = new Customer
                {
                    FullName = textBoxFullName.Text,
                    IdentityCardNo = textBoxIdentityCard.Text,
                    DriveLicenceNo = textBoxDriveLicence.Text,
                    PhoneNumber = textBoxPhoneNumber.Text
                };
                _customersSvervices.Add(customer);
                Refresh();
                FillData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewCustomers.CurrentRow == null) return;
                var customerId = Convert.ToInt32(dataGridViewCustomers.CurrentRow.Cells[0].Value);
                var customer = _customersSvervices.GetById(customerId);
                customer.Orders = null;

                var dr = MessageBox.Show(Resources.FormCustomers_deleteToolStripMenuItem_Click_Are_you_sure_, Resources.FormProduct_deleteToolStripMenuItem_Click_Warning, MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    _customersSvervices.Delete(customer);
                    Refresh();
                }
                FillData();
                MessageBox.Show(Resources.FormProduct_deleteToolStripMenuItem_Click_Delete_Successfully_);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var search = textBoxSearch.Text.ToLower();
            var list = _customersSvervices.GetAll();
            var listCustomers = !string.IsNullOrEmpty(search)
               ? list.Where(x => x.FullName.ToLower().Contains(search)).ToList()
               : list.ToList();
            var linq = from s in listCustomers
                       select new
                       {
                           s.Id,
                           s.FullName,
                           IdentityCard = s.IdentityCardNo,
                           DriveLicence = s.DriveLicenceNo,
                           s.PhoneNumber
                       };
            dataGridViewCustomers.DataSource = linq.ToList();
        }

        private void dataGridViewCustomers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewCustomers.CurrentRow == null) return;
            var customerId = Convert.ToInt32(dataGridViewCustomers.CurrentRow.Cells[0].Value);
            var customer = _customersSvervices.GetById(customerId);

            textBoxId.Text = customer.Id.ToString();
            textBoxFullName.Text = customer.FullName;
            textBoxIdentityCard.Text = customer.IdentityCardNo;
            textBoxDriveLicence.Text = customer.DriveLicenceNo;
            textBoxPhoneNumber.Text = customer.PhoneNumber;
        }
    }
}
