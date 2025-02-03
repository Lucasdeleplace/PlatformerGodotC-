using Godot;
using System;

public partial class Ui : CanvasLayer
{
    [Export] public Label CoinLabel;
    [Export] private Label _timeLabel;
    [Export] private Label _bestTimeLabel;
    private Timer _timer;
    private double _elapsedTime = 0.0;

    public int currentCoins = 0;

    public override void _Ready()
    {
        _timer = new Timer();
        AddChild(_timer);
        _timer.WaitTime = 0.1;
        _timer.OneShot = false;
        _timer.Connect("timeout", new Callable(this, nameof(OnTimerTimeout)));
        _timer.Start();
        CoinLabel.Text = "X " + currentCoins.ToString() + "/" + 10;
        _bestTimeLabel.Text = "Best Time: " + GlobalData.Instance.BestTime.ToString("F1");
    }

    private void OnTimerTimeout()
    {
        _elapsedTime += _timer.WaitTime;
        _timeLabel.Text = "Time: " + _elapsedTime.ToString("F1");
    }

    public void StopTimer()
    {
        _timer.Stop();
        GD.Print("Time: " + _elapsedTime);
        if (_elapsedTime < GlobalData.Instance.BestTime)
        {
            GD.Print("Best Time: " + _elapsedTime);
            GlobalData.Instance.BestTime = _elapsedTime;
            _bestTimeLabel.Text = "Best Time: " + GlobalData.Instance.BestTime.ToString("F1");
        }
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        CoinLabel.Text = "X " + currentCoins.ToString() + "/" + 10;
    }
}