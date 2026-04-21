using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Powers
{

internal class EnergyCirculationPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.ForEnergy(this),
        ];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
        new EnergyVar(1),
        ];

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
        {
            if (card.Type.Equals(CardType.Curse) || card.Type.Equals(CardType.Status) && Owner.HasPower<StatusQuoPower>())
            {
                Flash();
                await PlayerCmd.GainEnergy(Amount, Owner.Player);
            }
        }
    }
}