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
        public NewImageDialog()
        {
            InitializeComponent();
        }

        private void radioButtonSmall_CheckedChanged(object sender, EventArgs e)
        {
            changeImageSize(new Size(640, 480));
        }

        private void radioButtonMedium_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonLarge_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
