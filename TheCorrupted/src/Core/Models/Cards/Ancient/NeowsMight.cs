using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Ancient
{

internal class NeowsMight() : CardModel(1, CardType.Skill, CardRarity.Ancient, TargetType.Self)
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new CardsVar(2),
        ];

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [
            CardKeyword.Exhaust,
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
            foreach (CardModel card in PileType.Hand.GetPile(Owner).Cards)
            {
                card.EnergyCost.SetThisTurnOrUntilPlayed(card.EnergyCost.Canonical - 1, reduceOnly: true);
            }
        }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

    }

}