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
using TheCorrupted.src.Core.Models.Extensions;
using TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.src.Core.Models.Cards.Common
{
    internal class SmokeScreen() : CardModel(1, CardType.Skill, CardRarity.Common, TargetType.Self), ICustomModel
    {
        public override bool GainsBlock => true;
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();
        protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CardTag.Defend };

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<WeakPower>()
        ];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new BlockVar(5m, ValueProp.Move),
            new PowerVar<WeakPower>(2m),
            new RitualVar(1),
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
            CardModel cardModel = (await CardSelectCmd.FromHand(prefs: new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1), context: choiceContext, player: base.Owner, filter: null, source: this)).FirstOrDefault();
            if (cardModel != null)
            {

                await CardCmd.Exhaust(choiceContext, cardModel);
                await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
                if (cardModel.Type.Equals(CardType.Curse) || cardModel.Type.Equals(CardType.Status) && base.Owner.Creature.HasPower<StatusQuoPower>())
                {
                    await PowerCmd.Apply<WeakPower>(base.CombatState.HittableEnemies, base.DynamicVars.Weak.BaseValue, base.Owner.Creature, this);
                }

            }



        }

        protected override void OnUpgrade()
        {
            base.DynamicVars.Block.UpgradeValueBy(2m);
            base.DynamicVars["WeakPower"].UpgradeValueBy(1m);
        }

    }

}
