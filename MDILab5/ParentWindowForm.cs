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
        private string childSaveName;

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
            ChildForm activeChild = (ChildForm)this.ActiveMdiChild;
            if (activeChild == null)
                return;
            if (activeChild.Text != "ChildForm")
            {
                childSaveName = activeChild.Text;
                FileStream stream = (FileStream)saveFileDialog1.OpenFile();
                activeChild.ChildImg.Save(saveFileDialog1.FileName);
            }
            else
            {
                saveAsToolStripMenuItem_Click(saveAsToolStripMenuItem, new EventArgs());
            }
        } 

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine(e.ToString());
            ChildForm activeChild = (ChildForm)this.ActiveMdiChild;
            if (activeChild == null) //if no active window, ignore
                return;
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "jpg|*.jpg|bmp|*.bmp|gif|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                childSaveName = saveFileDialog1.FileName;
                using (FileStream stream = (FileStream)saveFileDialog1.OpenFile())
                {
                    activeChild.ChildImg.Save(saveFileDialog1.FileName);
                    activeChild.Text = childSaveName;

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
            this.Close();
        }
    }
}
