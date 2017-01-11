using System;
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
    }
}
