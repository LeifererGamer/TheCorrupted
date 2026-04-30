

using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using TheCorrupted.TheCorrupted.src.Core.Models.Commands;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;
using TheCorrupted.TheCorrupted.src.Core.Models.PotionPools;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Potions;

[Pool(typeof(CorruptedPotionPool))]
public sealed class SoulBrew : CustomPotionModel
    {
        public override PotionRarity Rarity => PotionRarity.Uncommon;

        public override PotionUsage Usage => PotionUsage.CombatOnly;

        public override TargetType TargetType => TargetType.Self;

        protected override IEnumerable<DynamicVar> CanonicalVars => 
        [
            new ArmyVar(15m)
        ];

        public override string CustomPackedImagePath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionImagePath();

    protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature? target)
        {
            NCombatRoom.Instance?.PlaySplashVfx(Owner.Creature, new Color("6876bd"));
            await ArmyCmd.Summon(choiceContext, Owner, DynamicVars.Summon.BaseValue, this);
        }
    }