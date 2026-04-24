using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheCorrupted.TheCorrupted.src.Core.Models.Commands;
using TheCorrupted.TheCorrupted.src.Core.Models.Enchantments;
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.TheCorrupted.src.Core.Models
{
    public static class Ritual
    {
        internal static async Task AddRitualEnchantment(CardModel cardModel)
        {
            cardModel.EnchantInternal(ModelDb.Enchantment<RitualReplay>().ToMutable(), 1m);
        }

        internal static async Task PerformRitual(PlayerChoiceContext choiceContext, CardPlay cardPlay, Player player, AbstractModel source, Func<CardModel, Task> getBenefits)
        {
            CardModel cardModel = (await CardSelectCmd.FromHand(
                prefs: new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1),
                context: choiceContext,
                player: player,
                filter: null,
                source: source)).FirstOrDefault();

            if (cardModel != null)
            {
                await CardCmd.Exhaust(choiceContext, cardModel);
                await CreatureCmd.TriggerAnim(player.Creature, "Cast", player.Character.CastAnimDelay);

                if (cardModel.Type.Equals(CardType.Curse) || cardModel.Type.Equals(CardType.Status) && player.Creature.HasPower<StatusQuoPower>())
                {
                    await getBenefits(cardModel);
                    if (player.Creature.HasPower<DoomingCorruptionPower>())
                    {
                        PowerCmd.Apply<DoomPower>([player.Creature], player.Creature.GetPower<DoomingCorruptionPower>().Amount, player.Creature, cardModel);
                    }
                    if (player.Creature.HasPower<RitualisticSummonsPower>())
                    {
                        ArmyCmd.Summon(choiceContext, player, player.Creature.GetPower<RitualisticSummonsPower>().Amount, cardModel);
                    }
                    if (player.Creature.HasPower<CleanseWithRitualsPower>())
                    {
                        Cleansing.PerformCleansing(player.Creature.GetPower<CleanseWithRitualsPower>().Amount, player.Creature, cardModel);
                    }
                }
            }
        }
    }
}
