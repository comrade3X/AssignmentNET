using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MobilizeYou.BLL;

namespace MobilizeYou
{
    public static class Helper
    {
        #region Show right form - prevent duplicate children form.

        public static void ShowRight<T>(this Form parent) where T : Form, new()
        {
            try
            {
                var x = typeof(T);
                var frm = (T)GetChilden(parent, x.FullName);
                if (frm == null || frm.IsDisposed)
                {
                    frm = new T() { MdiParent = parent };

                    frm.Show();
                }
                else
                {
                    frm.Activate();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static Form GetChilden(this Form parent, string childName)
        {
            Form f = null;
            try
            {
                foreach (var frm in parent.MdiChildren)
                {
                    if (frm.GetType().FullName == childName)
                    {
                        f = frm;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return f;
        }

        #endregion

        public static bool IsPhoneNumber(string phone)
        {
            Regex reg = new Regex(@"^\+?\d{1,3}?[- .]?\(?(?:\d{2,3})\)?[- .]?\d\d\d[- .]?\d\d\d\d$");
            //--example: 0123456789, +84123456789, +84 123456789, 123-456-789
            return reg.IsMatch(phone);
        }
    }
}
