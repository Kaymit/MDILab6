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

namespace MDILab6
{
    public partial class ParentWindowForm : Form
    {
        private Size imgSize;

        public ParentWindowForm()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewImageDialog newImage = new NewImageDialog();
            if (newImage.ShowDialog() == DialogResult.OK)
            {

                ChildForm ch = new ChildForm();
                ch.Height = newImage.ImgSize.Height;
                ch.Width = newImage.ImgSize.Width;
                ch.ChildImg = new Bitmap(newImage.ImgSize.Height, newImage.ImgSize.Width);
                ch.MdiParent = this; //set as parent
                ch.IsNewImage = true;
                ch.Show();
            }
        }

        private void openFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog newFile = new OpenFileDialog();
            newFile.Filter = "jpg|*.jpg|jpeg|*.jpeg|bmp|*.bmp|gif|*.gif";
            if (newFile.ShowDialog() == DialogResult.OK)
            {
                ChildForm ch = new ChildForm();
                ch.ChildImg = Image.FromFile(newFile.FileName);
                ch.MdiParent = this; //set as parent
                ch.IsNewImage = false;
                ch.Show();
            }
        }

        private void openFromWebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebImageDialog newFile = new WebImageDialog();

            if (newFile.ShowDialog() == DialogResult.OK)
            {
                ChildForm ch = new ChildForm();
                Stream stream = WebRequest.Create(newFile.TextBoxURL).GetResponse().GetResponseStream();
                ch.ChildImg = Image.FromStream(stream);
                ch.MdiParent = this; //set as parent
                ch.IsNewImage = false;
                ch.Show();
                stream.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            this.Close();
        }
    }
}
