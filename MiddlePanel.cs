using Godot;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public partial class MiddlePanel : TextureRect
{
	const int NumberOfButtons = 4;
	const int IndexFullHouse = 0;
	const int IndexSmallRun = 1;
	const int IndexLargeRun = 2;
	const int IndexUtopzee = 3;

    private IList<Button> _buttons = new List<Button>(NumberOfButtons);
    private bool[] _buttonDisabled = new bool[NumberOfButtons];	
    private Main _mainScene;


    public Main MainScene
    {
        get { return _mainScene; }
        set
        {
            _mainScene = value;

			_buttons.Add(GetNode<Button>("FullHouseButton"));
			_buttons.Add(GetNode<Button>("SmallRunButton"));
			_buttons.Add(GetNode<Button>("LargeRunButton"));
			_buttons.Add(GetNode<Button>("UtopzeeButton"));

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

	public void OnFullHouse()
	{
		(int[] instanceCounts, bool foundFive) = MainScene.GetInstanceCounts();
		
		bool foundThree = false;
		bool foundTwo = false;

		foreach(int count in instanceCounts)
		{
			if (count == 3) foundThree = true;
			if (count == 2) foundTwo = true;
		}
		
		int total = 0;

		if (foundFive || (foundThree && foundTwo))
		{
			total += 25;
		}

        GetNode<Label>("FullHouseLabel/Label").Text = total.ToString();
        _buttonDisabled[IndexFullHouse] = true;

		MainScene.ResetForNextRoll(total, foundFive);
	}

	public void OnSmallRun()
	{
		(int[] instanceCounts, bool foundFive) = MainScene.GetInstanceCounts();
		
		int consecutiveCount = 0;
		int maxConsecutive = 0;

		foreach(int count in instanceCounts)
		{	
			if (count > 0)
			{
				consecutiveCount++;
			}
			else
			{
				if (consecutiveCount > maxConsecutive)
				{
					maxConsecutive = consecutiveCount;
				}

				consecutiveCount = 0;
			}
		}

		if (consecutiveCount > maxConsecutive)
		{
			maxConsecutive = consecutiveCount;
		}
		
		int total = 0;

		if (foundFive || maxConsecutive >= 4)
		{
			total += 30;
		}

        GetNode<Label>("SmallRunLabel/Label").Text = total.ToString();
        _buttonDisabled[IndexSmallRun] = true;

		MainScene.ResetForNextRoll(total, foundFive);
	}

	public void OnLargeRun()
	{
		(int[] instanceCounts, bool foundFive) = MainScene.GetInstanceCounts();
		
		int notOneCount = 0;

		foreach(int count in instanceCounts)
		{
			if (count != 1) notOneCount++;
		}
		
		int total = 0;

		if (foundFive || (notOneCount == 1 &&
			(instanceCounts[0] == 0 ^ instanceCounts[^1] == 0)))
		{
			total += 40;
		}

        GetNode<Label>("LargeRunLabel/Label").Text = total.ToString();
        _buttonDisabled[IndexLargeRun] = true;

		MainScene.ResetForNextRoll(total, foundFive);
	}

	public void OnUtopzee()
	{
		(int[] instanceCounts, bool foundFive) = MainScene.GetInstanceCounts();
		
		int total = foundFive ? 50 : 0;

        GetNode<Label>("UtopzeeLabel/Label").Text = total.ToString();
        _buttonDisabled[IndexUtopzee] = true;
		MainScene.UtopzeeScored = foundFive;
		MainScene.ResetForNextRoll(total, false);	
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
