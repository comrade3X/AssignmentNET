using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace MobilizeYou
{
    using DTO;
    using BLL;

    public partial class FormOrder : Form
    {
        CategoriesServices CategoriesServices = new CategoriesServices();

        ProductServices ProductServices = new ProductServices();

        public FormOrder()
        {
            InitializeComponent();
            FillData();
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {

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
    }
}
