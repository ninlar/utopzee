using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

public partial class Main : Control
{
    const int MaxTurnsCompleted = 13;

    private bool goRight = true;
    private Vector2 prevPos = new Vector2();
    private bool isRolling = false;
    private AnimatedSprite2D male1;
    private AnimatedSprite2D wizard;
    private AnimatedSprite2D sheep;
    private PathFollow2D wizardLoc;
    private PathFollow2D male1Loc;
    private Button rollButton;
    private LeftPanel leftPanel;
    private MiddlePanel middlePanel;
    private RightPanel rightPanel;
    private TextureButton soundButton;
    private DicePad dicePad;
    private CanvasModulate moonlightModulate;
    private CanvasLayer fireworksLayer;
    private int _totalScore = 0;
    private int _bonusScore = 0;

    public event EventHandler MustRoll;
    public event EventHandler RollsCompleted;
    public event EventHandler FirstRoll;

    public int RollsRemaining { get; set; } = 3;
    public int TurnsCompleted { get; set; } = 0;
    public bool UtopzeeScored { get; set; } = false;
    public (int[], bool) GetInstanceCounts() => DicePad.GetInstanceCounts();
    public IList<Dice> DiceList => DicePad.DiceList;

    public bool CanLockDice => RollsRemaining > 0 && RollsRemaining != 3;
    public DicePad DicePad => dicePad;
    
    public int BonusScore
    {
        get { return _bonusScore; }
        set
        {
            _bonusScore = value;
            GetNode<Label>("RightPanel/BonusLabel/Label").Text = _bonusScore.ToString();
        }
    }

    public int TotalScore
    {
        get { return _totalScore; }
        set
        {
            _totalScore = value;
            GetNode<Label>("ScoreBanner/ScoreLabel").Text = $"Score: {value}";
        }
    }

    public void ResetForNextRoll(int additionalScore, bool addBonusUtopzee = false)
    {
        TotalScore += additionalScore;
        RollsRemaining = Constants.DefaultRollsRemaining;

		if (UtopzeeScored && addBonusUtopzee)
		{
			TotalScore += 100;
			BonusScore += 100;
		}

        TurnsCompleted++;
        ForceRollNext();
        UnlockDice();

        if (TurnsCompleted == MaxTurnsCompleted)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        rollButton.Disabled = true;
        fireworksLayer.Visible = true;
        moonlightModulate.Visible = true;
        GetNode<Label>("FireworksLayer/FinalScoreBanner/ScoreLabel").Text = $"Score: {TotalScore}";
    }

    private void ForceRollNext()
    {
        MustRoll?.Invoke(this, new EventArgs());
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        male1 = GetNode<AnimatedSprite2D>("Male1");
        wizard = GetNode<AnimatedSprite2D>("Wizard");
        sheep = GetNode<AnimatedSprite2D>("Sheep");
        rollButton = GetNode<Button>("RollButton");
        soundButton = GetNode<TextureButton>("SoundButton");
        leftPanel = GetNode<LeftPanel>("LeftPanel");
        leftPanel.MainScene = this;

        middlePanel = GetNode<MiddlePanel>("MiddlePanel");
        middlePanel.MainScene = this;

        rightPanel = GetNode<RightPanel>("RightPanel");
        rightPanel.MainScene = this;                
        dicePad = GetNode<DicePad>("DicePad");
        moonlightModulate = GetNode<CanvasModulate>("MoonlightModulate");
        fireworksLayer = GetNode<CanvasLayer>("FireworksLayer");

        male1Loc = GetNode<PathFollow2D>("Male1Path/PathFollow2D");
        wizardLoc = GetNode<PathFollow2D>("WizardPath/PathFollow2D");			

        ForceRollNext();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        rollButton.Text = $"{RollsRemaining} Rolls";
        rollButton.Disabled = (RollsRemaining <= 0);

        if (CanLockDice)
        {
            for (int i = 1; i <= 5; i++)
            {
                if(Input.IsActionJustPressed("Lock" + i))
                {
                    GetNode<Sprite2D>("DicePad/Lock" + i).Visible = !GetNode<Sprite2D>("DicePad/Lock" + i).Visible;
                }
            }
        }

        if (isRolling)
        {
            male1.Animation = "cheer";
            wizard.Animation = "cheer";
            
            return;
        }		
        
        delta = delta / 4.0;
        
        wizardLoc.ProgressRatio += (float) delta / 4.0f;
        
        if (goRight)
        {
            male1.Animation = "right";
            float newLoc = male1Loc.ProgressRatio + (float) delta;
            if (newLoc >= 1.0f)
            {
                goRight = false;
                male1Loc.ProgressRatio = 1.0f;
            }
            else
            {
                male1Loc.ProgressRatio += (float) delta;
            }
        }
        else
        {
            male1.Animation = "left";
            float newLoc = male1Loc.ProgressRatio - (float) delta;
            if (newLoc <= 0.0f)
            {
                goRight = true;
                male1Loc.ProgressRatio = 0.0f;
            }
            else
            {
                male1Loc.ProgressRatio -= (float) delta;
            }			
        }
        
        if (prevPos[0] < wizardLoc.Position[0])
        {
            wizard.Animation = "right";
        }
        else
        {
            wizard.Animation = "left";
        }
        
        prevPos = wizardLoc.Position;
        
        male1.Position = male1Loc.Position;
        wizard.Position = wizardLoc.Position;
    }
    
    public void OnTimer()
    {
        using(var random = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            for (int i = 1; i < 6; i++)
            {
                if (!GetNode<Sprite2D>("DicePad/Lock" + i).Visible)
                {
                    GetNode<AnimatedSprite2D>("DicePad/Die" + i).Frame = GetRandomNumber(random, 0, 6);
                }
            }
        }	
    }
    
    public void OnRollClicked()
    {
        GetNode<Timer>("DiceTimer").Start();
        GetNode<Timer>("RollTimer").Start();
        sheep.Animation = "cheer";
        isRolling = true;
        rollButton.Disabled = true;
        RollsRemaining--;
        
    }
    
    public void OnRollTimer()
    {
        sheep.Animation = "idle";
        GetNode<Timer>("DiceTimer").Stop();
        isRolling = false;

        if (RollsRemaining == 0)
        {
            RollsCompleted?.Invoke(this, new EventArgs());
        }

        if (RollsRemaining == 2)
        {
            FirstRoll?.Invoke(this, new EventArgs());
        }

        if (!CanLockDice)
        {
            UnlockDice();
        }       
    }

    private void UnlockDice()
    {
        for (int i = 1; i <= 5; i++)
        {
            GetNode<Sprite2D>("DicePad/Lock" + i).Visible = false;
        }        
    }

    private int GetRandomNumber(System.Security.Cryptography.RandomNumberGenerator rng, int minValue, int maxValue)
    {
        byte[] randomNumber = new byte[4];
        rng.GetBytes(randomNumber);
        int value = BitConverter.ToInt32(randomNumber, 0);
        return Math.Abs(value % (maxValue - minValue)) + minValue;
    }

    public void OnSoundClicked()
    {
        GetNode<AudioStreamPlayer>("BackgroundMusic").Playing = !GetNode<AudioStreamPlayer>("BackgroundMusic").Playing;
    }
}
