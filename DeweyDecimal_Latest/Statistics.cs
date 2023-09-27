/*
 *  Full Name: Angelo Traverso
 *  Student Number: ST10081927
 *  Subject: Programming 3B
 *  Code: PROG7312
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace DeweyDecimal_Latest
{
    public struct Statistics
    {
       /// <summary>
       ///  Holds the timestamp of the game
       /// </summary>
        public DateTime timeStamp { get; set; }

        /// <summary>
        ///     Holds the users' current score
        /// </summary>
        public double score { get; set; }

        /// <summary>
        ///     holds the users' number of failed attempts
        /// </summary>
        public int failedAttempts { get; set; }

        /// <summary>
        ///     holds the number of successful attempts
        /// </summary>
        public int successAttempts { get; set; }

        /// <summary>
        ///     stores whether the game was failed or not
        /// </summary>
        public bool isFail { get; set; }

        /// <summary>
        ///     Holds the amount of time it took the user to complete the game
        /// </summary>
        public TimeSpan time { get; set; }

        /// <summary>
        ///     Statistics Constructor
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <param name="_score"></param>
        /// <param name="_successAttempts"></param>
        /// <param name="_failedAttempts"></param>
        /// <param name="isFail"></param>
        /// <param name="_time"></param>
        public Statistics(DateTime timeStamp, double _score, int _successAttempts, int _failedAttempts ,bool isFail, TimeSpan _time)
        {
            this.timeStamp = timeStamp;
            this.score = _score;
            this.failedAttempts = _failedAttempts;
            this.successAttempts = _successAttempts;
            this.isFail = isFail;
            this.time = _time;
        }

        // ----------------------------------------------------------------------------------------------------------- //
        /// <summary>
        ///     Method to capture user stats after game completion
        /// </summary>
        /// <param name="statsList"></param>
        /// <param name="score"></param>
        /// <param name="attempts"></param>
        public void CaptureStats(List<Statistics> statsList, double score, int successAttempts, int failedAttempts,bool isFail, TimeSpan time) => statsList.Add(new Statistics(DateTime.Now, score, successAttempts, failedAttempts, isFail , time));

        // ----------------------------------------------------------------------------------------------------------- //
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
// --------------------------------- .....ooooo00000 END OF FILE 00000ooooo..... --------------------------------- //
