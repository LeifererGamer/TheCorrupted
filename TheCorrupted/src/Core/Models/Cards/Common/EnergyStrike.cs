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

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Common
{
    internal class EnergyStrike() : DoomedCardModel(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [
            HoverTipFactory.FromPower<DoomPower>(),
            EnergyHoverTip
        ];



        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new DoomedVar(5),
            new DamageDiffVar(5m),
            new DamageVar(10m, ValueProp.Move),
            new EnergyVar(1),
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();


        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2m);
            DynamicVars["DamageDiff"].UpgradeValueBy(1m);
        }

        protected override async Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            decimal amount = cardPlay.IsAutoPlay ? DynamicVars["DamageDiff"].BaseValue : DynamicVars.Damage.BaseValue;

            await DamageCmd.Attack(amount).FromCard(this).Targeting(cardPlay.Target)
                 .WithHitFx("vfx/vfx_attack_slash")
                 .Execute(choiceContext);
           
        }

        protected override async Task OnNormalPlayExtra(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            CardPile hand = PileType.Hand.GetPile(Owner);
            if (hand.Cards.Where(card => card.Type == CardType.Curse).ToList().Count > 0)
                await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue + 1, Owner);
        }

        protected override async Task OnAutoPlayExtra(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<EnergyNextTurnPower>(Owner.Creature, DynamicVars.Energy.IntValue, Owner.Creature, this);
        }
    }
}
