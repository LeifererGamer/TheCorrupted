using Godot;
using MegaCrit.Sts2.Core.Nodes.Combat;

namespace TheCorrupted.TheCorrupted.src.Core.Nodes.Combat;

[GlobalClass]
public partial class SNCreatureVisuals : NCreatureVisuals
{
    // Add a static reference for easy access from your logic classes
    public static SNCreatureVisuals? Instance { get; private set; }

    private AnimationPlayer? _anim;
    private Node2D? _visuals;

    public override void _Ready()
    {
        base._Ready();
        Instance = this; // Bind the instance when the node enters the tree

        _anim = GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        _visuals = GetNodeOrNull<Node2D>("Visuals");
        if (_anim != null)
        {
            _anim.AnimationFinished += _on_animation_player_animation_finished;
        }
    }

    public void _on_animation_player_animation_finished(StringName anim_name)
    {
        ModEntry.Logger.Info($"Animation finished: {anim_name}");
        if (anim_name != "idle")
        {
            _anim?.Play("idle");
        }
    }
}