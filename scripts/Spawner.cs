using Godot;
using System;
using System.Collections.Generic;

public class Spawner : Node2D
{
    PackedScene cano = (PackedScene)GD.Load("res://objetos/Cano.tscn");
    Stack<Node2D> lista = new Stack<Node2D>();
    [Export] public int distancia = 500;
    int min = 100, max = 600;
    Random r = new Random();
    Vector2 pos;
    public override void _Ready()
    {
        pos = new Vector2(500,r.Next(min,max));
        for (int i = 1; i < 4; i++)
        {
            Node2D a = cano.Instance<Node2D>();
            a.Position = pos;
            
            pos = new Vector2(distancia*i,0) + new Vector2(distancia,r.Next(min,max));
            lista.Push(a);
            AddChild(a);
        }
    }
    public override void _Process(float delta)
    {
        if(GetTree().Root.GetChild<main>(0).progresso == 3)
        {
            TrocarPosicao();
        }
    }
    void TrocarPosicao()
    {
        GetTree().Root.GetChild<main>(0).progresso--;
        GD.Print("pediu pra trocar e tem "+GetTree().Root.GetChild<main>(0).progresso);
        MoveChild(GetChild<Node2D>(0),GetChildren().Count-1);
        Node2D a = GetChild<Node2D>(0);
        pos = new Vector2(distancia*GetTree().Root.GetChild<main>(0).scoreAtual,0) + new Vector2(distancia,r.Next(min,max));
        a.Position = pos;
    }

}
