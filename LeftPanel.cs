using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class LeftPanel : TextureRect
{
    private enum DiceFrame : int
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six
    }

    const int NumberOfDice = 5;
    const int DefaultRollsRemaining = 3;

    private IList<Dice> _dice = new List<Dice>();
    private IList<Button> _buttons = new List<Button>();
    private Main _mainScene;


    public Main MainScene
    {
        get { return _mainScene; }
        set
        {
            _mainScene = value;

            for (int i = 1; i <= NumberOfDice; i++)
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
        _buttons[(int) DiceFrame.One].Disabled = true;
        int total = 0;

        foreach (Dice die in _dice)
        {
            if (die.Frame == (int) DiceFrame.One)
            {
                total++;
            }
        }

        GetNode<Label>("OneLabel/Label").Text = total.ToString();
        MainScene.TotalScore += total;
        MainScene.RollsRemaining = DefaultRollsRemaining;
    }

    public void TwoClicked()
    {
        _buttons[(int) DiceFrame.Two].Disabled = true;
        int total = 0;

        foreach (Dice die in _dice)
        {
            if (die.Frame == (int) DiceFrame.Two)
            {
                total += 2;
            }
        }

        GetNode<Label>("TwoLabel/Label").Text = total.ToString();
        MainScene.TotalScore += total;
        MainScene.RollsRemaining = DefaultRollsRemaining;
    }

    public void ThreeClicked()
    {
        _buttons[(int) DiceFrame.Three].Disabled = true;
        int total = 0;

        foreach (Dice die in _dice)
        {
            if (die.Frame == (int) DiceFrame.Three)
            {
                total += 3;
            }
        }

        GetNode<Label>("ThreeLabel/Label").Text = total.ToString();
        MainScene.TotalScore += total;
        MainScene.RollsRemaining = DefaultRollsRemaining;
    }

    public void FourClicked()
    {
        _buttons[(int) DiceFrame.Four].Disabled = true;
        int total = 0;

        foreach (Dice die in _dice)
        {
            if (die.Frame == (int) DiceFrame.Four)
            {
                total += 4;
            }
        }

        GetNode<Label>("FourLabel/Label").Text = total.ToString();
        MainScene.TotalScore += total;
        MainScene.RollsRemaining = DefaultRollsRemaining;
    }

    public void FiveClicked()
    {
        _buttons[(int) DiceFrame.Five].Disabled = true;
        int total = 0;

        foreach (Dice die in _dice)
        {
            if (die.Frame == (int) DiceFrame.Five)
            {
                total += 5;
            }
        }

        GetNode<Label>("FiveLabel/Label").Text = total.ToString();
        MainScene.TotalScore += total;
        MainScene.RollsRemaining = DefaultRollsRemaining;
    }

    public void SixClicked()
    {
        _buttons[(int) DiceFrame.Six].Disabled = true;
        int total = 0;

        foreach (Dice die in _dice)
        {
            if (die.Frame == (int) DiceFrame.Six)
            {
                total += 6;
            }
        }

        GetNode<Label>("SixLabel/Label").Text = total.ToString();
        MainScene.TotalScore += total;
        MainScene.RollsRemaining = DefaultRollsRemaining;
    }			
}
