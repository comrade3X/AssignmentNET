using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MobilizeYou.Properties;

namespace MobilizeYou
{
    using DTO;
    using BLL;

    public partial class FormEmployee : Form
    {
        EmployeeServices _employeeServices = new EmployeeServices();
        public FormEmployee()
        {
            InitializeComponent();
        }

        private void FormEmployee_Load(object sender, EventArgs e)
        {
            FillData();
        }

        private void FillData()
        {
            var listEmployee = _employeeServices.GetAll();
            var linq = from s in listEmployee
                       select new
                       {
                           s.Id,
                           s.FullName,
                           Dob = s.Dob.ToString("dd MMM yyyy"),
                           s.Sex,
                           s.Address,
                           s.PhoneNumber,
                           s.JobType
                       };
            dataGridViewEmployee.DataSource = linq.ToList();

            // Fill data to compbobox JobType
            var dic = new Dictionary<int, string>
            {
                {1, "Staff"},
                {2, "Manager"}
            };
            comboBoxJobType.DataSource = dic.ToList();
            comboBoxJobType.ValueMember = "Key";
            comboBoxJobType.DisplayMember = "Value";
        }

        public override void Refresh()
        {
            textBoxId.Text = string.Empty;
            textBoxFullName.Text = string.Empty;
            dateTimePickerDob.Value = DateTime.Today;
            radioButtonMale.PerformClick();
            numericUpDownPhoneNumber.Text = Resources.FormEmployee_Refresh__0;
            richTextBoxAddress.Text = string.Empty;
            comboBoxJobType.SelectedIndex = 0;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var employee = new Employee()
                {
                    FullName = textBoxFullName.Text,
                    Address = richTextBoxAddress.Text,
                    PhoneNumber = numericUpDownPhoneNumber.Text,
                    JobType = comboBoxJobType.Text,
                    Dob = dateTimePickerDob.Value
                };

                var sex = string.Empty;
                foreach (var control in panel1.Controls)
                {
                    var rd = (RadioButton)control;
                    if (rd.Checked)
                    {
                        sex = rd.Text;
                    }
                }

                employee.Sex = sex;
                _employeeServices.Add(employee);
                Refresh();
                FillData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var employeeId = Convert.ToInt32(textBoxId.Text);
                var employee = _employeeServices.GetById(employeeId);

                employee.FullName = textBoxFullName.Text;
                employee.Address = richTextBoxAddress.Text;
                employee.PhoneNumber = numericUpDownPhoneNumber.Text;
                employee.JobType = comboBoxJobType.Text;
                employee.Dob = dateTimePickerDob.Value;
                foreach (var control in panel1.Controls)
                {
                    var rd = (RadioButton)control;
                    if (rd.Checked)
                    {
                        employee.Sex = rd.Text;
                    }
                }

                _employeeServices.Update(employee);
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
                if (dataGridViewEmployee.CurrentRow == null) return;
                var employeeId = Convert.ToInt32(dataGridViewEmployee.CurrentRow.Cells[0].Value);
                var employee = _employeeServices.GetById(employeeId);
                employee.Orders = null;
                employee.Memberships = null;

                var dr = MessageBox.Show(Resources.FormProduct_deleteToolStripMenuItem_Click_Are_you_sure_, Resources.FormProduct_deleteToolStripMenuItem_Click_Warning, MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    _employeeServices.Delete(employee);
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
            var list = _employeeServices.GetAll();
            var listEmp = !string.IsNullOrEmpty(search)
               ? list.Where(x => x.FullName.Contains(search)).ToList()
               : list.ToList();
            var linq = from s in listEmp
                       select new
                       {
                           s.Id,
                           s.FullName,
                           Dob = s.Dob.ToString("dd MMM yyyy"),
                           s.Sex,
                           s.Address,
                           s.PhoneNumber,
                           s.JobType
                       };
            dataGridViewEmployee.DataSource = linq.ToList();
        }

        private void dataGridViewEmployee_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dataGridViewEmployee.CurrentRow == null) return;

                var employeeId = Convert.ToInt32(dataGridViewEmployee.CurrentRow.Cells[0].Value);
                var employee = _employeeServices.GetById(employeeId);

                textBoxId.Text = employee.Id.ToString();
                textBoxFullName.Text = employee.FullName;
                dateTimePickerDob.Value = employee.Dob;
                richTextBoxAddress.Text = employee.Address;
                comboBoxJobType.Text = employee.JobType;
                foreach (var control in panel1.Controls)
                {
                    var rd = (RadioButton)control;
                    if (rd.Text.Equals(employee.Sex))
                    {
                        rd.PerformClick();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
