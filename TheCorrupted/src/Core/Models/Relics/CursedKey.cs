using BaseLib.Cards.Variables;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Curse;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Relics
{
internal class CursedKey : RelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Rare;

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [
            HoverTipFactory.ForEnergy(this),
            HoverTipFactory.FromCard<CorruptionCorrupted>(),
        ];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
        new EnergyVar(1)
        ];

        public override decimal ModifyMaxEnergy(Player player, decimal amount)
        {
            if (player != base.Owner)
            {
                return amount;
            }

            return amount + (decimal)base.DynamicVars.Energy.IntValue;
        }

        public override async Task AfterCombatVictory(CombatRoom room)
        {
            if (room.RoomType != RoomType.Elite)
            {
                return;
            }

            Flash();
            await CardPileCmd.AddCurseToDeck<CorruptionCorrupted>(Owner);
        }
    }
}
