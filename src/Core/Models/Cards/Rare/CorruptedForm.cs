using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.src.Core.Models.Cards.Rare
{
    internal class CorruptedForm() : CardModel(3, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [
            new PowerVar<StrengthPower>(2m)
            ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => [

        HoverTipFactory.FromPower<StrengthPower>(),

        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
            IEnumerable<CardModel> curses = CardFactory.GetDistinctForCombat(base.Owner, ModelDb.CardPool<CurseCardPool>().GetUnlockedCards(base.Owner.UnlockState, base.CombatState.RunState.CardMultiplayerConstraint), 2, base.CombatState.RunState.Rng.CombatCardGeneration);
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(curses, PileType.Draw, true, CardPilePosition.Random));
            await PowerCmd.Apply<CorruptedFormPower>(base.Owner.Creature, base.DynamicVars["StrengthPower"].BaseValue, base.Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars["StrengthPower"].UpgradeValueBy(1m);
        }
    }
}
