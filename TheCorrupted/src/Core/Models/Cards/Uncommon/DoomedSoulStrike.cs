using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Commands;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;
using static Godot.HttpRequest;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon
{

internal class DoomedSoulStrike() : DoomedCardModel(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        decimal cleansingAmount = 0m;

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<DoomPower>(),
            HoverTipFactory.FromPower<WeakPower>(),
        ];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new DamageVar(6m, ValueProp.Move),
            new DamageVar("DamageDiff", 3m , ValueProp.Move),
            new CleansingVar(5),
            new DoomedVar(5),
            new PowerVar<WeakPower>(1),
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override async Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            decimal amount = getAmount(cardPlay, DynamicVars["DamageDiff"].BaseValue, DynamicVars.Damage.IntValue); 

            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(amount).FromCard(this).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                .Execute(choiceContext);
        }

        protected override async Task OnNormalPlayExtra(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            cleansingAmount = await Cleansing.PerformCleansing(DynamicVars["Cleansing"].BaseValue, Owner.Creature, this);
            if (cleansingAmount > 0)
            {
                DamageVar damage = new DamageVar("CleansingAmount", cleansingAmount, ValueProp.Move);
                ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
                await DamageCmd.Attack(cleansingAmount).FromCard(this).Targeting(cardPlay.Target)
               .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
               .Execute(choiceContext);
            }
        }

        protected override async Task OnAutoPlayExtra(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<WeakPower>(cardPlay.Target, DynamicVars.Weak.BaseValue, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2m);
            DynamicVars["DamageDiff"].UpgradeValueBy(1);
            DynamicVars["Doomed"].UpgradeValueBy(1);
            DynamicVars["Cleansing"].UpgradeValueBy(1);
            DynamicVars.Weak.UpgradeValueBy(1);
        }
    }
}