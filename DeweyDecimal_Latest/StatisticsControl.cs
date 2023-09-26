using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace DeweyDecimal_Latest
{
    public partial class StatisticsControl : UserControl
    {
        Statistics stats = new Statistics();
        public StatisticsControl()
        {
            InitializeComponent();

           // HandleStatsList();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private bool isStatsEmpty(List<Statistics> statsList) => statsList.Count == 0;

        /*private void HandleStatsList()
        {
            if (isStatsEmpty(stats.statsList))
            {
                lblNoStats.Visible = true;
                lblNoStatsDesc.Visible = true;

                lblBestTime.Visible = false;
                lblTimeHeading.Visible = false;
                lblScore.Visible = false;
                lblScoreHeading.Visible = false;
            }
            else 
            {
                lblNoStats.Visible = false;
                lblNoStatsDesc.Visible = false;

                lblBestTime.Visible = true;
                lblTimeHeading.Visible = true;
                lblScore.Visible = true;
                lblScoreHeading.Visible = true;
            }


        }*/

    }
}
