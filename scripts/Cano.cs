using Godot;
using System;

public class Cano : Node2D
{
    
    public override void _Ready()
    {
        
    }
    void AlguemEntrou(Node fora)
    {
        if(fora.Name == "RBJogador")
        {
            //GetChild(1).QueueFree();
            //GetChild(1).EmitSignal("hide");
            GetTree().Root.GetChild<main>(0).scoreAtual++;
            GetTree().Root.GetChild<main>(0).progresso++;
            GD.Print("enconstou e tem "+ GetTree().Root.GetChild<main>(0).progresso);
        } 

        
    }

}
