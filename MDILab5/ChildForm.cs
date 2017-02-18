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
        public bool IsNewImage
        {
            get
            {
                return isNewImage;
            }
            set
            {
                isNewImage = value;
            }
        }
        private Image childImg;
        private Size imgSize;
        public Image ChildImg
        {
            get
            {
                return childImg;
            }
            set
            {
                childImg = value;
            }
        }

        public Size ImgSize
        {
            set
            {
                imgSize = value;
            }
        }

        public ChildForm()
        {
            InitializeComponent();
        }

        //will check if new or existing, then paint on child forms
        private void ChildForm_Paint(object sender, PaintEventArgs e)
        {
            this.AutoScrollMinSize = childImg.Size;
            g = e.Graphics;
            if (isNewImage == true)
            {
                SolidBrush b = new SolidBrush(Color.Blue);
                g.FillRectangle(b, this.ClientRectangle);
            } else
            {
                g.DrawImage(childImg, this.AutoScrollPosition.X, this.AutoScrollPosition.Y, childImg.Width, childImg.Height);
            }
        }
    }
}
