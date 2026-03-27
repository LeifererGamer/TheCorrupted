using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.src.Core.Models.Cards.Rare
{
    internal class ParallelWorld() : CardModel(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [
            CardKeyword.Exhaust,
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            CardPile hand = PileType.Hand.GetPile(base.Owner);
            List<CardModel> cards = hand.Cards.ToList();
            foreach (CardModel card in cards)
            {
                if (card != null)
                {
                    var newCard = card.CreateClone();
                    CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(newCard, PileType.Draw, addedByPlayer: true, CardPilePosition.Random), 2.2f);
                }
            }

        }

        protected override void OnUpgrade()
        {
            base.EnergyCost.UpgradeBy(-1);
        }

    }

}