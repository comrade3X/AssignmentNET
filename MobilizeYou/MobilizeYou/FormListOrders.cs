using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MobilizeYou.Properties;

namespace MobilizeYou
{
    using DTO;
    using BLL;
    public partial class FormListOrders : Form
    {
        public EmployeeServices EmployeeServices;
        public OrderDetailsServices OrderDetailsServices;
        public OrderServices OrderServices;

        public FormListOrders()
        {
            InitializeComponent();
            EmployeeServices = new EmployeeServices();
            OrderDetailsServices = new OrderDetailsServices();
            OrderServices = new OrderServices();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var customerInfo = textBoxCsutomerInfo.Text;
                var dateFrom = dateTimePickerFrom.Value;
                var dateTo = dateTimePickerTo.Value;
                var employeeId = Convert.ToInt32(comboBoxSeller.SelectedValue);

                var searchResults = OrderDetailsServices.Search2(dateFrom, dateTo, employeeId, customerInfo);

                var dataGridViewData = from s in searchResults
                                       let product = s.Product
                                       select new OrderView
                                       {
                                           OrderId = s.Order.Id,
                                           Customer = s.Order.Customer.FullName,
                                           TotalAmount = @"$" + " " + s.Order.TotalPrice,
                                           CreatedDate = s.Order.CreatedDate.ToString("dd MMM yyyy"),
                                           Seller = s.Order.Employee.FullName
                                       };
                dataGridViewSearch.DataSource = dataGridViewData.ToList();
            }
            catch
            {
                MessageBox.Show(Resources.FormLogin_buttonLogin_Click_Error);
            }
        }

        private void FormListOrders_Load(object sender, EventArgs e)
        {
            FillData();
        }

        private void FillData()
        {
            var listEmployee = EmployeeServices.GetAll();
            listEmployee.Insert(0, new Employee { FullName = "All", Id = 0 });
            comboBoxSeller.DataSource = listEmployee;
            comboBoxSeller.DisplayMember = "FullName";
            comboBoxSeller.ValueMember = "Id";


        }

        private void RefreshForm()
        {
            textBoxCsutomerInfo.Text = string.Empty;
            comboBoxSeller.SelectedIndex = 0;
            dateTimePickerFrom.Value = DateTime.Now;
            dateTimePickerTo.Value = DateTime.Now;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void viewDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                if (dataGridViewSearch.CurrentRow != null)
                {
                    var orderId = Convert.ToInt32(dataGridViewSearch.CurrentRow.Cells[0].Value);
                    var order = OrderServices.GetById(orderId);
                    var orderDetail = OrderDetailsServices.GetByOrderId(orderId);

                    if (orderDetail != null && order != null)
                    {
                        labelCustomername.Text = order.Customer.FullName;
                        labelPhone.Text = order.Customer.PhoneNumber;
                        labelOrderId.Text = order.Id.ToString();
                        labelSeller.Text = order.Employee.FullName;
                        labelCreateDate.Text = order.CreatedDate.ToString("dd MMM yyyy");
                        labelTotalPrice.Text = @"$" + @" " + order.TotalPrice;

                        var linq = from s in orderDetail
                                   let product = s.Product
                                   select new
                                   {
                                       ProductName = product.Name,
                                       product.Make,
                                       product.Model,
                                       Price = @"$" + product.RentPerDay,
                                       From = s.ValidFrom.ToString("dd MMM yyyy"),
                                       To = s.ValidTo.ToString("dd MMM yyyy")
                                   };
                        dataGridViewProductDetail.DataSource = linq.ToList();
                    }
                }
            }
            catch
            {
                MessageBox.Show(Resources.FormLogin_buttonLogin_Click_Error);
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
