using Godot;
using System;

public class Inicio : CanvasLayer
{
    public override void _Ready()
    {
        
    }
    public override void _Process(float delta)
    {
        if(Input.IsActionJustPressed("Clique"))
        {
            QueueFree();
        }
    }

}
