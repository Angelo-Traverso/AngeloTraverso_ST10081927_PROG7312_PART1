using System;
using System.Collections.Generic;

namespace DeweyDecimal_Latest
{
    public struct Statistics
    {
        // Can calculate average score per game etc
        public DateTime timeStamp { get; set; }
        public double score { get; set; }

        public int attempts { get; set; }

        public Statistics(DateTime timeStamp, double _score, int _attempts)
        {
            this.timeStamp = timeStamp;
            this.score = _score;
            this.attempts = _attempts;
        }

        /// <summary>
        ///     Method to capture user stats after game completion
        /// </summary>
        /// <param name="statsList"></param>
        /// <param name="score"></param>
        /// <param name="attempts"></param>
        public void CaptureStats(List<Statistics> statsList, double score, int attempts) => statsList.Add(new Statistics(DateTime.Now, score, attempts));
    }
}
