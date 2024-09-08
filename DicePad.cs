using Godot;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public partial class DicePad : Node2D
{
    private IList<Dice> _dice = new List<Dice>();

    public IList<Dice> DiceList => _dice;

    public (int[] instanceCounts, bool utopzeeFound) GetInstanceCounts()
    {
		int[] instanceCounts = new int[(int)DiceValue.Six];
        bool utopzeeFound = false;

		foreach(Dice die in _dice)
		{
			instanceCounts[die.Frame]++;
		}

        utopzeeFound = instanceCounts.Contains(5);

		return (instanceCounts, utopzeeFound);        
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        for (int i = 1; i <= Constants.NumberOfDice; i++)
        {
            _dice.Add(GetNode<Dice>("Die" + i));
        }        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
