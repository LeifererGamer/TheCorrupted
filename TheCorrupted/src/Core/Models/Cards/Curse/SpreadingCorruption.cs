using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Curse
{
[Pool(typeof(CurseCardPool))]
    internal class SpreadingCorruption() : CustomCardModel(-1, CardType.Curse, CardRarity.Curse, TargetType.None)
    {
        public override int MaxUpgradeLevel => 0;

        protected override bool ShouldGlowGoldInternal
        {
            get
            {
                if (CombatState == null)
                {
                    return false;
                }

                return ExhaustOnNextPlay;
            }
        }

        public override CardPoolModel Pool => ModelDb.CardPool<CurseCardPool>();

        public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Unplayable];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePathCurses();

        public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
        {
            CardPile? pile = base.Pile;
            if (pile != null && pile.Type == PileType.Exhaust && player == base.Owner)
            {
                RemoveKeyword(CardKeyword.Unplayable);
                ExhaustOnNextPlay = true;
                await CardCmd.AutoPlay(choiceContext, this, null);
                AddKeyword(CardKeyword.Unplayable);
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreateRandomCurseInDrawPile(base.Owner, base.CombatState);
            
        }

        private static async Task<CardModel?> CreateRandomCurseInDrawPile(Player player, CombatState combatState)
        {
            CardModel cardModel = CardFactory.GetDistinctForCombat(player, from c in ModelDb.CardPool<CurseCardPool>().GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint)
                                                                           where c.Type == CardType.Curse
                                                                           select c, 1, player.RunState.Rng.CombatCardGeneration).FirstOrDefault();
            if (cardModel != null)
            {
                await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, addedByPlayer: true);
            }
            return cardModel;
        }

        public static async Task<CardModel?> CreateInHand(Player owner, CombatState combatState)
        {
            return (await CreateInHand(owner, 1, combatState)).FirstOrDefault();
        }

        public static async Task<IEnumerable<CardModel>> CreateInHand(Player owner, int count, CombatState combatState)
        {
            if (count == 0)
            {
                return Array.Empty<CardModel>();
            }

            if (CombatManager.Instance.IsOverOrEnding)
            {
                return Array.Empty<CardModel>();
            }

            List<CardModel> curse = new List<CardModel>();
            for (int i = 0; i < count; i++)
            {
                curse.Add(combatState.CreateCard<SpreadingCorruption>(owner));
            }

            await CardPileCmd.AddGeneratedCardsToCombat(curse, PileType.Hand, addedByPlayer: true);
            return curse;
        }

        public static async Task<CardModel?> CreateInDrawPile(Player owner, CombatState combatState, bool addedByPlayer = true)
        {
            return (await CreateInDrawPile(owner, 1, combatState)).FirstOrDefault();
        }

        public static async Task<IEnumerable<CardModel>> CreateInDrawPile(Player owner, int count, CombatState combatState, bool addedByPlayer = true)
        {
            if (count == 0)
            {
                return Array.Empty<CardModel>();
            }

            if (CombatManager.Instance.IsOverOrEnding)
            {
                return Array.Empty<CardModel>();
            }

            List<CardModel> curse = new List<CardModel>();
            for (int i = 0; i < count; i++)
            {
                curse.Add(combatState.CreateCard<SpreadingCorruption>(owner));
            }

            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(curse, PileType.Draw, addedByPlayer, CardPilePosition.Random));
            await Cmd.Wait(3f);
            return curse;
        }

    }
}