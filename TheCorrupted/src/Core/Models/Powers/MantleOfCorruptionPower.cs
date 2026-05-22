using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Rare;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Token;
using TheCorrupted.TheCorrupted.src.Core.Models.Commands;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Powers
{

internal class MantleOfCorruptionPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<DoomPower>(),
            HoverTipFactory.Static(StaticHoverTip.Block),
           ..HoverTipFactory.FromCardWithCardHoverTips<MantleOfCorruption>(),
        ];

        private const string _doomOrCleansingKey = "DoomOrCleansing";

        protected override IEnumerable<DynamicVar> CanonicalVars => [
             new DynamicVar("Army",0),
            new DynamicVar(_doomOrCleansingKey,0)
        ];

        public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            if (player == base.Owner.Player)
            {
                Flash();
                await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null);
                await ArmyCmd.Summon(choiceContext, Owner.Player, DynamicVars["Army"].BaseValue, this);
                var ritualPerformed = await Ritual.PerformRitual(choiceContext, Owner.Player, this, async (card) =>
                {
                    await Cleansing.PerformCleansing(DynamicVars[_doomOrCleansingKey].BaseValue, Owner, null);
                }, true);
                if(!ritualPerformed)
                {
                    await PowerCmd.Apply<DoomPower>([Owner], DynamicVars[_doomOrCleansingKey].BaseValue, Owner, null);
                }
            }
        }

        public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
        {
            if ( (power == this))
                await UpdateDoomOrCleansing(); 
        }

        private async Task UpdateDoomOrCleansing()
        {
            AssertMutable();
            DynamicVars["Army"].BaseValue = Amount / 2;
            DynamicVars[_doomOrCleansingKey].BaseValue = (int)(Amount / 5) * 5;
            await Task.CompletedTask;
        }
    }
}