// ----------------------------------------------------------------------------
//
// Definition of the InputBox class
// Date: 29.11.2018
// Author: Dorian Niclass
//
// ----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Krop.ControlWindow
{
    public partial class Input : Form
    {
        public Input()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
