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
    /// <summary>
    /// Dialog box to open files from the web
    /// </summary>
    public partial class WebImageDialog : Form
    {
        public string TextBoxURL
        {
            get { return textBoxURL.Text; }
        }

        public WebImageDialog()
        {
            InitializeComponent();
            this.Text = TextBoxURL;
        }
    }
}
