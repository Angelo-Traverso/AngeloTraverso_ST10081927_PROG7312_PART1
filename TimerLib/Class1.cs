using System;
using System.Timers;

namespace TimerLib
{
    public class Class1
    {
        private Timer timer;

        /// <summary>
        ///     Holds the alapsed time in second
        /// </summary>
        private int elapsedTimeInSeconds;

        /// <summary>
        ///     Store a TimeSpan Value
        /// </summary>
        private TimeSpan remainingTime;

        public event EventHandler<ElapsedEventArgs> Elapsed;

        public Class1(int interval)
        {
            timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            elapsedTimeInSeconds++;
            OnElapsed(new ElapsedEventArgs(elapsedTimeInSeconds));
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Start timer
        /// </summary>
        public void Start()
        {
            timer.Start();
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Stops timer
        /// </summary>
        public void Stop()
        {
            timer.Stop();
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Pauses timer
        /// </summary>
        public void Pause()
        {
            remainingTime = TimeSpan.FromMilliseconds(timer.Interval);
            timer.Stop();
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Resumes timer from paused time
        /// </summary>
        public void Resume()
        {
            try
            {

                timer.Interval = (int)remainingTime.TotalMilliseconds;

            }
            catch (ArgumentException)
            {

            }
            timer.Start();
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     Ticks the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            elapsedTimeInSeconds++;
            OnElapsed(new ElapsedEventArgs(elapsedTimeInSeconds));
        }

        // ------------------------------------------------------------------ //
        /// <summary>
        ///     triggers elapsed event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnElapsed(ElapsedEventArgs e)
        {
            Elapsed?.Invoke(this, e);
        }
    }

    public class ElapsedEventArgs : EventArgs
    {
        public int ElapsedTimeInSeconds { get; }

        public ElapsedEventArgs(int elapsedTimeInSeconds)
        {
            ElapsedTimeInSeconds = elapsedTimeInSeconds;
        }
    }
}
