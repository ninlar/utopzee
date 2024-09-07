using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class RightPanel : TextureRect
{
	const int NumberOfButtons = 3;

	private IList<Button> _buttons = new List<Button>(NumberOfButtons);
    private bool[] _buttonDisabled = new bool[NumberOfButtons];
    private Main _mainScene;


    public Main MainScene
    {
        get { return _mainScene; }
        set
        {
            _mainScene = value;

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

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
