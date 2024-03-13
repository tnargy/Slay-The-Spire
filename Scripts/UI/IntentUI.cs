using Godot;

public partial class IntentUI : HBoxContainer
{
    TextureRect icon;
    Label number;

    public override void _Ready()
    {
        icon = GetNode<TextureRect>("Icon");
        number = GetNode<Label>("Number");
    }

    public void UpdateIntent(Intent intent)
    {
        if (intent == null) 
        { 
            Hide();
            return; 
        }
        
        icon.Texture = intent.icon;
        icon.Visible = icon.Texture != null;
        number.Text = intent.number;
        number.Visible = number.Text.Length > 0;
        Show();
    }
}
