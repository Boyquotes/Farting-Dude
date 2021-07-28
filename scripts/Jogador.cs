using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public class Jogador : RigidBody2D
{
    [Export] public int forcaPeido = 450;
    [Export] public int escalaGravidade = 10;
    [Export] public int velocidade = 400;
    bool iniciou = true;
    bool morreu = false;
    Sprite dude, dudeFarting;
    Random r = new Random();

    bool peidando = false;
    
    public override void _Ready()
    {
        dude = GetChild<Sprite>(1);
        dudeFarting = GetChild<Sprite>(2);
        Sleeping = true;

    
    }
    public override void _Process(float delta)
    {
        // if(Input.IsMouseButtonPressed((int)ButtonList.Left)) Peidar();
        if(!iniciou && Input.IsActionJustPressed("Clique")) Iniciar();
        if(iniciou && Input.IsActionJustPressed("Clique") && !morreu) Peidar();
        if(morreu && Input.IsActionJustPressed("Clique")) Reiniciar();

    }
    void Iniciar()
    {
        Sleeping = !true;

        LinearVelocity = new Vector2(velocidade,300);
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
        GetChild<Sprite>(3).Show();
        GetChild<Sprite>(3).Rotate(r.Next(0,300));
        GetChild(3).GetChild<AudioStreamPlayer2D>(0).Play();
        AddChild(tela.Instance());
    }

    void Reiniciar()
    {
        GetTree().Root.GetChild<main>(0).progresso = 0;
        GetTree().Root.GetChild<main>(0).scoreAtual = 0;
        GetTree().ReloadCurrentScene();
    }
    async void Peidar()
    {
        //temporizador.Start(0.2f);
        float p = 1 + ((float)r.NextDouble()/2);
        GD.Print(p);
        GetChild<AudioStreamPlayer2D>(GetChildren().Count-1).PitchScale = p;

        GetChild<AudioStreamPlayer2D>(GetChildren().Count-1).Play();
        peidando = true;
        // dude.Show();
        dude.Hide();
        // dudeFarting.Hide();
        dudeFarting.Show();
        LinearVelocity = new Vector2(velocidade,0);
        ApplyImpulse(Vector2.Up, Vector2.Up*forcaPeido);

        await ToSignal(GetTree().CreateTimer(0.2f),"timeout");
        TrocarSprite();
    }
    void TrocarSprite()
    {
        dude.Show();

        dudeFarting.Hide();
        peidando = false;

    }
    void DesenharPeido()
    {
        
    }
    void EsconderPeido()
    {
        //GetChild<AudioStreamPlayer2D>(GetChildren().Count-1).Play();
        //GetChild(2).GetChild<AudioStreamPlayer2D>(0).Stop();
    }
    
}
