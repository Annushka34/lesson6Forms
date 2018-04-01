using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson7WebBrowser
{
    public partial class Form1 : Form
    {
        string homePage = @"https://www.google.com/";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages[0].Text = "Google   ";
            tabControl1.TabPages[1].Text = "+";
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += TabControl1_DrawItem;
            tabControl1.MouseClick += TabControl1_MouseClick;

            WebBrowser webBrowser = new WebBrowser();
            webBrowser.Dock = DockStyle.Fill;
            tabControl1.TabPages[0].Controls.Add(webBrowser);
            webBrowser.Navigate(homePage);
        }

        private void TabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (tabControl1.SelectedIndex == tabControl1.TabPages.Count - 1)
            {
                TabPage newPage = new TabPage("+");
                tabControl1.TabPages.Add(newPage);

                TabPage current = tabControl1.TabPages[tabControl1.SelectedIndex];
                WebBrowser newWeb = new WebBrowser();
                newWeb.Dock = DockStyle.Fill;
                current.Controls.Add(newWeb);
                newWeb.Navigate(homePage);
                current.Text = newWeb.DocumentTitle;
            }
            else
            {
                //git test
                Rectangle r = tabControl1.GetTabRect(tabControl1.SelectedIndex);
                Rectangle close = new Rectangle(r.X + r.Width - 12, r.Y + 2, 10, 10);
                if (close.Contains(e.Location))
                {
                    // tabControl1.TabPages.Remove(tabControl1.TabPages[tabControl1.SelectedIndex]);
                    MessageBox.Show("Test");
                }
            }
        }
        private void TabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Rectangle r = e.Bounds;
            Rectangle close = new Rectangle(r.X + r.Width - 12, r.Y + 2, 10, 10);
          
            Graphics g = e.Graphics;
            string text = tabControl1.TabPages[e.Index].Text;
            g.DrawString(text, e.Font, new SolidBrush(Color.Blue), r);

            if (e.Index == tabControl1.TabCount - 1)
                return;
            g.DrawImage(Image.FromFile(@"D:\ШАГ\forms\ПрикладиЗКласу\Lesson7WebBrowser\delete.ico"), close);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = textBox1.Text;
            WebBrowser web = (WebBrowser)tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
            web.Navigate(url);
            tabControl1.TabPages[tabControl1.SelectedIndex].Text = web.DocumentTitle;
        }
    }
}
