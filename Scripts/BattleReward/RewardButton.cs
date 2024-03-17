using Godot;

public partial class RewardButton : Button
{
	[Export] Texture2D _rewardIcon;
	public Texture2D rewardIcon
	{
		get => _rewardIcon;
		set => SetRewardIcon(value);
	}
	[Export] string _rewardText;
	public string rewardText
	{
		get => _rewardText;
		set => SetRewardText(value);
	}
	[Export] TextureRect customIcon;
	[Export] Label customText;

    public override void _Ready()
    {
        SetRewardIcon(rewardIcon);
        SetRewardText(rewardText);
    }

    void SetRewardIcon(Texture2D value)
	{
		_rewardIcon = value;
		customIcon.Texture = _rewardIcon;
	}

	void SetRewardText(string value)
	{
		_rewardText = value;
		customText.Text = _rewardText;
	}

    void OnPressed()
	{
		QueueFree();
	}
}
