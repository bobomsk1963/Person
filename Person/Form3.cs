using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Person
{
    public partial class Form3 : Form
    {
        object O;
        public Form3(object o)
        {
            O = o;
            InitializeComponent();
        }

        private void Form3_Shown(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = O;
        }

    }
}
