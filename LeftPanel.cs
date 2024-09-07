using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class LeftPanel : TextureRect
{
    const int NumberOfButtons = 6;

    private IList<Dice> _dice = new List<Dice>(Constants.NumberOfDice);
    private IList<Button> _buttons = new List<Button>(NumberOfButtons);
    private bool[] _buttonDisabled = new bool[NumberOfButtons];
    private Main _mainScene;
    public bool PanelCompleted { get; set; } = false;
    public int PanelTotal { get; set; } = 0;


    public Main MainScene
    {
        get { return _mainScene; }
        set
        {
            _mainScene = value;

            for (int i = 1; i <= Constants.NumberOfDice; i++)
            {
                _dice.Add(value.GetNode<Dice>("DicePad/Die" + i));
                _buttons.Add(GetNode<Button>("Button" + i));
            }

            _buttons.Add(GetNode<Button>("Button6"));

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

	private int[] GetInstanceCounts()
	{
		int[] instanceCounts = new int[(int)DiceValue.Six];

		foreach(Dice die in _dice)
		{
			instanceCounts[die.Frame]++;
		}

		return instanceCounts;
	}

    public void MainScene_FirstRoll(object sender, EventArgs e)
    {
        bool allDisabled = true;
        for (int i = 0; i < _buttons.Count; i++)
        {
            if (!_buttonDisabled[i])
            {
                allDisabled = false;
            }

            _buttons[i].Disabled = _buttonDisabled[i];
        }

        if (allDisabled && !PanelCompleted)
        {
            if (PanelTotal >= 63)
            {
                MainScene.TotalScore += 35;
                MainScene.BonusScore += 35;
                PanelCompleted = true;
            }
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
        _buttons[(int) DiceFrame.One].Disabled = true;
        int total = 0;

        foreach (Dice die in _dice)
        {
            if (die.Value == DiceValue.One)
            {
                total++;
            }
        }

		int[] instanceCounts = GetInstanceCounts();
		
		bool foundFive = false;

		foreach(int count in instanceCounts)
		{
			if (count == 5) foundFive = true;
		}        

        GetNode<Label>("OneLabel/Label").Text = total.ToString();
        PanelTotal += total;
        _buttonDisabled[(int) DiceFrame.One] = true;
        MainScene.ResetForNextRoll(total, foundFive);
    }

    public void TwoClicked()
    {
        _buttons[(int) DiceFrame.Two].Disabled = true;
        int total = 0;

        foreach (Dice die in _dice)
        {
            if (die.Value == DiceValue.Two)
            {
                total += (int) DiceValue.Two;
            }
        }

		int[] instanceCounts = GetInstanceCounts();
		
		bool foundFive = false;

		foreach(int count in instanceCounts)
		{
			if (count == 5) foundFive = true;
		}         

        GetNode<Label>("TwoLabel/Label").Text = total.ToString();
        PanelTotal += total;
        _buttonDisabled[(int) DiceFrame.Two] = true;
        MainScene.ResetForNextRoll(total, foundFive);
    }

    public void ThreeClicked()
    {
        _buttons[(int) DiceFrame.Three].Disabled = true;
        int total = 0;

        foreach (Dice die in _dice)
        {
            if (die.Value == DiceValue.Three)
            {
                total += (int) DiceValue.Three;
            }
        }

		int[] instanceCounts = GetInstanceCounts();
		
		bool foundFive = false;

		foreach(int count in instanceCounts)
		{
			if (count == 5) foundFive = true;
		}         

        GetNode<Label>("ThreeLabel/Label").Text = total.ToString();
        PanelTotal += total;
        _buttonDisabled[(int) DiceFrame.Three] = true;
        MainScene.ResetForNextRoll(total, foundFive);
    }

    public void FourClicked()
    {
        _buttons[(int) DiceFrame.Four].Disabled = true;
        int total = 0;

        foreach (Dice die in _dice)
        {
            if (die.Value == DiceValue.Four)
            {
                total += (int) DiceValue.Four;
            }
        }

		int[] instanceCounts = GetInstanceCounts();
		
		bool foundFive = false;

		foreach(int count in instanceCounts)
		{
			if (count == 5) foundFive = true;
		}         

        GetNode<Label>("FourLabel/Label").Text = total.ToString();
        PanelTotal += total;
        _buttonDisabled[(int) DiceFrame.Four] = true;
        MainScene.ResetForNextRoll(total, foundFive);
    }

    public void FiveClicked()
    {
        _buttons[(int) DiceFrame.Five].Disabled = true;
        int total = 0;

        foreach (Dice die in _dice)
        {
            if (die.Value == DiceValue.Five)
            {
                total += (int) DiceValue.Five;
            }
        }

		int[] instanceCounts = GetInstanceCounts();
		
		bool foundFive = false;

		foreach(int count in instanceCounts)
		{
			if (count == 5) foundFive = true;
		}         

        GetNode<Label>("FiveLabel/Label").Text = total.ToString();
        PanelTotal += total;
        _buttonDisabled[(int) DiceFrame.Five] = true;
        MainScene.ResetForNextRoll(total, foundFive);
    }

    public void SixClicked()
    {
        _buttons[(int) DiceFrame.Six].Disabled = true;
        int total = 0;

        foreach (Dice die in _dice)
        {
            if (die.Value == DiceValue.Six)
            {
                total += (int) DiceValue.Six;
            }
        }

		int[] instanceCounts = GetInstanceCounts();
		
		bool foundFive = false;

		foreach(int count in instanceCounts)
		{
			if (count == 5) foundFive = true;
		}         

        GetNode<Label>("SixLabel/Label").Text = total.ToString();
        PanelTotal += total;
        _buttonDisabled[(int) DiceFrame.Six] = true;
        MainScene.ResetForNextRoll(total, foundFive);
    }			
}
