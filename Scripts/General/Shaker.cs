using System;
using Godot;

public partial class Shaker : Node
{
    public static void Shake(Node2D thing, float strength, float duration = 0.2f)
    {
        if (thing == null) { return; }

        var origPos = thing.Position;
        var shakeCount = 10;
        var tween = thing.CreateTween();

        for (int i = 0; i < shakeCount; i++)
        {
            var shakeOffset = new Vector2((float)((Random.Shared.NextDouble() * 2.0) - 1.0), (float)((Random.Shared.NextDouble() * 2.0) - 1.0));
            var target = origPos + strength * shakeOffset;
            if (i % 2 == 0) { target = origPos; }

            tween.TweenProperty(thing, "position", target, duration / (float)shakeCount);
            strength *= 0.75f;
        }

        tween.Finished += () => thing.Position = origPos;
    }
}
