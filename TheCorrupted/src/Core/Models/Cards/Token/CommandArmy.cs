using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Token
{
    public sealed class CommandArmy() : CardModel(2, CardType.Attack, CardRarity.Token, TargetType.AnyEnemy)
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();


        public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [
            CardKeyword.Retain
        ]; 

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new CalculationBaseVar(0m),
            new ExtraDamageVar(1m).FromOsty(),
            new CalculatedDamageVar(ValueProp.Move).FromOsty().WithMultiplier(delegate(CardModel card, Creature? _)
            {
                Creature osty = card.Owner.Osty;
                return osty != null && osty.IsAlive ? osty.CurrentHp : 0;
            })
        ];


        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            if (!Osty.CheckMissingWithAnim(Owner))
            {
                await DamageCmd.Attack(DynamicVars.CalculatedDamage).FromOsty(Owner.Osty, this).Targeting(cardPlay.Target)
                    .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                    .Execute(choiceContext);
            }
        }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

    }
}
