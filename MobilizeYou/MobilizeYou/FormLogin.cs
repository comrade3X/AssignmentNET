using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobilizeYou
{
    using DTO;
    using BLL;
    public partial class FormLogin : Form
    {
        MembershipServices _membershipServices = new MembershipServices();

        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            var userName = textBoxUsername.Text;
            var pw = textBoxPassword.Text;
            var membership = _membershipServices.GetAll().FirstOrDefault(x => userName.Equals(x.Username) && pw.Equals(x.Password));
            if (membership != null)
            {
                FormMain frmMain = new FormMain(membership);
                frmMain.Show();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
