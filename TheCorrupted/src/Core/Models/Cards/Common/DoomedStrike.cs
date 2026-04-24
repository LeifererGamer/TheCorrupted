using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using BaseLib.Utils;
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

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Common
{
    [Pool(typeof(CorruptedCardPool))]
    internal class DoomedStrike() : DoomedCardModel(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CardTag.Strike };

        protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<DoomPower>()];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new DoomedVar(3),
            new DamageDiffVar(4m),
            new DamageVar(8m, ValueProp.Move),
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2m);
            DynamicVars.First().Value.UpgradeValueBy(2); //DoomedVar
            DynamicVars.ElementAt(1).Value.UpgradeValueBy(1); //DamageDiffVar
        }

        protected override async Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            decimal amount = cardPlay.IsAutoPlay ? DynamicVars["DamageDiff"].BaseValue : DynamicVars.Damage.BaseValue;

            await DamageCmd.Attack(amount).FromCard(this).Targeting(cardPlay.Target) //DamageDiffVar
                 .WithHitFx("vfx/vfx_attack_slash")
                 .Execute(choiceContext);
        }
    }
}
