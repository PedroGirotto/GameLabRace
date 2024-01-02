using Godot;
using System;


public partial class MonsterManager : Node{
	// * variaveis editaveis no godot
	[Export]
	public Godot.Collections.Array<PackedScene> Enemies {set;get;}
	[Export]
	public float RandomSpawnTime = 0.5f;
	[Export]
	public bool spawn = true;


	[Signal]
	public delegate void EnemyDeathEventHandler();


	// * variaveis locais
	public PathFollow2D EnemiespawnLocation;
	public Timer MonsterSpawnTimer;
	public int RandomSpawnEnemy;



	public override void _Ready(){
		EnemiespawnLocation = GetNode<PathFollow2D>("Path2D/PathFollow2D");
		MonsterSpawnTimer = GetNode<Timer>("MonsterSpawnTimer");
	}



	private void OnTimeOut(){
		if(spawn){
			EnemiespawnLocation.ProgressRatio = GD.Randf();
			RandomSpawnEnemy = GD.RandRange(0, Enemies.Count-1);

			Node2D EnemyCar = Enemies[RandomSpawnEnemy].Instantiate<Node2D>();
			EnemyCar.Position = EnemiespawnLocation.Position;
			AddChild(EnemyCar);
			
			MonsterSpawnTimer.WaitTime = GD.Randf() + RandomSpawnTime;
		}
	}



	private void OnPlayerDeath(){
		spawn = false;
		QueueFree();
	}
}
