using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Curse
{


internal class Necronomicurse() : CardModel(-1, CardType.Curse, CardRarity.Curse, TargetType.None)
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

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
        [
            CardKeyword.Unplayable,
            CardKeyword.Eternal,
        ];

        public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
        {
            if (card == this)
            {
                await CreateInHand(card.Owner, CombatState);
            }
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
                curse.Add(combatState.CreateCard<Necronomicurse>(owner));
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
                curse.Add(combatState.CreateCard<Necronomicurse>(owner));
            }

            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(curse, PileType.Draw, addedByPlayer, CardPilePosition.Random));
            await Cmd.Wait(3f);
            return curse;
        }

        public static async Task<CardModel?> CreateInDeckPile(Player owner, CombatState combatState, bool addedByPlayer = true)
        {
            return (await CreateInDeckPile(owner, 1, combatState)).FirstOrDefault();
        }

        public static async Task<IEnumerable<CardModel>> CreateInDeckPile(Player owner, int count, CombatState combatState, bool addedByPlayer = true)
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
                curse.Add(combatState.CreateCard<Necronomicurse>(owner));
            }

            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(curse, PileType.Deck, addedByPlayer, CardPilePosition.Random));
            await Cmd.Wait(3f);
            return curse;
        }

    }
}