using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Enchantments;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Rare
{
internal class CorruptedGem() : TheCorruptedCardModel(2, CardType.Skill, CardRarity.Rare, TargetType.Self), ICustomModel
    {
        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromEnchantment<RitualReplay>().First(),
            HoverTipFactory.Static(StaticHoverTip.ReplayStatic),

        ];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new IntVar("Replay", 1m),
            new RitualVar(),
        ];

        protected override async Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
            CardModel cardModel = (await CardSelectCmd.FromHand(prefs: new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1), context: choiceContext, player: base.Owner, filter: delegate (CardModel c)
            {
                CardType type = c.Type;
                return (type != CardType.Curse && type != CardType.Status && !c.DynamicVars.ContainsKey("Ritual") && c.Enchantment == null && type != CardType.Power) ? true : false;
            }, source: this)).FirstOrDefault();
            if (cardModel != null)
            {
                await Ritual.AddRitualEnchantment(cardModel);
                CardCmd.Preview(cardModel);
            }
        }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }
    }
}