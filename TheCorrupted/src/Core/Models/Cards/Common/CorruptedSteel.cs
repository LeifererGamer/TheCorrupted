using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Common
{

internal class CorruptedSteel() : TheCorruptedCardModel(1, CardType.Skill, CardRarity.Common, TargetType.Self), ICustomModel
    {
        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<PlatingPower>()
        ];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new PowerVar<PlatingPower>(6m),
            new RitualVar(1),
        ];

        protected override async Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await Ritual.PerformRitual(choiceContext, Owner, this, async (card) =>
            {
                await PowerCmd.Apply<PlatingPower>(choiceContext, Owner.Creature, DynamicVars["PlatingPower"].BaseValue, Owner.Creature, this);
            });
        }


        protected override void OnUpgrade()
        {
            DynamicVars["PlatingPower"].UpgradeValueBy(2m);
        }

    }
}