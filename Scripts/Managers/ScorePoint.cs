using Godot;
using System;

public partial class ScorePoint : CanvasLayer{
	[Export]
	public Label scoreNumber;



	public int score;
	


	public override void _Ready(){
		score = 0;
	}



	public void OnEnemyDeath(){
		score += 1;
		scoreNumber.Text = score.ToString();
	}



	public void OnPlayerDeath(){
		GetNode<PanelContainer>("PanelContainer2").Visible = true;
	}
}
