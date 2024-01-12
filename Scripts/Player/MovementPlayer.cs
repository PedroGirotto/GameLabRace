using Godot;
using System;


public partial class MovementPlayer : Node2D{
	// * variaveis editaveis no godot
	[Export]
	public int Speed {get;set;} = 400;


	// * variaveis locais
	public Vector2 ScreenSize;
	public AnimatedSprite2D animation;
	public int XSpriteSize;
	public int YSpriteSize;
	public AudioStreamPlayer soundEffect;
	public bool alive = true;


	// * inicialização dos valores e posição inicial
	public override void _Ready(){
		ScreenSize = GetViewportRect().Size;
		animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		soundEffect = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		XSpriteSize = (int) animation.SpriteFrames.GetFrameTexture("default",0).GetSize()[0];
		YSpriteSize = (int) animation.SpriteFrames.GetFrameTexture("default",0).GetSize()[1];

		Position = new Vector2(
			x: ScreenSize.X/2,
			y: ScreenSize.Y - YSpriteSize/2 - 10
		);

		animation.Play("default");
	}



	// * processamento a cada frame
	public override void _Process(double delta){
		Vector2 Velocity = Vector2.Zero;

		if(Input.IsActionPressed("move_right") && alive){
			Velocity.X += 1;
		}

		if(Input.IsActionPressed("move_left") && alive){
			Velocity.X -= 1;
		}

		if(Velocity.Length() > 0){
			Velocity = Velocity.Normalized() * Speed;
		}

		Position += Velocity * (float) delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, ScreenSize.X*0.18f + XSpriteSize/2, ScreenSize.X*0.85f - XSpriteSize/2),
			y: Position.Y
		);
	}



	// * detecção de colisão
	private void OnArea2DEntered(Area2D area){
		// ! Aparecer aqui a UI de Game Over e voltar para o menu principal
		if(alive){
			alive = false;
			GetNode<Area2D>("Area2D");
			animation.Stop();
			animation.Play("explosion");
			soundEffect.Play();
		}
	}



	private void OnExplosionEnd(){
		if(animation.Animation.ToString() == "explosion"){
			QueueFree();
		}
	}
}
