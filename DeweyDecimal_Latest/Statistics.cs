using System;
using System.Collections.Generic;
using System.Linq;

namespace DeweyDecimal_Latest
{
    public struct Statistics
    {
        // Can calculate average score per game etc
        public DateTime timeStamp { get; set; }
        public double score { get; set; }

        public int failedAttempts { get; set; }
        public int successAttempts { get; set; }
        public bool isFail { get; set; }
        public TimeSpan time { get; set; }


        public Statistics(DateTime timeStamp, double _score, int _successAttempts, int _failedAttempts ,bool isFail, TimeSpan _time)
        {
            this.timeStamp = timeStamp;
            this.score = _score;
            this.failedAttempts = _failedAttempts;
            this.successAttempts = _successAttempts;
            this.isFail = isFail;
            this.time = _time;
        }

        /// <summary>
        ///     Method to capture user stats after game completion
        /// </summary>
        /// <param name="statsList"></param>
        /// <param name="score"></param>
        /// <param name="attempts"></param>
        public void CaptureStats(List<Statistics> statsList, double score, int successAttempts, int failedAttempts,bool isFail, TimeSpan time) => statsList.Add(new Statistics(DateTime.Now, score, successAttempts, failedAttempts, isFail , time));

        /// <summary>
        ///     Returns users' personal best time for a non-failed attempt
        /// </summary>
        /// <param name="statsList"></param>
        /// <returns></returns>
        public string GetPersonalBest(List<Statistics> statsList)
        {
            try
            {
                if (statsList.Count == 0)
                return "N/A";
           

                TimeSpan? minTime = statsList
                    .Where(stat => !stat.isFail)
                    .Select(stat => stat.time)
                    .Min();
           

            if (minTime.HasValue)
            {
                return $"{minTime.Value.Minutes:D2}:{minTime.Value.Seconds:D2}";
            }
            else
            {
                return "No personal best yet...";
            }
            }
            catch (Exception)
            {
                return "No personal best yet...";
            }
        }
    }
}
