using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
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
using TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.src.Core.Models.Cards.Curse;
using TheCorrupted.src.Core.Models.Extensions;

namespace TheCorrupted.src.Core.Models.Cards.Common
{
    internal class CorruptedMace() : CardModel(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<DoomPower>()];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new DoomedVar(3),
            new DamageDiffVar(5m),
            new DamageVar(10m, ValueProp.Move),
            new PowerVar<VulnerablePower>(2m)
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            if (cardPlay.IsAutoPlay)
            {
                await DamageCmd.Attack(base.DynamicVars.ElementAt(1).Value.BaseValue).FromCard(this).Targeting(cardPlay.Target) //DamageDiffVar
                 .WithHitFx("vfx/vfx_attack_slash")
                 .Execute(choiceContext);
            }
            else
            {
                await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
                await CorruptionCorrupted.CreateInHand(Owner, CombatState);
            }
            await PowerCmd.Apply<VulnerablePower>(cardPlay.Target, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, this);
        }

        public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
        {
            if (side != base.Owner.Creature.Side)
            {
                return;
            }
            if (this.Pile.Type.Equals(PileType.Hand))
            {
                IEnumerable<Creature> creatures = [base.Owner.Creature];
                await CardCmd.AutoPlay(choiceContext, this, null);
                await PowerCmd.Apply<DoomPower>(creatures, CanonicalVars.First().BaseValue, base.Owner.Creature, this);
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2m);
            DynamicVars.First().Value.UpgradeValueBy(2); //DoomedVar
            DynamicVars.ElementAt(1).Value.UpgradeValueBy(1); //DamageDiffVar
            base.DynamicVars.Vulnerable.UpgradeValueBy(1m);
        }
    }
}