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
                    ChildForm child = new ChildForm();
                    child.ChildImg = new Bitmap(newImage.ImgSize.Height, newImage.ImgSize.Width);
                    child.MdiParent = this; //set as parent
                    child.IsNewImage = true; //property of child form
                    child.Show();
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
                    ChildForm child = new ChildForm();
                    child.ChildImg = Image.FromFile(newFile.FileName);
                    child.MdiParent = this;
                    child.IsNewImage = false;
                    child.Text = newFile.FileName;
                    child.Show();
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
                    ChildForm child = new ChildForm();
                    Stream stream = WebRequest.Create(newFile.TextBoxURL).GetResponse().GetResponseStream();
                    child.ChildImg = Image.FromStream(stream);
                    child.MdiParent = this; //set as parent
                    child.IsNewImage = false;
                    child.Text = newFile.Text;
                    child.Show();
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
            ChildForm child = (ChildForm)this.ActiveMdiChild;

            try
            {
                if (child == null)
                    return;
                else if (child.Text != "ChildForm")
                {
                    Image image = (Image)new Bitmap(child.ChildImg.Width, child.ChildImg.Height);
                    Graphics gfx = Graphics.FromImage(image);
                    gfx.Clear(Color.Blue);
                    gfx.DrawImage(child.ChildImg, 0, 0);
                    child.ChildImg.Dispose();
                    child.ChildImg = image;

                    child.ChildImg.Save(child.Text);
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
            ChildForm child = (ChildForm)this.ActiveMdiChild;
            if (child == null) //if no active window, ignore
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
                        Image image = (Image)new Bitmap(child.ChildImg.Width, child.ChildImg.Height);
                        Graphics gfx = Graphics.FromImage(image);
                        gfx.Clear(Color.Blue);
                        gfx.DrawImage(child.ChildImg, 0, 0);
                        child.ChildImg.Dispose();
                        child.ChildImg = image;

                        child.ChildImg.Save(saveFileDialog.FileName);
                        child.Text = saveFileDialog.FileName;
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
