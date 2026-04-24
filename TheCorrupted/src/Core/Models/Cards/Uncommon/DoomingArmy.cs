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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Commands;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;
using static Godot.OpenXRInterface;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon
{
    internal class DoomingArmy() : DoomedCardModel(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();
        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<DoomPower>(),
            EnergyHoverTip,
        ];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new ArmyVar(8m),
            new DamageDiffVar(4m),
            new DoomedVar(4),
            new EnergyVar(2),
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override async Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            decimal amount = cardPlay.IsAutoPlay ? DynamicVars["DamageDiff"].BaseValue : DynamicVars["Army"].BaseValue;

            await ArmyCmd.Summon(choiceContext, Owner, amount, this);
        }

        protected override async Task OnNormalPlayExtra(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            CardPile hand = PileType.Hand.GetPile(Owner);
            if (hand.Cards.Where((c) => c.Type == CardType.Curse).ToList().Any())
            {
                await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars["Doomed"].UpgradeValueBy(2);
            DynamicVars["Army"].UpgradeValueBy(4m);
            DynamicVars["DamageDiff"].UpgradeValueBy(2m);
        }
    }

}