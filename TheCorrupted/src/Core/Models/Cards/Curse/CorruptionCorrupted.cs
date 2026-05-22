using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Curse
{
    [Pool(typeof(CurseCardPool))]
    public sealed class CorruptionCorrupted() : CustomCardModel(-1, CardType.Curse, CardRarity.Curse, TargetType.None)
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

        public static async Task<CardModel?> CreateInHand(Player owner, ICombatState combatState)
        {
            return (await CreateInHand(owner, 1, combatState)).FirstOrDefault();
        }

        public static async Task<IEnumerable<CardModel>> CreateInHand(Player owner, int count, ICombatState combatState)
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
                curse.Add(combatState.CreateCard<CorruptionCorrupted>(owner));
            }

            await CardPileCmd.AddGeneratedCardsToCombat(curse, PileType.Hand, owner);
            return curse;
        }

        public static async Task<CardModel?> CreateInDrawPile(Player owner, ICombatState combatState, bool addedByPlayer = true)
        {
            return (await CreateInDrawPile(owner, 1, combatState)).FirstOrDefault();
        }

        public static async Task<IEnumerable<CardModel>> CreateInDrawPile(Player owner, int count, ICombatState combatState, bool addedByPlayer = true)
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
                curse.Add(combatState.CreateCard<CorruptionCorrupted>(owner));
            }

            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(curse, PileType.Draw, owner, CardPilePosition.Random));
            //await Cmd.Wait(3f);
            return curse;
        }

        public static async Task<CardModel?> CreateInDeckPile(Player owner, ICombatState combatState, bool addedByPlayer = true)
        {
            return (await CreateInDeckPile(owner, 1, combatState)).FirstOrDefault();
        }

        public static async Task<IEnumerable<CardModel>> CreateInDeckPile(Player owner, int count, ICombatState combatState, bool addedByPlayer = true)
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
                curse.Add(combatState.CreateCard<CorruptionCorrupted>(owner));
            }

            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(curse, PileType.Deck, owner, CardPilePosition.Random));
            await Cmd.Wait(3f);
            return curse;
        }

    }
}


