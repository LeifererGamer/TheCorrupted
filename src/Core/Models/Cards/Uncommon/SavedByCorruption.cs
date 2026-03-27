using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.src.Core.Models.Cards.Uncommon
{

internal class SavedByCorruption() : CardModel(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CardTag.Defend };

        protected override IEnumerable<DynamicVar> CanonicalVars => [
           new PowerVar<SavedByCorruptionPower>(3m)
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<SavedByCorruptionPower>(base.Owner.Creature, base.DynamicVars["SavedByCorruptionPower"].IntValue, base.Owner.Creature, this);
        }


        protected override void OnUpgrade()
        {
            base.DynamicVars["SavedByCorruptionPower"].UpgradeValueBy(1m);
        }

    }
}