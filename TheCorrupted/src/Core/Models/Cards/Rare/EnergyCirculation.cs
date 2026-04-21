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
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Rare
{

internal class EnergyCirculation() : CardModel(5, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<DynamicVar> CanonicalVars => [
        new EnergyVar(1),
        ];

        public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Ethereal,
        ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        EnergyHoverTip,
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<EnergyCirculationPower>(Owner.Creature, DynamicVars.Energy.IntValue, Owner.Creature, this);
        }


        protected override void OnUpgrade()
        {
            RemoveKeyword(CardKeyword.Ethereal);
        }

    }
}

