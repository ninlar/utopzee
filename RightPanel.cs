using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class RightPanel : TextureRect
{
	const int NumberOfButtons = 3;
	const int IndexThreeKind = 0;
	const int IndexFourKind = 1;
	const int IndexChance = 2;

	private IList<Dice> _dice = new List<Dice>(Constants.NumberOfDice);
	private IList<Button> _buttons = new List<Button>(NumberOfButtons);
    private bool[] _buttonDisabled = new bool[NumberOfButtons];
    private Main _mainScene;


    public Main MainScene
    {
        get { return _mainScene; }
        set
        {
            _mainScene = value;

            for (int i = 1; i <= Constants.NumberOfDice; i++)
            {
                _dice.Add(value.GetNode<Dice>("DicePad/Die" + i));
            }

			_buttons.Add(GetNode<Button>("ThreeKindButton"));
			_buttons.Add(GetNode<Button>("FourKindButton"));
			_buttons.Add(GetNode<Button>("ChanceButton"));

            value.MustRoll += MainScene_MustRoll;
            value.FirstRoll += MainScene_FirstRoll;
        }
    }

    public void MainScene_MustRoll(object sender, EventArgs e)
    {
        foreach(Button button in _buttons)
        {
            button.Disabled = true;
        }
    }

    public void MainScene_FirstRoll(object sender, EventArgs e)
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].Disabled = _buttonDisabled[i];
        }
    }

	private int[] GetInstanceCounts()
	{
		int[] instanceCounts = new int[(int)DiceValue.Six];

		foreach(Dice die in _dice)
		{
			instanceCounts[die.Frame]++;
		}

		return instanceCounts;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnChance()
	{
		int[] instanceCounts = GetInstanceCounts();
		
		bool foundFive = false;

		foreach(int count in instanceCounts)
		{
			if (count == 5) foundFive = true;
		}
		
		int total = 0;

		foreach(Dice die in _dice)
		{
			total += (int) die.Value;
		}

        GetNode<Label>("ChanceLabel/Label").Text = total.ToString();
        _buttonDisabled[IndexChance] = true;

		MainScene.ResetForNextRoll(total, foundFive);
	}

	public void OnThreeKind()
	{
		int[] instanceCounts = GetInstanceCounts();
		
		bool foundFive = false;
		bool foundThree = false;
		bool foundFour = false;

		foreach(int count in instanceCounts)
		{
			if (count == 5) foundFive = true;
			if (count == 3) foundThree = true;
			if (count == 4) foundFour = true;
		}
		
		int total = 0;

		if (foundFive || foundThree || foundFour)
		{
			foreach(Dice die in _dice)
			{
				total += (int) die.Value;
			}
		}

        GetNode<Label>("ThreeKindLabel/Label").Text = total.ToString();
        _buttonDisabled[IndexThreeKind] = true;

		MainScene.ResetForNextRoll(total, foundFive);
	}

	public void OnFourKind()
	{
		int[] instanceCounts = GetInstanceCounts();
		
		bool foundFive = false;
		bool foundFour = false;

		foreach(int count in instanceCounts)
		{
			if (count == 5) foundFive = true;
			if (count == 4) foundFour = true;
		}
		
		int total = 0;

		if (foundFive || foundFour)
		{
			foreach(Dice die in _dice)
			{
				total += (int) die.Value;
			}
		}

        GetNode<Label>("FourKindLabel/Label").Text = total.ToString();
        _buttonDisabled[IndexFourKind] = true;

		MainScene.ResetForNextRoll(total, foundFive);
	}
}
