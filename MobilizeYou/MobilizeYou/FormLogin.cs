﻿using System;
using System.Windows.Forms;
using MobilizeYou.Properties;

namespace MobilizeYou
{
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
            try
            {
                var userName = textBoxUsername.Text;
                var pw = textBoxPassword.Text;
                var membership = _membershipServices.Login(userName, pw);

                if (membership == null)
                {
                    MessageBox.Show("User name or Password invalid.");
                    return;
                }

                FormMain frmMain = new FormMain(membership);
                frmMain.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
