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
    public partial class NewImageDialog : Form
    {
        private Size imgSize;
        public NewImageDialog()
        {
            InitializeComponent();
        }

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

        private void radioButtonSmall_CheckedChanged(object sender, EventArgs e)
        {
            ImgSize = new Size(640, 480);
        }

        private void radioButtonMedium_CheckedChanged(object sender, EventArgs e)
        {
            ImgSize = new Size(800, 600);
        }

        private void radioButtonLarge_CheckedChanged(object sender, EventArgs e)
        {
            ImgSize = new Size(1024, 768);
        }
    }
}
