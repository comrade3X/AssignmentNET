using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MobilizeYou.Properties;


namespace MobilizeYou
{
    using BLL;
    public partial class FormOrder2 : Form
    {
        public CategoriesServices CategoriesServices;
        public ProductServices ProductServices;
        public OrderDetailsServices OrderDetailsServices;

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
                comboBoxCategory.DataSource = listCategory;
                comboBoxCategory.DisplayMember = "Name";
                comboBoxCategory.ValueMember = "Id";

                CascadeCb(null);
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
            comboBoxMake.DataSource = listMakeByCate;
        }

        #endregion

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            try
            {
                var dateFr = dateTimePickerFrom.Value;
                var dateTo = dateTimePickerTo.Value;

                var results = OrderDetailsServices.Search(dateFr, dateTo);

                var linq = from s in results
                           select new
                           {
                               s.Status,
                               s.Product.Name,
                               Type = s.Product.Category.Name,
                               s.Product.RentPerDay,
                               s.From,
                               s.To,
                               s.Customer,
                               s.OrderDate
                           };

                dataGridViewSearchResults.DataSource = linq.ToList()
                    .OrderBy(x => x.Status)
                    .ThenBy(x => x.Type).ThenBy(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewSearchResults.CurrentRow != null)
            {
                var status = dataGridViewSearchResults.CurrentRow.Cells[0].Value;
                if ("Hired".Equals(status))
                {
                    MessageBox.Show(Resources.FormOrder2_addOrderToolStripMenuItem_Click_Cannot_order_this_items);
                    return;
                }

            }
        }
    }
}
