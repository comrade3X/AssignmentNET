using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

using MobilizeYou.Properties;

namespace MobilizeYou
{
    using DTO;
    using BLL;
    public partial class FormProduct : Form
    {
        ProductServices _productServices = new ProductServices();
        CategoriesServices _categoriesServices = new CategoriesServices();

        public FormProduct()
        {
            InitializeComponent();
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            FillData();

            //Load data to year combobox
            var listYear = new List<int>();
            for (var i = 1990; i <= 2016; i++)
            {
                listYear.Add(i);
            }
            comboBoxYear.DataSource = listYear;
        }

        private void FillData()
        {
            var listProduct = _productServices.GetAll();
            var linq = from s in listProduct
                       select new
                       {
                           s.Id,
                           Category = s.Category.Name,
                           s.Name,
                           s.Make,
                           s.Model,
                           s.YearOfRegistion,
                           s.AddOns,
                           RentPrice = s.RentPerDay
                       };
            dataGridView1.DataSource = linq.ToList();

            var listCat = _categoriesServices.GetAll();
            comboBoxCategory.DataSource = listCat;
            comboBoxCategory.ValueMember = "Id";
            comboBoxCategory.DisplayMember = "Name";
        }

        public override void Refresh()
        {
            textBoxId.Text = string.Empty;
            textBoxName.Text = string.Empty;
            textBoxMake.Text = string.Empty;
            textBoxModel.Text = string.Empty;
            textBoxAddons.Text = string.Empty;
            comboBoxCategory.SelectedIndex = 0;
            numericBoxRentPrice.Text = Resources.FormProduct_Refresh__0_0;
            comboBoxYear.SelectedIndex = 0;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var product = new Product()
                {
                    CategoryId = Convert.ToInt32(comboBoxCategory.SelectedValue),
                    Name = textBoxName.Text,
                    Make = textBoxMake.Text,
                    Model = textBoxModel.Text,
                    YearOfRegistion = comboBoxYear.SelectedValue.ToString(),
                    AddOns = textBoxAddons.Text,
                    RentPerDay = Convert.ToDecimal(numericBoxRentPrice.Text)
                };

                _productServices.Add(product);
                Refresh();
                FillData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var product = _productServices.GetById(Convert.ToInt32(textBoxId.Text));
                product.Name = textBoxName.Text;
                product.Make = textBoxMake.Text;
                product.Model = textBoxModel.Text;
                product.AddOns = textBoxAddons.Text;
                product.RentPerDay = Convert.ToDecimal(numericBoxRentPrice.Text);
                product.YearOfRegistion = comboBoxYear.SelectedValue.ToString();
                product.Category = new Category { Id = Convert.ToInt32(comboBoxCategory.SelectedValue) };

                _productServices.Update(product);
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
                var dataGridViewRow = dataGridView1.CurrentRow;
                if (dataGridViewRow != null)
                {
                    var productId = Convert.ToInt32(dataGridViewRow.Cells[0].Value);
                    var product = _productServices.GetById(productId);
                    product.Category = null;
                    product.OrderDetails = null;

                    var dr = MessageBox.Show(Resources.FormProduct_deleteToolStripMenuItem_Click_Are_you_sure_,
                        Resources.FormProduct_deleteToolStripMenuItem_Click_Warning,
                        MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        _productServices.Delete(product);
                    }
                    FillData();
                    MessageBox.Show(Resources.FormProduct_deleteToolStripMenuItem_Click_Delete_Successfully_);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var search = textBoxSearch.Text.ToLower();
            var list = _productServices.GetAll();
            var listProd = !string.IsNullOrEmpty(search)
               ? list.Where(x => x.Name.ToLower().Contains(search)).ToList()
               : list.ToList();

            var linq = from s in listProd
                       select new
                       {
                           s.Id,
                           Category = s.Category.Name,
                           s.Name,
                           s.Make,
                           s.Model,
                           s.YearOfRegistion,
                           s.AddOns,
                           RentPrice = s.RentPerDay
                       };
            dataGridView1.DataSource = linq.ToList();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dataGridViewRow = dataGridView1.CurrentRow;
            if (dataGridViewRow != null)
            {
                var productId = Convert.ToInt32(dataGridViewRow.Cells[0].Value);
                var product = _productServices.GetById(productId);

                textBoxId.Text = product.Id.ToString();
                textBoxName.Text = product.Name;
                textBoxMake.Text = product.Make;
                textBoxModel.Text = product.Model;
                textBoxAddons.Text = product.AddOns;
                numericBoxRentPrice.Text = product.RentPerDay.ToString(CultureInfo.InvariantCulture);
                comboBoxYear.Text = product.YearOfRegistion;
                comboBoxCategory.Text = product.Category.Name;
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
