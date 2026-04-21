using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon
{
    internal class CorruptedBullets() : CardModel(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();


        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new CalculationBaseVar(0m),
            new ExtraDamageVar(7m),
            new CalculatedDamageVar(ValueProp.Move).WithMultiplier(static (card, _) => PileType.Hand.GetPile(card.Owner).Cards.Count((c) => c.Type.Equals(CardType.Curse))),
            new DamageVar(7m, ValueProp.Move),
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            foreach (CardModel c in PileType.Hand.GetPile(Owner).Cards.Where((c) => c.Type.Equals(CardType.Curse)).ToList())
                await DamageCmd.Attack(DynamicVars.Damage.IntValue).FromCard(this).Targeting(cardPlay.Target)
                    .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                    .Execute(choiceContext);
        }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
            DynamicVars.Damage.UpgradeValueBy(1m);
            DynamicVars.ExtraDamage.UpgradeValueBy(1m);
        }
    }
}