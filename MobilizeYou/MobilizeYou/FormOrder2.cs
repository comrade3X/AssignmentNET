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
        List<OrderDetailsDto> _listOrderDetails = new List<OrderDetailsDto>();

        public FormOrder2()
        {
            InitializeComponent();

            CategoriesServices = new CategoriesServices();
            ProductServices = new ProductServices();
            OrderDetailsServices = new OrderDetailsServices();

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

                var searchResults = OrderDetailsServices.Search(dateFr, dateTo, category, make, name);

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

                    var orderDetailItems = new OrderDetailsDto
                    {
                        Product = product,
                        From = dateFr.ToString("dd MMM yyyy"),
                        To = dateTo.ToString("dd MMM yyyy")
                    };

                    // Refresh dataGridView
                    _listOrderDetails.Add(orderDetailItems);
                    var linq = from s in _listOrderDetails
                               select new
                               {
                                   s.Product.Name,
                                   Type = s.Product.Category.Name,
                                   s.Product.RentPerDay,
                                   ProductId = s.Product.Id,
                                   s.From,
                                   s.To
                               };
                    dataGridViewOrderDetails.DataSource = null;
                    dataGridViewOrderDetails.DataSource = linq.ToList();
                }
            }
            catch
            {
                MessageBox.Show(Resources.FormLogin_buttonLogin_Click_Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewSearchResults.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string value = cell.Value.ToString();
                    MessageBox.Show(value);
                }
            }
        }
    }
}
