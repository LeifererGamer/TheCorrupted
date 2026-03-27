using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.src.Core.Models.Cards.Curse;
using TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.src.Core.Models.Cards.Rare
{
    internal class DemonSword() : CardModel(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [
                new PowerVar<StrengthPower>(1m),
                new DamageVar(11m, ValueProp.Move),
            ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [
                HoverTipFactory.FromPower<StrengthPower>(),
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_flying_slash")
                .Execute(choiceContext);
            CardPile hand = PileType.Hand.GetPile(base.Owner);
            List<CardModel> items = hand.Cards.Where((CardModel c) => c.Type == CardType.Curse).ToList();
            foreach (CardModel item in items)
            {
                if (item != null)
                {
                    await CardCmd.Exhaust(choiceContext, item);
                    await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, DynamicVars["StrengthPower"].BaseValue, base.Owner.Creature, this);
                }
            }
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars.Damage.UpgradeValueBy(4m);
        }
    }
}
