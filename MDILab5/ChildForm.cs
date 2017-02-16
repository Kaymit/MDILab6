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
    public partial class ChildForm : Form
    {
        //image field and property
        Graphics g;
        bool isNewImage = true; //flag to check if new or existing image
        private Image myImage;
        public Image MyImage
        {
            get
            {
                return myImage;
            }
            set
            {
                myImage = value;
                this.AutoScrollMinSize = myImage.Size;
            }
        }

        public ChildForm()
        {
            InitializeComponent();
        }

        //will check if new or existing, then paint on child forms
        private void ChildForm_Paint(object sender, PaintEventArgs e)
        {
            if (isNewImage == true)
            {
                g = e.Graphics;
                SolidBrush b = new SolidBrush(Color.Blue);
                g.FillRectangle(b, this.ClientRectangle);
            }
            else
            {

            }
        }
    }
}
