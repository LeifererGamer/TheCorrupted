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
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.src.Core.Models.Cards.Curse;
using TheCorrupted.src.Core.Models.Extensions;
using TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.src.Core.Models.Cards.Rare
{
    internal class HellfireBarrier() : CardModel(3, CardType.Skill, CardRarity.Rare, TargetType.Self), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new RitualVar(1),
            new PowerVar<IntangiblePower>(1m),
        ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<IntangiblePower>()
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            CardModel cardModel = (await CardSelectCmd.FromHand(prefs: new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1), context: choiceContext, player: base.Owner, filter: null, source: this)).FirstOrDefault();
            if (cardModel != null)
            {
                await CardCmd.Exhaust(choiceContext, cardModel);
                await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
                if (cardModel.Type.Equals(CardType.Curse) || cardModel.Type.Equals(CardType.Status) && base.Owner.Creature.HasPower<StatusQuoPower>())
                {
                    await PowerCmd.Apply<IntangiblePower>(base.Owner.Creature, base.DynamicVars["IntangiblePower"].IntValue, base.Owner.Creature, this);
                }
            }
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars["IntangiblePower"].UpgradeValueBy(1m);
        }

    }

}