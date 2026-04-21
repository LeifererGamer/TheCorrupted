using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon
{

internal class Grudge() : CardModel(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        public override bool GainsBlock => true;
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [
            CardKeyword.Exhaust,
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            CardModel rite = CardFactory.GetDistinctForCombat(Owner, ModelDb.CardPool<RitualCardPool>().GetUnlockedCards(Owner.UnlockState, CombatState.RunState.CardMultiplayerConstraint), 1, CombatState.RunState.Rng.CombatCardGeneration).FirstOrDefault();
            if (rite != null)
            {
                rite.EnergyCost.SetThisTurnOrUntilPlayed(0, reduceOnly: true);
                await CardPileCmd.AddGeneratedCardToCombat(rite, PileType.Hand, addedByPlayer: true);
            }
        }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

    }
}