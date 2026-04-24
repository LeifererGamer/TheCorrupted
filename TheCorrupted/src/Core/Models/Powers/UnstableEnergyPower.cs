using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Commands;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Powers
{
internal class UnstableEnergyPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new EnergyVar(1),
            new PowerVar<DoomPower>(10),
        ];

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            if (player.Creature.HasPower<DoomPower>())
            {
                Flash();
                int amount = (player.Creature.GetPower<DoomPower>().Amount / (int) DynamicVars["DoomPower"].BaseValue) * Amount;
                await PlayerCmd.GainEnergy(amount, player);
            }
        }
    }
}