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

        /// <summary>
        /// Creates a new child MDI form and inits a new Bitmap and Image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewImageDialog newImage = new NewImageDialog();
            try
            {
                if (newImage.ShowDialog() == DialogResult.OK)
                {
                    ChildForm child = new ChildForm();
                    child.ChildImg = new Bitmap(newImage.ImgSize.Height, newImage.ImgSize.Width); //creates the image
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

        /// <summary>
        /// Opens a file on the computer, creating a new child form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog newFile = new OpenFileDialog();
            try
            {
                newFile.Filter = "jpg|*.jpg|jpeg|*.jpeg|bmp|*.bmp|gif|*.gif";//specifies the image formats that can be opened
                if (newFile.ShowDialog() == DialogResult.OK)
                {
                    ChildForm child = new ChildForm();
                    child.ChildImg = Image.FromFile(newFile.FileName);//creates the image from the file
                    child.MdiParent = this;//set as parent
                    child.IsNewImage = false; //property of child form
                    child.Text = newFile.FileName; //sets the next of the new child form to the filename
                    child.Show();
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, this.Text);
            }
        }

        /// <summary>
        /// Opens a user-chosen image from the internet in a new child form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFromWebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebImageDialog newFile = new WebImageDialog();
            try
            {
                if (newFile.ShowDialog() == DialogResult.OK)
                {
                    ChildForm child = new ChildForm();
                    Stream stream = WebRequest.Create(newFile.TextBoxURL).GetResponse().GetResponseStream();
                    child.ChildImg = Image.FromStream(stream);//creates a new image from the web stream
                    child.MdiParent = this; //set as parent
                    child.IsNewImage = false; //property of child form
                    child.Text = newFile.Text; //sets the next of the new child form to the filename
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
        /// Saves existing file. If image does not exist, saveAs will be called.
        /// </summary>
        /// <resources>
        /// https://msdn.microsoft.com/en-us/library/sfezx97z(v=vs.110).aspx
        /// https://msdn.microsoft.com/en-us/library/4s6dtf7z(v=vs.110).aspx
        /// </resources>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm child = (ChildForm)this.ActiveMdiChild; //sets the ChildForm to be saved to the active child form
            try
            {
                if (child == null) //if no active window, ignore
                    return;
                else if (child.Text != "ChildForm")//if the current child image is not new
                {
                    Image image = (Image)new Bitmap(child.ChildImg.Width, child.ChildImg.Height);//creates a new image
                    Graphics gfx = Graphics.FromImage(image);
                    gfx.Clear(Color.Blue); //sets the background color to blue if saving a blank image
                    gfx.DrawImage(child.ChildImg, 0, 0);
                    child.ChildImg.Dispose();
                    child.ChildImg = image;

                    child.ChildImg.Save(child.Text);
                }
                else//if it is new, call saveAs function
                {
                    saveAsToolStripMenuItem_Click(saveAsToolStripMenuItem, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, this.Text);
            }
        }

        /// <summary>
        /// Allows user to choose a filepath to save the current image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm child = (ChildForm)this.ActiveMdiChild;//sets the ChildForm to be saved to the active child form
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
                        gfx.Clear(Color.Blue); //sets the background color to blue if saving a blank image
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

        /// <summary>
        /// Arranges the images into a cascade pattern
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        /// <summary>
        /// Arranges the images into a horizontal stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        /// <summary>
        /// Arranges the images into a vertical stack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        /// <summary>
        /// Closes the program, will ask if user wants to save if there are active child forms.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ChildForm activeChild = (ChildForm)this.ActiveMdiChild;//sets the ChildForm to be saved to the active child form
                if (activeChild == null) //if no active window, close
                    this.Close();

                foreach (Form child in this.MdiChildren)
                {
                    ExitDialogue exit = new ExitDialogue();
                    if (exit.ShowDialog() == DialogResult.OK)
                    {
                        saveAsToolStripMenuItem_Click(sender, e);
                        child.Close();
                        child.Dispose();
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, this.Text);
            }
        }

        /// <summary>
        /// Checks to see if there are and child forms open, and if not, it disables the save and save as buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
