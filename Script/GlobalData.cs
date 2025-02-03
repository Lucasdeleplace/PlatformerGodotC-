using Godot;
using System;

public partial class GlobalData : Node
{
    private static GlobalData _instance;

    public static GlobalData Instance => _instance ??= new GlobalData();

    public double BestTime { get; set; } = 9999.9;
}