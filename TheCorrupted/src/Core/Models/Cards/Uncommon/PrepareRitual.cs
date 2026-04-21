using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Curse;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon
{
    internal class PrepareRitual() : CardModel(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [
            CardKeyword.Exhaust,
        ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromCard<CorruptionCorrupted>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            CardPile hand = PileType.Hand.GetPile(Owner);
            List<CardModel> cardModels = (await CardSelectCmd.FromHand(prefs: new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 0, hand.Cards.Count), context: choiceContext, player: Owner, filter: card => card.Type != CardType.Curse, source: this)).ToList();
            foreach (CardModel cardModel in cardModels)
            {
                if (cardModel != null)
                {
                    await CardCmd.Exhaust(choiceContext, cardModel);
                    await CorruptionCorrupted.CreateInHand(Owner, cardPlay.Card.CombatState);
                }
            }
        }

        protected override void OnUpgrade()
        {
            RemoveKeyword(CardKeyword.Exhaust);
        }

    }
}