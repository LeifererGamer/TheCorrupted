using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.src.Core.Models.CardPools;

namespace TheCorrupted.src.Core.Models.Cards.Common
{
    internal class CloakOfCorruption() : CardModel(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
        public override bool GainsBlock => true;
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();
        protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CardTag.Defend };

        protected override IEnumerable<DynamicVar> CanonicalVars => 
        [
            new BlockVar(8m, ValueProp.Move),
            new EnergyVar(1),
        ];


        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
            CardPile hand = PileType.Hand.GetPile(base.Owner);
            if (hand.Cards.Where((CardModel c) => c.Type == CardType.Curse).ToList().Any())
            {
                await PlayerCmd.GainEnergy(1, Owner);
            }
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars.Block.UpgradeValueBy(3m);
        }
    }
}