using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeweyDecimal_Latest
{
    public partial class FindingCallNumberTreeControl : UserControl
    {
        FileWorker fileWorker = new FileWorker();
        public FindingCallNumberTreeControl()
        {
            if (!this.DesignMode)
            {
                InitializeComponent();

            }
        }

        private void FindingCallNumberTreeControl_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                fileWorker.ReadFromFile();

                fileWorker.DisplayQuestion(new Panel[] { pnlOption1, pnlOption2, pnlOption3, pnlOption4 }, lblQuestion);
            }
        }

    }
}
