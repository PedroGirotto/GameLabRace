using Godot;
using System;


public partial class MovementEnemy : Node2D{
	// * variaveis editaveis no godot
	[Export]
	public int MinimumSpeed {get;set;} = 100;
	[Export]
	public int MaximumSpeed {get;set;} = 500;


	// * varuaveis locais
	public Vector2 Velocity;
	public AudioStreamPlayer audioEffect;



	public override void _Ready(){
		audioEffect = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		Velocity = Vector2.Zero;
		Velocity.Y += 1;
		Velocity = Velocity.Normalized() * GD.RandRange(MinimumSpeed, MaximumSpeed);
	}



	public override void _Process(double delta){
		Position += Velocity * (float) delta;
	}



		// * detecção de colisão
	private void OnArea2DEntered(Area2D area){
		audioEffect.Play();
		GetNode<Sprite2D>("Sprite2D").Hide();
		GetNode<Area2D>("Area2D").Hide();
	}



	private void OnSoundEnd(){
		GetParent().EmitSignal("EnemyDeath");
		QueueFree();
	}



	// * Deletar objeto quando sair da tela
	private void OnScreenExited(){
		GetParent().EmitSignal("EnemyDeath");
		QueueFree();
	}
}
