using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MobilizeYou.DTO;

namespace MobilizeYou
{
    public partial class FormMain : Form
    {
        public FormMain(Membership membership)
        {
            InitializeComponent();
            HideMenu(membership);
        }

        public FormMain() { InitializeComponent(); }

        private void listCustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisposeAll();
            this.ShowRight<FormCustomers>();
        }

        private void listProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisposeAll();
            this.ShowRight<FormProduct>();
        }

        private void listEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisposeAll();
            this.ShowRight<FormEmployee>();
        }

        private void membershipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisposeAll();
            this.ShowRight<FormMembership>();
        }

        #region -- Helper --

        /// <summary>
        /// Hide menu with persmission
        /// </summary>
        /// <param name="m"></param>
        private void HideMenu(Membership m)
        {
            if (!m.Role.Name.Equals("User")) return;
            productToolStripMenuItem.Visible = false;
            employeeToolStripMenuItem.Visible = false;
            reportToolStripMenuItem.Visible = false;
        }

        private void DisposeAll()
        {
            ActiveMdiChild?.Close();
        }

        #endregion

        private void reportToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void createOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisposeAll();
            this.ShowRight<FormOrder>();
        }
    }
}
