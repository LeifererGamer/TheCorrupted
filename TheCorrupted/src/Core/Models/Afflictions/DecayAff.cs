using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Enchantments;
using static BaseLib.Utils.BetaMainCompatibility;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Afflictions
{
    internal class DecayAff : CustomAfflictionModel
    {
        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromEnchantment<Corrupted>().First(),
        ];

        public override async Task OnPlay(PlayerChoiceContext choiceContext, Creature? target)
        {
            await CreatureCmd.TriggerAnim(Card.Owner.Creature, "Cast", Card.Owner.Character.CastAnimDelay);
            CardModel cardModel = (await CardSelectCmd.FromHand(prefs: new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1), context: choiceContext, player: Card.Owner, filter: delegate (CardModel c)
            {
                CardType type = c.Type;
                return (type != CardType.Curse && type != CardType.Status && c.Enchantment == null && type == CardType.Attack) ? true : false;
            }, source: this)).FirstOrDefault();
            if (cardModel != null)
            {
                cardModel.EnchantInternal(ModelDb.Enchantment<Corrupted>().ToMutable(), 1m);
                CardCmd.Preview(cardModel);
            }
        }
    }
}