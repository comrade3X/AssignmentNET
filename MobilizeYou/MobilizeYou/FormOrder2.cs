using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MobilizeYou.DTO;
using MobilizeYou.Properties;


namespace MobilizeYou
{
    using BLL;
    public partial class FormOrder2 : Form
    {
        public CategoriesServices CategoriesServices;
        public ProductServices ProductServices;
        public OrderDetailsServices OrderDetailsServices;
        public CustomersSvervices CustomersSvervices;
        List<OrderDetailsView> _listOrderDetails = new List<OrderDetailsView>();
        public EmployeeServices EmployeeServices;
        public OrderServices OrderServices;

        private int _empId = 0;

        public FormOrder2(int customerId)
        {
            InitializeComponent();
            _empId = customerId;
            CategoriesServices = new CategoriesServices();
            ProductServices = new ProductServices();
            OrderDetailsServices = new OrderDetailsServices();
            CustomersSvervices = new CustomersSvervices();
            EmployeeServices = new EmployeeServices();
            OrderServices = new OrderServices();

            FillData();
        }

        private void comboBoxCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CascadeCb(comboBoxCategory.Text);
        }


        #region -- Helper -- 

        /// <summary>
        /// Fill data to all controls in form.
        /// </summary>
        private void FillData()
        {
            try
            {
                var listCategory = CategoriesServices.GetAll();
                listCategory.Insert(0, new Category { Name = "All" });
                comboBoxCategory.DataSource = listCategory;
                comboBoxCategory.DisplayMember = "Name";
                comboBoxCategory.ValueMember = "Id";

                CascadeCb(null);
                dataGridViewOrderDetails.DataSource = _listOrderDetails;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Fill data to combobox "Make" by category
        /// </summary>
        /// <param name="cat"></param>
        private void CascadeCb(string cat)
        {
            List<string> listMakeByCate;

            if (string.IsNullOrEmpty(cat))
            {
                listMakeByCate = (from s in ProductServices.GetAll()
                                  select s.Make).Distinct().OrderBy(x => x).ToList();
            }
            else
            {

                listMakeByCate = (from s in ProductServices.GetAll()
                                  where s.Category.Name == cat
                                  select s.Make).Distinct().OrderBy(x => x).ToList();
            }

            // If data not found, display text "No items."
            if (listMakeByCate.Count == 0)
            {
                listMakeByCate.Add("No items.");
            }
            else
            {
                listMakeByCate.Insert(0, "All");
            }
            comboBoxMake.DataSource = listMakeByCate;
        }

        public override void Refresh()
        {
            dataGridViewSearchResults.DataSource = null;
            dataGridViewOrderDetails.DataSource = null;
            comboBoxCategory.SelectedIndex = 0;
            comboBoxMake.SelectedIndex = 0;
            textBoxName.Text = string.Empty;
            dateTimePickerFrom.Value = DateTime.Now;
            dateTimePickerTo.Value = DateTime.Now;
        }

        public void RefreshAll()
        {
            Refresh();
            textBoxFullName.Text = string.Empty;
            textBoxIdentityCard.Text = string.Empty;
            textBoxDriveLicence.Text = string.Empty;
            textBoxPhoneNumber.Text = string.Empty;
            _listOrderDetails = null;
        }

        /// <summary>
        /// Validate input customer information
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="identityCard"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        private bool Validate(string fullName, string identityCard, string phone)
        {
            var validate = new List<string> { fullName, identityCard, phone };
            if (validate.Any(x => x.Equals(string.Empty)))
            {
                MessageBox.Show(@"Please enter all fields mandatory (with""*"").",
                   "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (!Helper.IsPhoneNumber(phone))
            {
                MessageBox.Show(@"Please enter valid phone number",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            return true;
        }

        #endregion

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            try
            {
                var dateFr = dateTimePickerFrom.Value;
                var dateTo = dateTimePickerTo.Value;
                var category = comboBoxCategory.Text;
                var make = comboBoxMake.Text;
                var name = textBoxName.Text;

                var searchResults = OrderDetailsServices.Search(dateFr, dateTo, category, make, name, null);

                var linq = from s in searchResults
                           select new
                           {
                               s.Status,
                               s.Product.Name,
                               Type = s.Product.Category.Name,
                               s.Product.RentPerDay,
                               ProductId = s.Product.Id,
                               s.From,
                               s.To,
                               s.Customer,
                               s.OrderDate
                           };

                dataGridViewSearchResults.DataSource = linq.ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewSearchResults.CurrentRow != null)
                {
                    var status = dataGridViewSearchResults.CurrentRow.Cells[0].Value;
                    if ("Hired".Equals(status))
                    {
                        MessageBox.Show(Resources.FormOrder2_addOrderToolStripMenuItem_Click_Cannot_order_this_items);
                        return;
                    }

                    var productId = Convert.ToInt32(dataGridViewSearchResults.CurrentRow.Cells[4].Value);
                    var product = ProductServices.GetById(productId);
                    var dateFr = dateTimePickerFrom.Value;
                    var dateTo = dateTimePickerTo.Value;

                    var orderDetailItems = new OrderDetailsView
                    {
                        ProductName = product.Name,
                        Type = product.Category.Name,
                        RentPerDay = product.RentPerDay,
                        ProductId = product.Id,
                        From = dateFr,
                        To = dateTo
                    };

                    // Refresh dataGridView
                    _listOrderDetails.Add(orderDetailItems);
                    dataGridViewOrderDetails.DataSource = null;
                    dataGridViewOrderDetails.DataSource = _listOrderDetails;
                }
            }
            catch
            {
                MessageBox.Show(Resources.FormLogin_buttonLogin_Click_Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void buttonCheckOut_Click(object sender, EventArgs e)
        {
            var fullName = textBoxFullName.Text;
            var identityCard = textBoxIdentityCard.Text;
            var driveLicence = textBoxDriveLicence.Text;
            var phone = textBoxPhoneNumber.Text;

            //Validate
            var fieldValid = Validate(fullName, identityCard, phone);
            if (!fieldValid)
            {
                return;
            }

            var orderDetailsValid = _listOrderDetails.Count > 0;
            if (!orderDetailsValid)
            {
                MessageBox.Show(@"Please add items to order list.");
                return;
            }

            try
            {
                var list = (List<OrderDetailsView>)dataGridViewOrderDetails.DataSource;
                var customer = new Customer
                {
                    FullName = fullName,
                    IdentityCardNo = identityCard,
                    DriveLicenceNo = driveLicence,
                    PhoneNumber = phone
                };

                var totalPrice = list.Sum(x => x.RentPerDay);

                var listOrderDetails = list.Select(item => new OrderDetail
                {
                    ProductId = item.ProductId,
                    ValidFrom = item.From,
                    ValidTo = item.To
                }).ToList();

                var order = new Order
                {
                    Customer = customer,
                    Seller = _empId,
                    CreatedDate = DateTime.Now,
                    TotalPrice = totalPrice,
                    OrderDetails = listOrderDetails
                };

                OrderServices.Add(order);
                FormOrderBilling frmOrderBilling = new FormOrderBilling(order);
                frmOrderBilling.StartPosition = FormStartPosition.CenterScreen;
                frmOrderBilling.Show();
                RefreshAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
