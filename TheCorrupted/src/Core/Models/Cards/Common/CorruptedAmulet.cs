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
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Curse;
using TheCorrupted.TheCorrupted.src.Core.Models.Relics;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Common
{
    internal class CorruptedAmulet() : CardModel(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
        public override bool GainsBlock => true;
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromCard<CorruptionCorrupted>()];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new BlockVar(8m, ValueProp.Move),
            new CardsVar(1),
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            IEnumerable<CardModel> curses = CorruptedCardPool.GetRandomCurses(Owner, 1);
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(curses, PileType.Draw, true, CardPilePosition.Random));
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(3m);
        }
    }
}