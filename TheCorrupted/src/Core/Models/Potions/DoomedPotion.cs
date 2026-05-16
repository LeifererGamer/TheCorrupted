using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Commands;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;
using TheCorrupted.TheCorrupted.src.Core.Models.PotionPools;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Potions;



[Pool(typeof(CorruptedPotionPool))]
internal class DoomedPotion : CustomPotionModel
{
    public override PotionRarity Rarity => PotionRarity.Uncommon;

    public override PotionUsage Usage => PotionUsage.CombatOnly;

    public override TargetType TargetType => TargetType.Self;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<DoomPower>(10m)
    ];

    public override string CustomPackedImagePath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionImagePath();

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
    {
        NCombatRoom.Instance?.PlaySplashVfx(Owner.Creature, new Color("6876bd"));
        await PowerCmd.Apply<DoomPower>(choiceContext, [Owner.Creature], DynamicVars["DoomPower"].BaseValue, Owner.Creature, null);
    }
}