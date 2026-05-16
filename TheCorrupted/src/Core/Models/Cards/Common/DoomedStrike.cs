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
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Common
{
    [Pool(typeof(CorruptedCardPool))]
    internal class DoomedStrike() : DoomedCardModel(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CardTag.Strike };

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [
            HoverTipFactory.FromPower<DoomPower>(),
            HoverTipFactory.FromPower<WeakPower>(),
        ];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new DoomedVar(3),
            new DamageVar ("DamageDiff", 4m, ValueProp.Move),
            new DamageVar(8m, ValueProp.Move),
            new PowerVar<WeakPower>(1m)
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2m);
            DynamicVars["Doomed"].UpgradeValueBy(2);
            DynamicVars["DamageDiff"].UpgradeValueBy(1);
            DynamicVars.Weak.UpgradeValueBy(1m);
        }

        protected override async Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            decimal amount = getAmount(cardPlay, DynamicVars["DamageDiff"].BaseValue, DynamicVars.Damage.BaseValue);

            await DamageCmd.Attack(amount).FromCard(this).Targeting(cardPlay.Target) //DamageDiffVar
                 .WithHitFx("vfx/vfx_attack_slash")
                 .Execute(choiceContext);
        }

        protected override async Task OnAutoPlayExtra(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<WeakPower>(choiceContext, cardPlay.Target, DynamicVars.Weak.BaseValue, Owner.Creature, this);
        }
    }
}
