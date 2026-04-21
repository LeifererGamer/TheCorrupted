using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Rare
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
            CardPile hand = PileType.Hand.GetPile(Owner);
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
            EnergyCost.UpgradeBy(-1);
        }

    }

}