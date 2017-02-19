using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace MDILab6
{
    public partial class ParentWindowForm : Form
    {
        public ParentWindowForm()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewImageDialog newImage = new NewImageDialog();
            try
            {
                if (newImage.ShowDialog() == DialogResult.OK)
                {
                    ChildForm ch = new ChildForm();
                    ch.ChildImg = new Bitmap(newImage.ImgSize.Height, newImage.ImgSize.Width);
                    ch.MdiParent = this; //set as parent
                    ch.IsNewImage = true; //property of child form
                    ch.Show();
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, this.Text);
            }
        }

        private void openFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog newFile = new OpenFileDialog();
            try
            {
                newFile.Filter = "jpg|*.jpg|jpeg|*.jpeg|bmp|*.bmp|gif|*.gif";
                if (newFile.ShowDialog() == DialogResult.OK)
                {
                    ChildForm ch = new ChildForm();
                    ch.ChildImg = Image.FromFile(newFile.FileName);
                    ch.MdiParent = this;
                    ch.IsNewImage = false;
                    ch.Text = newFile.FileName;
                    ch.Show();
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, this.Text);
            }
        }

        private void openFromWebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebImageDialog newFile = new WebImageDialog();
            try
            {
                if (newFile.ShowDialog() == DialogResult.OK)
                {
                    ChildForm ch = new ChildForm();
                    Stream stream = WebRequest.Create(newFile.TextBoxURL).GetResponse().GetResponseStream();
                    ch.ChildImg = Image.FromStream(stream);
                    ch.MdiParent = this; //set as parent
                    ch.IsNewImage = false;
                    ch.Text = newFile.Text;
                    ch.Show();
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, this.Text);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <resources>
        /// https://msdn.microsoft.com/en-us/library/sfezx97z(v=vs.110).aspx
        /// https://msdn.microsoft.com/en-us/library/4s6dtf7z(v=vs.110).aspx
        /// </resources>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm ch = (ChildForm)this.ActiveMdiChild;

            try
            {
                if (ch == null)
                    return;
                else if (ch.Text != "ChildForm")
                {
                    Image image = (Image)new Bitmap(ch.ChildImg.Width, ch.ChildImg.Height);
                    Graphics g = Graphics.FromImage(image);
                    g.Clear(Color.Blue);
                    g.DrawImage(ch.ChildImg, 0, 0);
                    ch.ChildImg.Dispose();
                    ch.ChildImg = image;

                    ch.ChildImg.Save(ch.Text);
                }
                else
                {
                    saveAsToolStripMenuItem_Click(saveAsToolStripMenuItem, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, this.Text);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm ch = (ChildForm)this.ActiveMdiChild;
            if (ch == null) //if no active window, ignore
                return;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "jpg|*.jpg|bmp|*.bmp|gif|*.gif";
            saveFileDialog.Title = "Save an Image File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.FileName != "")
                {
                    try
                    {
                        Image image = (Image)new Bitmap(ch.ChildImg.Width, ch.ChildImg.Height);
                        Graphics g = Graphics.FromImage(image);
                        g.Clear(Color.Blue);
                        g.DrawImage(ch.ChildImg, 0, 0);
                        ch.ChildImg.Dispose();
                        ch.ChildImg = image;

                        ch.ChildImg.Save(saveFileDialog.FileName);
                        ch.Text = saveFileDialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        int num = (int)MessageBox.Show(ex.Message, this.Text);
                    }
                }
            }
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, this.Text);
            }
        }

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                saveToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
            }
            else
            {
                saveToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
            }
        }
    }
}
