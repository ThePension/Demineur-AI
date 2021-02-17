using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demineur
{
    public partial class frmSettings : Form
    {
        int size;
        public frmSettings()
        {
            InitializeComponent();
        }

        private void btnCommencer_Click(object sender, EventArgs e)
        {
            if (rb8.Checked) size = 8;
            if (rb16.Checked) size = 16;
            if (rb20.Checked) size = 20;
            frmDemineur frmDem = new frmDemineur(size, this);
            this.Hide();
            frmDem.ShowDialog();
        }

        private void btnCommencerAI_Click(object sender, EventArgs e)
        {
            if (rb8.Checked) size = 8;
            if (rb16.Checked) size = 16;
            if (rb20.Checked) size = 20;
            if (rbAutre.Checked) size = Convert.ToInt32(nudSize.Value);
            frmDemineurAI frmDemAI = new frmDemineurAI(size, this);
            this.Hide();
            frmDemAI.ShowDialog();
        }
    }
}
