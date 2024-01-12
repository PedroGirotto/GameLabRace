using Godot;
using System;


public partial class MovementEnemy2 : Node2D{
	// * variaveis editaveis no godot
	[Export]
	public int MinimumSpeed {get;set;} = 1;
	[Export]
	public int MaximumSpeed {get;set;} = 10;
	[Export]
	public int MinimumAmplitude {get;set;} = 10;
	[Export]
	public int MaximumAmplitude {get;set;} = 50;


	private double velocity = 0;
	private int speed = 0;
	private double amplitude = 0;
	private int direction;
	private AudioStreamPlayer audioEffect;



	public override void _Ready(){
		audioEffect = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		audioEffect.PitchScale = (float) (1 + GD.Randf() - 0.5*GD.Randf());
		
		speed = GD.RandRange(MinimumSpeed, MaximumSpeed);
		amplitude = GD.RandRange(MinimumAmplitude, MaximumAmplitude);

		// * utilizando equações booleanas para evitar a utilização do laço de seleção
		direction = (-1)*Convert.ToInt16(speed%2 == 0) + 1*Convert.ToInt16(speed%2 == 1);
	}



	public override void _Process(double delta){
		velocity += delta*speed;
		Position = new Vector2(
			x: Position.X + (float) (direction*amplitude*Mathf.Sin(velocity)),
			y: Position.Y + (float) velocity
		);
	}



		// * detecção de colisão
	private void OnArea2DEntered(Area2D area){
		audioEffect.Play();
		GetNode<Area2D>("Area2D").Hide();
		GetNode<Sprite2D>("Sprite2D").Hide();
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
