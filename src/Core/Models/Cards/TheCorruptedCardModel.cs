using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.src.Core.Models.Cards
{
    internal abstract class TheCorruptedCardModel(int cost, CardType type, CardRarity rarity, TargetType target)
           : CardModel(cost, type, rarity, target)
    {
         protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (this.Type == CardType.Attack)
                ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");

            await DoOnPlay(choiceContext, cardPlay);

            if (cardPlay.IsAutoPlay)
            {
                await OnAutoPlayExtra(choiceContext, cardPlay);
            }
            else
            {
                await OnNormalPlayExtra(choiceContext, cardPlay);
            }
        }

        protected abstract Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay);

        protected virtual Task OnNormalPlayExtra(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnAutoPlayExtra(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            return Task.CompletedTask;
        }
    }
}
