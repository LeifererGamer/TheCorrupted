using Godot;
using MegaCrit.Sts2.Core.Nodes.Screens.Shops;

namespace TheCorrupted.src.Core.Nodes.Screens.Shops;

[GlobalClass]
public partial class SNMerchantCharacter : NMerchantCharacter
{
    // 1. Override the _Ready method
    public override void _Ready()
    {
        // DO NOT write base._Ready() here! 
        // By leaving it out, we completely stop the vanilla game from
        // trying to run the Spine logic when the room loads.

        GD.Print("Cursed Merchant spawned successfully without Spine!");
    }

    // 2. Hide the vanilla PlayAnimation method
    // We use the 'new' keyword because the vanilla method isn't marked as 'virtual'
    public new void PlayAnimation(string anim, bool loop = false)
    {
        // This stops the game from crashing if it tries to make the 
        // merchant talk, flinch, or idle later on.
        GD.Print($"Vanilla game asked to play animation: {anim}, but we are ignoring it.");

        // Later, you can add your own animation logic here!
        // For example, if you add an AnimationPlayer node:
        // GetNode<AnimationPlayer>("AnimationPlayer").Play(anim);
    }
}