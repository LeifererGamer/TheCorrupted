using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Curse;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Relics
{


internal class DuVuDoll : RelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Rare;

        public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)
        {
            if (player == Owner && combatState.RoundNumber == 1)
            {
                Flash();
                var amount = player.Piles.Where(p => p.Type == PileType.Deck).SingleOrDefault()?.Cards.Where(c => c.Type == CardType.Curse).Count() ?? 0;
                await PowerCmd.Apply<StrengthPower>(choiceContext, [player.Creature], amount, player.Creature, null);
            }
        }
    }
}
