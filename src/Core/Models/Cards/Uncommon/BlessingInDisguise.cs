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
using TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.src.Core.Models.Cards.Uncommon
{
    internal class BlessingInDisguise() : CardModel(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        public override bool GainsBlock => true;

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new BlockVar(7m, ValueProp.Move),
            new CalculationBaseVar(0m),
            new CalculationExtraVar(7m),
            new CalculatedBlockVar(ValueProp.Move).WithMultiplier(static (CardModel card, Creature? _) => PileType.Hand.GetPile(card.Owner).Cards.Count((CardModel c) => c.Type == CardType.Curse || c.Type ==  CardType.Status && card.Owner.Creature.HasPower<StatusQuoPower>())),
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            CardPile hand = PileType.Hand.GetPile(base.Owner);
            List<CardModel> items = hand.Cards.Where((CardModel c) => c.Type == CardType.Curse || c.Type ==  CardType.Status && base.Owner.Creature.HasPower<StatusQuoPower>()).ToList();
            foreach (CardModel item in items)
            {
                if (item != null)
                {
                    await CardCmd.Exhaust(choiceContext, item);
                    await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
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