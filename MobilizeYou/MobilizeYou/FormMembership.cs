using System;
using System.Linq;
using System.Windows.Forms;
using MobilizeYou.Properties;

namespace MobilizeYou
{
    using DTO;
    using BLL;

    public partial class FormMembership : Form
    {
        MembershipServices _membershipServices = new MembershipServices();
        EmployeeServices _employeeServices = new EmployeeServices();
        RoleServices _roleServices = new RoleServices();

        public FormMembership()
        {
            InitializeComponent();
        }

        private void FormMembership_Load(object sender, EventArgs e)
        {
            FillData();
        }

        private void FillData()
        {
            var listMembership = _membershipServices.GetAll();
            var linq = from s in listMembership
                       select new
                       {
                           s.Id,
                           s.Username,
                           s.Password,
                           Role = s.Role.Name,
                           Employee = s.Employee.FullName
                       };
            dataGridView1.DataSource = linq.ToList();

            // Load data employee
            var listEmployee = _employeeServices.GetAll();
            comboBoxEmployee.DataSource = listEmployee;
            comboBoxEmployee.DisplayMember = "FullName";
            comboBoxEmployee.ValueMember = "Id";

            var listRole = _roleServices.GetAll();
            comboBoxRole.DataSource = listRole;
            comboBoxRole.DisplayMember = "Name";
            comboBoxRole.ValueMember = "Id";
        }

        public override void Refresh()
        {
            textBoxId.Text = string.Empty;
            textBoxUsername.Text = string.Empty;
            textBoxPassword.Text = string.Empty;
            comboBoxEmployee.SelectedIndex = 0;
            comboBoxRole.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var membership = new Membership
                {
                    Username = textBoxUsername.Text,
                    Password = textBoxPassword.Text,
                    RoleId = Convert.ToInt32(comboBoxRole.SelectedValue),
                    EmployeeId = Convert.ToInt32(comboBoxEmployee.SelectedValue)
                };
                _membershipServices.Add(membership);
                Refresh();
                FillData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var membership = _membershipServices.GetById(Convert.ToInt32(textBoxId.Text));
                membership.Username = textBoxUsername.Text;
                membership.Password = textBoxPassword.Text;
                membership.EmployeeId = Convert.ToInt32(comboBoxEmployee.SelectedValue);
                membership.RoleId = Convert.ToInt32(comboBoxRole.SelectedValue);

                _membershipServices.Update(membership);
                Refresh();
                FillData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null) return;

                var membershipId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                var membership = _membershipServices.GetById(membershipId);

                textBoxId.Text = membership.Id.ToString();
                textBoxUsername.Text = membership.Username;
                textBoxPassword.Text = membership.Password;
                comboBoxEmployee.Text = membership.Employee.FullName;
                comboBoxRole.Text = membership.Role.Name;
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
                var dataGridViewRow = dataGridView1.CurrentRow;
                if (dataGridViewRow == null) return;

                var membershipId = Convert.ToInt32(dataGridViewRow.Cells[0].Value);
                var membership = _membershipServices.GetById(membershipId);
                membership.Employee = null;
                membership.Role = null;

                var dr = MessageBox.Show(Resources.FormProduct_deleteToolStripMenuItem_Click_Are_you_sure_,
                    Resources.FormProduct_deleteToolStripMenuItem_Click_Warning, MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    _membershipServices.Delete(membership);
                }
                FillData();
                MessageBox.Show(Resources.FormProduct_deleteToolStripMenuItem_Click_Delete_Successfully_);
            }
            catch
            {
                MessageBox.Show(Resources.FormProduct_deleteToolStripMenuItem_Click_Delete_Fail_);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var search = textBoxSearch.Text;
            var list = _membershipServices.GetAll();

            var listMem = !string.IsNullOrEmpty(search)
               ? list.Where(x => x.Username.Contains(search)).ToList()
               : list.ToList();

            var linq = from s in listMem
                       select new
                       {
                           s.Id,
                           s.Username,
                           s.Password,
                           Role = s.Role.Name,
                           Employee = s.Employee.FullName
                       };
            dataGridView1.DataSource = linq.ToList();
        }
    }
}
