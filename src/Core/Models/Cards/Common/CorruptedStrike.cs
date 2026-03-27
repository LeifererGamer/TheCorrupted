using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Cards;
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
    internal class CorruptedStrike() : CardModel(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [
            HoverTipFactory.FromCard<CorruptionCorrupted>(),
            HoverTipFactory.FromPower<WeakPower>(),
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new CorruptedVar(1),
            new DamageDiffVar(4m),
            new DamageVar(8m, ValueProp.Move),
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            if (cardPlay.IsAutoPlay)
            {
                await DamageCmd.Attack(base.DynamicVars["DamageDiff"].BaseValue).FromCard(this).Targeting(cardPlay.Target)
                 .WithHitFx("vfx/vfx_attack_slash")
                 .Execute(choiceContext);
            }
            else
            {
                await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
                    .WithHitFx("vfx/vfx_attack_slash")
                    .Execute(choiceContext);
            }
        }

        public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
        {
            if (side != base.Owner.Creature.Side)
            {
                return;
            }
            if (this.Pile.Type.Equals(PileType.Hand))
            {
                await CardCmd.AutoPlay(choiceContext, this, null);
                await PowerCmd.Apply<WeakPower>(base.Owner.Creature, base.DynamicVars["Corrupted"].BaseValue, base.Owner.Creature, this);
                IEnumerable<CardModel> curses = CardFactory.GetDistinctForCombat(base.Owner, ModelDb.CardPool<CurseCardPool>().GetUnlockedCards(base.Owner.UnlockState, base.CombatState.RunState.CardMultiplayerConstraint), base.DynamicVars["Corrupted"].IntValue, base.CombatState.RunState.Rng.CombatCardGeneration);
                CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(curses, PileType.Draw, true, CardPilePosition.Random));
            }
        }



        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2m);
            DynamicVars.First().Value.UpgradeValueBy(1); //CursedVar
            DynamicVars.ElementAt(1).Value.UpgradeValueBy(1); //DamageDiffVar
        }
    }

}
