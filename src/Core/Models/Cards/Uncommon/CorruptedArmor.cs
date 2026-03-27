using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
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
using TheCorrupted.src.Core.Models.Extensions;

namespace TheCorrupted.src.Core.Models.Cards.Uncommon
{


internal class CorruptedArmor() : CardModel(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self), ICustomModel
    {
        public override bool GainsBlock => true;
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();
        protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CardTag.Defend };

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [
            HoverTipFactory.FromCard<CorruptionCorrupted>(),
            HoverTipFactory.FromPower<FrailPower>()
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new CorruptedVar(2),
            new DamageDiffVar(10m),
            new BlockVar(20m, ValueProp.Move),
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.IsAutoPlay)
            {
                await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars["DamageDiff"].BaseValue, ValueProp.Move, cardPlay);
            }
            else
            {
                await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
                IEnumerable<CardModel> curses = CardFactory.GetDistinctForCombat(base.Owner, ModelDb.CardPool<CurseCardPool>().GetUnlockedCards(base.Owner.UnlockState, base.CombatState.RunState.CardMultiplayerConstraint), 2, base.CombatState.RunState.Rng.CombatCardGeneration);
                CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(curses, PileType.Draw, true, CardPilePosition.Random));
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
                await PowerCmd.Apply<FrailPower>(base.Owner.Creature, base.DynamicVars["Corrupted"].BaseValue, base.Owner.Creature, this);
                IEnumerable<CardModel> curses = CardFactory.GetDistinctForCombat(base.Owner, ModelDb.CardPool<CurseCardPool>().GetUnlockedCards(base.Owner.UnlockState, base.CombatState.RunState.CardMultiplayerConstraint), base.DynamicVars["Corrupted"].IntValue, base.CombatState.RunState.Rng.CombatCardGeneration);
                CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(curses, PileType.Draw, true, CardPilePosition.Random));
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(5m);
            DynamicVars["DamageDiff"].UpgradeValueBy(4m);
        }
    }
}