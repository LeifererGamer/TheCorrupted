using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon
{
    internal class CorruptionToAshes() : TheCorruptedCardModel(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self), ICustomModel
    {        
        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new CardsVar(4),
            new RitualVar(1),
        ];

        protected override async Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await Ritual.PerformRitual(choiceContext, Owner, this, async (card) =>
            {
                await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
            });
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Cards.UpgradeValueBy(1);
        }

    }

}