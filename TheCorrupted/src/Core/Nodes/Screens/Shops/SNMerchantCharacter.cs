using Godot;
using MegaCrit.Sts2.Core.Nodes.Screens.Shops;

namespace TheCorrupted.TheCorrupted.src.Core.Nodes.Screens.Shops;

[GlobalClass]
public partial class SNMerchantCharacter : NMerchantCharacter
{
    // 1. Override the _Ready method
    public override void _Ready()
    {

    }

    // 2. Hide the vanilla PlayAnimation method
    // We use the 'new' keyword because the vanilla method isn't marked as 'virtual'
    public new void PlayAnimation(string anim, bool loop = false)
    {

    }
}