using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class LeftPanel : TextureRect
{
	private IList<Dice> _dice = new List<Dice>();
	private IList<Button> _buttons = new List<Button>();
	private Main _mainScene;


	public Main MainScene
	{
		get { return _mainScene; }
		set
		{
			_mainScene = value;

			for (int i = 1; i <= 5; i++)
			{
				_dice.Add(value.GetNode<Dice>("DicePad/Die" + i));
				_buttons.Add(GetNode<Button>("Button" + i));
			}

			_buttons.Add(GetNode<Button>("Button6"));			
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OneClicked()
	{
		_buttons[0].Disabled = true;
		int total = 0;

		foreach (Dice die in _dice)
		{
			if (die.Frame == 0)
			{
				total++;
			}
		}

		GetNode<Label>("OneLabel/Label").Text = total.ToString();
		MainScene.TotalScore += total;
		MainScene.RollsRemaining = 3;
	}
}
