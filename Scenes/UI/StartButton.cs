using Godot;
using System;

public partial class StartButton : Button{
	[Export]
	public PackedScene mainGame;



	public override void _Ready(){
		Pressed += OnStartButtonPressed;

	}



	public void OnStartButtonPressed(){
		GetTree().ChangeSceneToPacked(mainGame);
	}
}
