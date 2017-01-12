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
using MobilizeYou.BLL;
using MobilizeYou.DTO;

namespace MobilizeYou
{
    public partial class FormOrderBilling : Form
    {
        public FormOrderBilling(Order order)
        {
            InitializeComponent();
            EmployeeServices services= new EmployeeServices();
            var emp = services.GetById(order.Seller);

            labelCustomerName.Text = order.Customer.FullName;
            labelPhone.Text = order.Customer.PhoneNumber;
            labelOrderId.Text = order.Id.ToString();
            labelSeller.Text = emp.FullName;
            labelCreatedDate.Text = order.CreatedDate.ToString("dd MMM yyyy");
            labelTotal.Text = @"$" + order.TotalPrice.ToString(CultureInfo.InvariantCulture);
        }
    }
}
