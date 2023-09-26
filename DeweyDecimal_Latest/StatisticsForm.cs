using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DeweyDecimal_Latest
{
    public partial class StatisticsForm : Form
    {
        List<Statistics> statsList = new List<Statistics>();
        public StatisticsForm(List<Statistics> statsList)
        {
            InitializeComponent();
            this.statsList = statsList;
            SetUpStats();
        }

        private bool isStatsEmpty() => statsList.Count == 0;

        private void SetUpStats()
        {
            if (isStatsEmpty())
            {
                return;
            }
            else
            {
                lblQuickestTimeValue.Text = QuickestCompletionTime().ToString();
                lblScoreValue.Text = statsList[0].score.ToString();
                lblRoundsValue.Text = statsList.Count.ToString();
                lblAverage.Text = AverageCompletionTime().ToString();
            }
        }

        private void PopulateStatsFields()
        { 
            
        }

        private void FindQuickestTime()
        { 
        
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }         

        private void lblScoreValue_Click(object sender, EventArgs e)
        {

        }

        private double QuickestCompletionTime()
        {
            double lowest = statsList[0].time.Seconds;

            for (int i = 0; i <  statsList.Count; i++) 
            {
                if (statsList[i].time.Seconds < lowest)
                { 
                    lowest = statsList[i].time.Seconds; 
                    break;
                }
            }
            return lowest;
        }

        private TimeSpan TotalCompletionTime()
        {
            TimeSpan total = TimeSpan.Zero;

            foreach (var stat in statsList)
            {
                total += stat.time;
            }

            return total;
        }

        private double AverageCompletionTime()
        {
            TimeSpan total = TotalCompletionTime();

            double averageSeconds = total.TotalSeconds / statsList.Count;

            return averageSeconds;
        }

    }
}
