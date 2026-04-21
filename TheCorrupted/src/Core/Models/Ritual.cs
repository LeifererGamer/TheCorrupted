using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.TheCorrupted.src.Core.Models
{
    public static class Ritual
    {
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
                }
            }
        }
    }
}
