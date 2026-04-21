using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
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
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon
{
    internal class BlessingInDisguise() : CardModel(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        public override bool GainsBlock => true;

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new BlockVar(7m, ValueProp.Move),
            new CalculationBaseVar(0m),
            new CalculationExtraVar(7m),
            new CalculatedBlockVar(ValueProp.Move).WithMultiplier(static (card, _) => PileType.Hand.GetPile(card.Owner).Cards.Count((c) => c.Type == CardType.Curse || c.Type ==  CardType.Status && card.Owner.Creature.HasPower<StatusQuoPower>())),
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            CardPile hand = PileType.Hand.GetPile(Owner);
            List<CardModel> items = hand.Cards.Where((c) => c.Type == CardType.Curse || c.Type ==  CardType.Status && Owner.Creature.HasPower<StatusQuoPower>()).ToList();
            foreach (CardModel item in items)
            {
                if (item != null)
                {
                    await CardCmd.Exhaust(choiceContext, item);
                    await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
                }
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(1m);
            DynamicVars.CalculationExtra.UpgradeValueBy(1m);
        }

    }

}