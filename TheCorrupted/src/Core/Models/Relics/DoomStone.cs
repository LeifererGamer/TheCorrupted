using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Curse;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Relics
{
internal class DoomStone : RelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Uncommon;

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<DoomPower>(),
        ];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<StrengthPower>(1)
        ];

        public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
        {
            if (Owner.Creature != power.Owner || applier != Owner.Creature || power is not DoomPower)
            {
                return;
            }
            if (amount <= 0)
            {
                return;
            }
            Flash();
            await PowerCmd.Apply<StrengthPower>([Owner.Creature], DynamicVars["StrengthPower"].BaseValue, Owner.Creature, cardSource);
        }
    }
}