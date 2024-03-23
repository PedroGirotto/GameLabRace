using Godot;
using System;

public partial class CreditButton : Button
{
	[Export]
	public TextEdit creditoEsquerdo { get; set; } = new TextEdit();
	
	[Export]
	public TextEdit creditoDireito { get; set; } = null;

	public bool visibilidade;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		visibilidade = false;
		Pressed += OnCreditButtonPressed;
	}

	public void OnCreditButtonPressed()
	{
		visibilidade = !visibilidade;
		creditoDireito.Visible = visibilidade;
		creditoEsquerdo.Visible = visibilidade;
	}
}
