using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards
{
    internal abstract class TheCorruptedCardModel(int cost, CardType type, CardRarity rarity, TargetType target)
           : CardModel(cost, type, rarity, target)
    {
         protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (Type == CardType.Attack)
                ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");

            await DoOnPlay(choiceContext, cardPlay);

            if (cardPlay.IsAutoPlay && !cardPlay.Card.Owner.HasPower<DoomedEmpowermentPower>())
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

        protected virtual decimal getAmount(CardPlay cardPlay, decimal autoPlayValue, decimal normalValue)
        {
            return cardPlay.IsAutoPlay && !Owner.HasPower<DoomedEmpowermentPower>() ? autoPlayValue : normalValue;
        }
    }
}
