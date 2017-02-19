using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDILab6
{
    /// <summary>
    /// Dialog box for opening files from your computer.
    /// </summary>
    public partial class NewImageDialog : Form
    {
        private Size imgSize;//Stores the size of the new image

        public Size ImgSize
        {
            get
            {
                return imgSize;
            }
            set
            {
                imgSize = value;
            }
        }

        public NewImageDialog()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Sets the image size to 640 by 480 if the first radio button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonSmall_CheckedChanged(object sender, EventArgs e)
        {
            ImgSize = new Size(640, 480);
        }

        /// <summary>
        /// Sets the image size to 800 by 600 if the second radio button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonMedium_CheckedChanged(object sender, EventArgs e)
        {
            ImgSize = new Size(800, 600);
        }

        /// <summary>
        /// Sets the image size to 1024 by 768 if the third radio button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonLarge_CheckedChanged(object sender, EventArgs e)
        {
            ImgSize = new Size(1024, 768);
        }
    }
}
