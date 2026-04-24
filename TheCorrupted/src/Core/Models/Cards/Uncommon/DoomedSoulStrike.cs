using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
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

        bool isAutoplayed = false;
        bool secondAttack = true;
        decimal cleansingAmount = 0m;

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new DamageVar(6m, ValueProp.Move),
            new DamageDiffVar(3m),
            new CleansingVar(5),
            new DoomedVar(5)
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props, Creature target, CardModel? cardSource)
        {
            if (dealer == Owner.Creature && result.UnblockedDamage > 0 && !isAutoplayed)
            {
                cleansingAmount = result.UnblockedDamage;
            }
        }

        public override async Task BeforeCardAutoPlayed(CardModel card, Creature? target, AutoPlayType type)
        {
            isAutoplayed = true;
        }

        protected override async Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            decimal amount = cardPlay.IsAutoPlay ? DynamicVars["DamageDiff"].IntValue  : DynamicVars.Damage.IntValue;

            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(amount).FromCard(this).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                .Execute(choiceContext);
        }

        protected override async Task OnNormalPlayExtra(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            cleansingAmount = await Cleansing.PerformCleansing(cleansingAmount, Owner.Creature, this);
            if (cleansingAmount > 0)
            {
                await DamageCmd.Attack(cleansingAmount).FromCard(this).Targeting(cardPlay.Target)
               .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
               .Execute(choiceContext);
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2m);
            DynamicVars["Doomed"].UpgradeValueBy(1);
            DynamicVars["Cleansing"].UpgradeValueBy(1);
        }
    }
}