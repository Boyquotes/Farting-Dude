using Godot;
using System;

public class Jogador : RigidBody2D
{
    [Export] public int forcaPeido = 450;
    [Export] public int escalaGravidade = 10;
    [Export] public int velocidade = 400;
    bool iniciou = true;
    bool morreu = false;
    public override void _Ready()
    {
        Sleeping = true;
    }
    public override void _Process(float delta)
    {
        // if(Input.IsMouseButtonPressed((int)ButtonList.Left)) Peidar();
        if(iniciou && Input.IsActionJustPressed("Clique") && !morreu) Peidar();
        if(!iniciou && Input.IsActionJustPressed("Clique")) Iniciar();
        if(morreu && Input.IsActionJustPressed("Clique")) Reiniciar();
    }
    void Iniciar()
    {
        Sleeping = !true;

        LinearVelocity = new Vector2(velocidade,0);
        GravityScale = escalaGravidade;
    }
    public void Testar(Node fora)
    {
        //GD.Print("encostou?");
        morreu = true;
        Sleeping = true;

        GD.Print(fora.Name);
        GD.Print("morreru");
        if(GetTree().Root.GetChild<main>(0).scoreAtual > GetTree().Root.GetChild<main>(0).highscore)
        {
            GetTree().Root.GetChild<main>(0).highscore = GetTree().Root.GetChild<main>(0).scoreAtual;
            GD.Print("highscore: "+GetTree().Root.GetChild<main>(0).highscore);

            
        }
        PackedScene tela = (PackedScene)GD.Load("res://objetos/Restart.tscn");
            
        AddChild(tela.Instance());
    }
    void Reiniciar()
    {
        GetTree().Root.GetChild<main>(0).progresso = 0;
        GetTree().Root.GetChild<main>(0).scoreAtual = 0;
        GetTree().ReloadCurrentScene();
    }
    void Peidar()
    {
        LinearVelocity = new Vector2(velocidade,0);
        ApplyImpulse(Vector2.Up, Vector2.Up*forcaPeido);
    }
}
