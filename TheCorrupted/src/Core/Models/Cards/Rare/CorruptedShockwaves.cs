using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Badges;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Curse;
using TheCorrupted.TheCorrupted.src.Core.Models.Enchantments;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Rare
{
    internal class CorruptedShockwaves() : CardModel(2, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override bool IsPlayable => CardPile.GetCards(base.Owner, PileType.Exhaust).Where(c => c.Type == CardType.Curse || (c.Type == CardType.Status && base.Owner.Creature.HasPower<StatusQuoPower>())).Count() >= base.DynamicVars.Cards.IntValue;

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new RitualVar(),
            new CardsVar(3),
            new CardsVar("AutoCards", 8),
            new DamageVar(20m, ValueProp.Move),
            new DamageVar("DamageDiff", 8m , ValueProp.Move),
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();


        public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            if (player == Owner)
            {
                if (Pile != null && Pile.Type == PileType.Discard && player == base.Owner && CardPile.GetCards(base.Owner, PileType.Exhaust).Where(c => c.Type == CardType.Curse || (c.Type == CardType.Status && base.Owner.Creature.HasPower<StatusQuoPower>())).Count() >= base.DynamicVars["AutoCards"].IntValue)
                {
                    //await CardCmd.AutoPlay(choiceContext, this, null);
                    CardCmd.Preview(this);
                    await DamageCmd.Attack(DynamicVars["DamageDiff"].BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
                    .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                    .Execute(choiceContext);
                    await CorruptionCorrupted.CreateInDrawPile(base.Owner, base.CombatState);
                }
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(4m);
            DynamicVars["DamageDiff"].UpgradeValueBy(2m);
            DynamicVars["AutoCards"].UpgradeValueBy(-2);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await Ritual.PerformRitual(choiceContext, Owner, this, async (card) =>
            {
                await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(base.CombatState)
                .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                .Execute(choiceContext);
                await CorruptionCorrupted.CreateInHand(base.Owner, base.CombatState);
            });
        }
    }
}