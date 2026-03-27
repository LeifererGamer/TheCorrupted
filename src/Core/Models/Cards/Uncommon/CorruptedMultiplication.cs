using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
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

namespace TheCorrupted.src.Core.Models.Cards.Uncommon
{
    internal class CorruptedMultiplication() : CardModel(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        public override bool GainsBlock => true;
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();
        protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CardTag.Defend };

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new BlockVar(5m, ValueProp.Move),
        ];

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [
            CardKeyword.Exhaust,
        ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromCard<CorruptionCorrupted>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
            await CorruptionCorrupted.CreateInHand(base.Owner, cardPlay.Card.CombatState);
        }

        public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
        {
            if (card == this)
            {
                var newCard = card.CreateClone();
                CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(newCard, PileType.Hand, addedByPlayer: true));
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(2m);
        }

    }

}
