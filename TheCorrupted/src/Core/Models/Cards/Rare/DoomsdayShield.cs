using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
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
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Rare
{

internal class DoomsdayShield() : DoomedCardModel(1, CardType.Skill, CardRarity.Rare, TargetType.Self), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        public override bool GainsBlock => true;

        decimal cleansingAmount = 0m;

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<DoomPower>(),
        ];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new BlockVar(10m, ValueProp.Move),
            new CleansingVar(10),
            new DoomedVar(10)
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override async Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
        }

        protected override async Task OnNormalPlayExtra(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            cleansingAmount = await Cleansing.PerformCleansing(choiceContext, DynamicVars["Cleansing"].BaseValue, Owner.Creature, this);
            if (cleansingAmount > 0)
                await CreatureCmd.GainBlock(Owner.Creature, new BlockVar(cleansingAmount, ValueProp.Move), cardPlay);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(2m);
            DynamicVars["Doomed"].UpgradeValueBy(2);
            DynamicVars["Cleansing"].UpgradeValueBy(2);
        }
    }
}