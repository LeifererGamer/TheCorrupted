using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using TheCorrupted.TheCorrupted.src.Core.Models.Commands;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Powers
{
    internal class SummonArmyNextTurnPower : PowerModel, ICustomModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            if (player == Owner.Player && AmountOnTurnStart != 0)
            {
                await ArmyCmd.Summon(choiceContext, Owner.Player, Amount, this);
                await PowerCmd.Remove(this);
            }
        }
    }
}