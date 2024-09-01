using Godot;
using System;

public partial class Main : Control
{
    private Random random = new Random();
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
    private TextureButton soundButton;
    private DicePad dicePad;

    public event EventHandler MustRoll;
    public event EventHandler RollsCompleted;
    public event EventHandler FirstRoll;

    public int RollsRemaining { get; set; } = 3;
    public int TotalScore { get; set; } = 0;
    public bool CanLockDice => RollsRemaining > 0 && RollsRemaining != 3;
    public DicePad DicePad => dicePad;
    
    public void ForceRollNext()
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
        dicePad = GetNode<DicePad>("DicePad");

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
        for (int i = 1; i < 6; i++)
        {
            if (!GetNode<Sprite2D>("DicePad/Lock" + i).Visible)
            {
                GetNode<AnimatedSprite2D>("DicePad/Die" + i).Frame = random.Next(6);
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
            for (int i = 1; i <= 5; i++)
            {
                GetNode<Sprite2D>("DicePad/Lock" + i).Visible = false;
            }
        }       
    }

    public void OnSoundClicked()
    {
        GetNode<AudioStreamPlayer>("BackgroundMusic").Playing = !GetNode<AudioStreamPlayer>("BackgroundMusic").Playing;
    }
}
