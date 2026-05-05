using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
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
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Curse;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Token;
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Ancient
{

internal class NeowsDoomingCorruption() : CardModel(1, CardType.Power, CardRarity.Ancient, TargetType.Self), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<DoomPower>(),
            HoverTipFactory.FromPower<NeowsCorruptionPower>(),
            HoverTipFactory.FromPower<NeowsDoomingPower>(),
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<DoomPower>([Owner.Creature], DynamicVars.Doom.BaseValue, Owner.Creature, this);
            await SpreadingCorruption.CreateInHand(Owner, DynamicVars["Ritual"].IntValue, CombatState);
            var ritualPerformed = await Ritual.ChooseIfPerformRitual(choiceContext, Owner, this, async (card) =>
            {
                await PowerCmd.Apply<NeowsCorruptionPower>([Owner.Creature], 1m, Owner.Creature, this);
            });
            if (!ritualPerformed)
            {
                await PowerCmd.Apply<NeowsDoomingPower>([Owner.Creature], 1m, Owner.Creature, this);
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.ExtraDamage.UpgradeValueBy(1m);
            DynamicVars.CalculationExtra.UpgradeValueBy(1m);
            DynamicVars.Doom.UpgradeValueBy(5m);
            DynamicVars["Ritual"].UpgradeValueBy(1m);
        }

    }

}