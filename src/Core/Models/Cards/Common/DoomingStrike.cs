using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
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

namespace TheCorrupted.src.Core.Models.Cards.Common
{
    internal class DoomingStrike() : CardModel(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CardTag.Strike };

        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(8m, ValueProp.Move)];


        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            AttackCommand attackCommand = await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
            await PowerCmd.Apply<DoomPower>(base.Owner.Creature, attackCommand.Results.Sum((DamageResult r) => (r.TotalDamage / 2)), base.Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars.Damage.UpgradeValueBy(2m);
        }
    }
}
