using System;
using System.Windows.Forms;

public class GameTimer
{
    private Timer timer;
    private int elapsedTimeInSeconds;
    private TimeSpan remainingTime;  // Remaining time in the timer

    public event EventHandler<ElapsedEventArgs> Elapsed;

    public int ElapsedTimeInSeconds => elapsedTimeInSeconds;

    public GameTimer(int interval)
    {
        timer = new Timer();
        timer.Interval = interval;
        timer.Tick += Timer_Tick;
    }

    public void Start()
    {
       // elapsedTimeInSeconds = 0;
        timer.Start();
    }

    public void Stop()
    {
        timer.Stop();
    }

    public void Pause()
    {
        remainingTime = TimeSpan.FromMilliseconds(timer.Interval);
        timer.Stop();
    }

    public void Resume()
    {
        timer.Interval = (int)remainingTime.TotalMilliseconds;
        timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        elapsedTimeInSeconds++;
        OnElapsed(new ElapsedEventArgs(elapsedTimeInSeconds));
    }

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
