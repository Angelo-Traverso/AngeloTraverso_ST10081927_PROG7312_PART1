using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
namespace DeweyDecimal_Latest
{
    public partial class StatisticsForm : Form
    {
        /// <summary>
        ///     Holds Statistics objects
        /// </summary>
        List<Statistics> statsList = new List<Statistics>();

        /// <summary>
        ///     Constructor accepting a List<T> to retrieve stats list objects
        /// </summary>
        /// <param name="statsList"></param>
        public StatisticsForm(List<Statistics> statsList)
        {
            InitializeComponent();
            this.statsList = statsList;
            SetUpStats();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        // ----------------------------------------------------------------------------------------------------------- //

        /// <summary>
        ///     Returns whether the stats list is empty or not
        /// </summary>
        /// <returns></returns>
        private bool isStatsEmpty() => statsList.Count == 0;

        // ----------------------------------------------------------------------------------------------------------- //

        /// <summary>
        ///     Sets statistic values if the list is not empty
        /// </summary>
        private void SetUpStats()
        {
            if (isStatsEmpty())
            {
                return;
            }
            else
            {
                lblQuickestTimeValue.Text = QuickestCompletionTime();
                lblScoreValue.Text = GetScore().ToString();
                lblRoundsValue.Text = statsList.Count.ToString();
                lblAverage.Text = AverageCompletionTime();
                lblFailedAttemptsValue.Text = GetFailedAttempts().ToString();
                lblSuccessfulAttemptsValue.Text = GetSuccessfullAttempts().ToString();
            }
        }

        // ----------------------------------------------------------------------------------------------------------- //

        /// <summary>
        ///     Gets the users' current score
        /// </summary>
        /// <returns></returns>
        private double GetScore()
        {
            return statsList[statsList.Count - 1].score;
        }

        // ----------------------------------------------------------------------------------------------------------- //

        /// <summary>
        ///     Returns users' failed number of attempts
        /// </summary>
        /// <returns></returns>
        private int GetFailedAttempts() => statsList[statsList.Count - 1].failedAttempts;

        // ----------------------------------------------------------------------------------------------------------- //

        /// <summary>
        ///     Returns users' number of successful attempts
        /// </summary>
        /// <returns></returns>
        private int GetSuccessfullAttempts() => statsList[statsList.Count - 1].successAttempts;

        // ----------------------------------------------------------------------------------------------------------- //

        /// <summary>
        ///     Returns the Quickest completion time based on non-failed attemptd
        /// </summary>
        /// <returns></returns>
        private string QuickestCompletionTime()
        {
            // Filter the statistics to consider only games with isFail = false
            var validStats = statsList.Where(stat => !stat.isFail).ToList();

            if (validStats.Count == 0)
                return "N/A";

            // Find the minimum time in seconds from the valid statistics
            double lowestSeconds = validStats.Min(stat => stat.time.TotalSeconds);

            // Calculate minutes and seconds
            int minutes = (int)lowestSeconds / 60;
            int seconds = (int)lowestSeconds % 60;

            // Format the time as "M:SS"
            string formattedTime = $"{minutes}:{seconds.ToString("D2")}"; // D2 pads the seconds with leading zeros if needed

            return formattedTime;
        }

        // ----------------------------------------------------------------------------------------------------------- //

        /// <summary>
        ///     Returns the total time user has used
        /// </summary>
        /// <returns></returns>
        private TimeSpan TotalCompletionTime()
        {
            TimeSpan total = TimeSpan.Zero;

            foreach (var stat in statsList)
            {
                total += stat.time;
            }

            return total;
        }

        // ----------------------------------------------------------------------------------------------------------- //

        /// <summary>
        ///     Returns the average completion time
        /// </summary>
        /// <returns></returns>
        private string AverageCompletionTime()
        {
            TimeSpan total = TotalCompletionTime();

            double averageSeconds = total.TotalSeconds / statsList.Count;

            int averageMinutes = (int)averageSeconds / 60;
            int averageSecondsRemainder = (int)averageSeconds % 60;

            string formattedAverageTime = $"{averageMinutes:D2}:{averageSecondsRemainder:D2}";

            return formattedAverageTime;
        }

        // ----------------------------------------------------------------------------------------------------------- //

        /// <summary>
        ///     Back button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
// --------------------------------- .....ooooo00000 END OF FILE 00000ooooo..... --------------------------------- //