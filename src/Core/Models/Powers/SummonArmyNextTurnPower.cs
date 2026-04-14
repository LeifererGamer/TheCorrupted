using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.src.Core.Models.Cards.Token;
using TheCorrupted.src.Core.Models.Commands;

namespace TheCorrupted.src.Core.Models.Powers
{
    internal class SummonArmyNextTurnPower : PowerModel, ICustomModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            if (player == base.Owner.Player && base.AmountOnTurnStart != 0)
            {
                await CorruptedArmyCmd.Summon(choiceContext, base.Owner.Player, base.Amount, this);
                await PowerCmd.Remove(this);
            }
        }
    }
}