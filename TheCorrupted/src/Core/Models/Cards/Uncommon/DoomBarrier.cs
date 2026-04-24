using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon
{
    internal class DoomBarrier() : DoomedCardModel(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self), ICustomModel
    {
        public override bool GainsBlock => true;
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();
        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [
            HoverTipFactory.FromPower<DoomPower>(),
        ];
        protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CardTag.Defend };

        protected override IEnumerable<DynamicVar> CanonicalVars => [
             new CalculationBaseVar(0m),
            new CalculationExtraVar(0.5m),
            new CalculatedBlockVar(ValueProp.Move).WithMultiplier(static (card, _) => card.Owner.Creature.HasPower<DoomPower>() ? card.Owner.Creature.GetPower<DoomPower>().Amount: 0),
            new DoomedVar(4),
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.IsAutoPlay)
            {
                await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.CalculatedBlock.Calculate(cardPlay.Target) / 2, DynamicVars.CalculatedBlock.Props, cardPlay);
            }
            else
            {
                await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.CalculatedBlock.Calculate(cardPlay.Target), DynamicVars.CalculatedBlock.Props, cardPlay);
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars["Doomed"].UpgradeValueBy(4);
        }

        protected override async Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            decimal amount = cardPlay.IsAutoPlay ? DynamicVars.CalculatedBlock.Calculate(cardPlay.Target) / 2 : DynamicVars.CalculatedBlock.Calculate(cardPlay.Target);

            await CreatureCmd.GainBlock(Owner.Creature, amount, DynamicVars.CalculatedBlock.Props, cardPlay);
        }
    }

}