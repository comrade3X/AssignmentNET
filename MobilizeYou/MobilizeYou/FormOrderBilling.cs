using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using MobilizeYou.BLL;
using MobilizeYou.DTO;

namespace MobilizeYou
{
    public partial class FormOrderBilling : Form
    {
        EmployeeServices EmployeeServices = new EmployeeServices();
        ProductServices ProductServices = new ProductServices();
        public FormOrderBilling(Order order)
        {
            InitializeComponent();

            var emp = EmployeeServices.GetById(order.Seller);

            labelCustomerName.Text = order.Customer.FullName;
            labelPhone.Text = order.Customer.PhoneNumber;
            labelOrderId.Text = order.Id.ToString();
            labelSeller.Text = emp.FullName;
            labelCreatedDate.Text = order.CreatedDate.ToString("dd MMM yyyy");
            labelTotal.Text = @"$" + order.TotalPrice.ToString(CultureInfo.InvariantCulture);

            var linq = from s in order.OrderDetails.ToList()
                       let product = ProductServices.GetById(s.ProductId)
                       select new
                       {
                           ProductName = product.Name,
                           product.Make,
                           product.Model,
                           Price = @"$" + product.RentPerDay,
                           From = s.ValidFrom.ToString("dd MMM yyyy"),
                           To = s.ValidTo.ToString("dd MMM yyyy")
                       };

            dataGridViewProducts.DataSource = linq.ToList();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
