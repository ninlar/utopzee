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

    public void MainScene_FirstRoll(object sender, EventArgs e)
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].Disabled = _buttonDisabled[i];
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

        GetNode<Label>("OneLabel/Label").Text = total.ToString();
        MainScene.TotalScore += total;
        MainScene.RollsRemaining = Constants.DefaultRollsRemaining;
        _buttonDisabled[(int) DiceFrame.One] = true;
        MainScene.ForceRollNext();
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

        GetNode<Label>("TwoLabel/Label").Text = total.ToString();
        MainScene.TotalScore += total;
        MainScene.RollsRemaining = Constants.DefaultRollsRemaining;
        _buttonDisabled[(int) DiceFrame.Two] = true;
        MainScene.ForceRollNext();
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

        GetNode<Label>("ThreeLabel/Label").Text = total.ToString();
        MainScene.TotalScore += total;
        MainScene.RollsRemaining = Constants.DefaultRollsRemaining;
        _buttonDisabled[(int) DiceFrame.Three] = true;
        MainScene.ForceRollNext();
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

        GetNode<Label>("FourLabel/Label").Text = total.ToString();
        MainScene.TotalScore += total;
        MainScene.RollsRemaining = Constants.DefaultRollsRemaining;
        _buttonDisabled[(int) DiceFrame.Four] = true;
        MainScene.ForceRollNext();
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

        GetNode<Label>("FiveLabel/Label").Text = total.ToString();
        MainScene.TotalScore += total;
        MainScene.RollsRemaining = Constants.DefaultRollsRemaining;
        _buttonDisabled[(int) DiceFrame.Five] = true;
        MainScene.ForceRollNext();
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

        GetNode<Label>("SixLabel/Label").Text = total.ToString();
        MainScene.TotalScore += total;
        MainScene.RollsRemaining = Constants.DefaultRollsRemaining;
        _buttonDisabled[(int) DiceFrame.Six] = true;
        MainScene.ForceRollNext();
    }			
}
