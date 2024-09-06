using Godot;
using System;

public partial class Dice : AnimatedSprite2D
{
    public int Value => this.Frame + 1;

    // This represents which 
    public int Ordinal => this.Name.ToString()[^1] - '0';

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void OnMouseInput(Node viewPort, InputEvent @event, int shapeIdx)
    {
        Main mainScene = GetNode<Main>("../..");

        if (mainScene.CanLockDice && @event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == MouseButton.Left && mouseButton.Pressed)
            {
                string lockName = "../Lock" + Ordinal;
                GetNode<Sprite2D>(lockName).Visible = !GetNode<Sprite2D>(lockName).Visible;
            }
        }        
    }
}
