using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Nodes.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.src.Core.Models.Extensions;
using TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.src.Core.Models.Cards.Uncommon
{

internal class Grudge() : CardModel(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        public override bool GainsBlock => true;
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [
            CardKeyword.Exhaust,
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            CardModel rite = CardFactory.GetDistinctForCombat(base.Owner, ModelDb.CardPool<RitualCardPool>().GetUnlockedCards(base.Owner.UnlockState, base.CombatState.RunState.CardMultiplayerConstraint), 1, base.CombatState.RunState.Rng.CombatCardGeneration).FirstOrDefault();
            if (rite != null)
            {
                rite.EnergyCost.SetThisTurnOrUntilPlayed(0, reduceOnly: true);
                await CardPileCmd.AddGeneratedCardToCombat(rite, PileType.Hand, addedByPlayer: true);
            }
        }

        protected override void OnUpgrade()
        {
            base.EnergyCost.UpgradeBy(-1);
        }

    }
}