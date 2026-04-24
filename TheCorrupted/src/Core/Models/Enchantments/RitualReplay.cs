using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Enchantments
{
    public class RitualReplay : EnchantmentModel
    {
        public override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            // Call your static PerformRitual method
            await Ritual.PerformRitual(choiceContext, cardPlay, cardPlay.Card.Owner, this, async (card) =>
            {
                cardPlay.Card.BaseReplayCount += 1;
            });
        }
    }
}