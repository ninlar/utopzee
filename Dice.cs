using Godot;
using System;

public partial class Dice : AnimatedSprite2D
{
    public int Value => this.Frame + 1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        Main mainScene = GetNode<Main>("../..");

        if (mainScene.CanLockDice && @event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == MouseButton.Left && mouseButton.Pressed)
            {
                if(Math.Abs(this.GlobalPosition.DistanceTo(mouseButton.GlobalPosition)) < 20.0f)
                {
                    string lockName = "../Lock" + this.Name.ToString()[this.Name.ToString().Length - 1];
                    GetNode<Sprite2D>(lockName).Visible = !GetNode<Sprite2D>(lockName).Visible;
                }

            }
        }
    }
}
