using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace TheCorrupted.src.Core.Models.Cards.Curse
{
    public sealed class CorruptionCorrupted() : CardModel(-1, CardType.Curse, CardRarity.Curse, TargetType.None)
    {
        public override int MaxUpgradeLevel => 0;

        protected override bool ShouldGlowGoldInternal
        {
            get
            {
                if (base.CombatState == null)
                {
                    return false;
                }

                return base.ExhaustOnNextPlay;
            }
        }

        public override CardPoolModel Pool => ModelDb.CardPool<CurseCardPool>();

        public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Unplayable];

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
                curse.Add(combatState.CreateCard<CorruptionCorrupted>(owner));
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
                curse.Add(combatState.CreateCard<CorruptionCorrupted>(owner));
            }

            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(curse, PileType.Draw, addedByPlayer, CardPilePosition.Random));
            await Cmd.Wait(3f);
            return curse;
        }

    }
}


