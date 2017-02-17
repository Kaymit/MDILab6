﻿using System;
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
                ch.ChildImg = new Bitmap(newImage.Size.Height, newImage.Size.Width);
                ch.MdiParent = this; //set as parent
                ch.Show();
            }
        }

        private void openFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openFromWebToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
    }
}