/*using Godot;
using System;*/

/*public partial class player : CharacterBody2D*/
/*{

    [Export]
	public int Speed { get; set; } = 120;
	[Export]
	public bool keyGet;

	private Node Playr;
	private Area2D coin;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Playr = this;
		GD.Print(Playr);
		keyGet = false;
		coin = Coin.goldcoin;
		if(coin != null)
		{
			GD.Print("coin: ", coin.Name);
		}
		else
		{
			GD.Print("Can't find coin...");
		}
		
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 velocity = Vector2.Zero;

		if(Input.IsActionPressed("move_right"))
		{
			velocity.X += 1;
		}

		if(Input.IsActionPressed("move_left"))
		{
			velocity.X -= 1;
		}

		if(Input.IsActionPressed("move_down"))
		{
			velocity.Y += 1;
		}

		if(Input.IsActionPressed("move_up"))
		{
			velocity.Y -= 1;
		}

		if(velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
		}

        Position += velocity * (float)delta; //Needed to associate the player's position with the velocity...

		MoveAndSlide();

        
    }
    private static void OnArea2DBodyEntered(Node2D other)
    {
		//GD.Print(other.Name);

		if (other.Name == "EndLvlDoor") //There's a better way to check then I'll use that...
		{
			end_lvl_door.LevelComplete();
        }
		
		if(other.Name == "Coin")
		{
			Collectables.DestroyCollect(other);
		}
    }
}*/
