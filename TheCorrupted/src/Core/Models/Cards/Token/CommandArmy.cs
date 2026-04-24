using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Token
{
    public sealed class CommandArmy() : CardModel(2, CardType.Attack, CardRarity.Token, TargetType.AnyEnemy)
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        private bool HasTrample
        {
            get
            {
                if (base.IsMutable && base.Owner != null)
                {
                    return base.Owner.Creature.HasPower<TramplePower>();
                }

                return false;
            }
        }
        public override TargetType TargetType
        {
            get
            {
                if (!HasTrample)
                {
                    return TargetType.AnyEnemy;
                }

                return TargetType.AllEnemies;
            }
        }

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
    if (!Osty.CheckMissingWithAnim(Owner))
    {
        if (HasTrample)
        {
            await DamageCmd.Attack(DynamicVars.CalculatedDamage)
                .FromOsty(Owner.Osty, this)
                .TargetingAllOpponents(base.CombatState) 
                .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                .Execute(choiceContext);
        }
        else
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            
            await DamageCmd.Attack(DynamicVars.CalculatedDamage)
                .FromOsty(Owner.Osty, this)
                .Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                .Execute(choiceContext);
        }
    }
}

        public static NSovereignBladeVfx? GetVfxNode(Player player, CardModel card)
        {
            CardModel originalCard = card.DupeOf ?? card;
            return (NCombatRoom.Instance?.GetCreatureNode(player.Creature))?.GetChildren().OfType<NSovereignBladeVfx>().FirstOrDefault((NSovereignBladeVfx b) => b.Card == originalCard);
        }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

    }
}
