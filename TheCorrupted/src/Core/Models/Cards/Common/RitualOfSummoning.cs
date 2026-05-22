using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Token;
using TheCorrupted.TheCorrupted.src.Core.Models.Commands;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Common
{
    internal class RitualOfSummoning() : TheCorruptedCardModel(0, CardType.Skill, CardRarity.Common, TargetType.Self), ICustomModel
    {
        protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromCard<CommandArmy>()];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new RitualVar(1),
            new ArmyVar(3),
        ];

        protected override async Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await Ritual.PerformRitual(choiceContext, Owner, this, async (card) =>
            {
                await ArmyCmd.Summon(choiceContext, Owner, DynamicVars["Army"].BaseValue, this);
            });
        }

        protected override void OnUpgrade()
        {
            DynamicVars["Army"].UpgradeValueBy(2m);
        }

    }
}