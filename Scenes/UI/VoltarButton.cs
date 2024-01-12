using Godot;
using System;

public partial class VoltarButton : Button{



	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		Pressed += OnVoltarPressed;
	}



	public void OnVoltarPressed(){
		GetTree().ChangeSceneToFile("res://Scenes/UI/MainMenu.tscn");
	}
}
