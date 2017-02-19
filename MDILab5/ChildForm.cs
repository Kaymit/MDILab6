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
        Graphics gfx;
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

        private Image childImg;//image displayed in child form
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

        private Size imgSize;//size of the child image
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

        /// <summary>
        /// will check if new or existing image, then paint on child forms
        /// </summary>
        private void ChildForm_Paint(object sender, PaintEventArgs e)
        {
            this.AutoScrollMinSize = childImg.Size;
            gfx = e.Graphics;
            if (isNewImage == true)
            {
                SolidBrush b = new SolidBrush(Color.Blue);
                gfx.FillRectangle(b, this.ClientRectangle);
            } else
            {
                gfx.DrawImage(childImg, this.AutoScrollPosition.X, this.AutoScrollPosition.Y, childImg.Width, childImg.Height);
            }
        }
    }
}
