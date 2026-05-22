using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
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

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon
{
    internal class CorruptedEnergy() : TheCorruptedCardModel(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new EnergyVar(1),
        ];

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [
            CardKeyword.Exhaust,
        ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [
            EnergyHoverTip,
        ];

        protected override async Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
            await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
            CardPile hand = PileType.Hand.GetPile(Owner);
            if (hand.Cards.Where((c) => c.Type == CardType.Curse).ToList().Any())
            {
                await PlayerCmd.GainEnergy(1, Owner);
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Energy.UpgradeValueBy(1m);
        }
    }
}