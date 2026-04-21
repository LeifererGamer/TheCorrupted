using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Token;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Common
{
    internal class DelayedSummoning() : CardModel(1, CardType.Skill, CardRarity.Common, TargetType.Self), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromCard<CommandArmy>()];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new ArmyVar(4),
            new EnergyVar(1),
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
            await PowerCmd.Apply<SummonArmyNextTurnPower>(Owner.Creature, DynamicVars["Army"].BaseValue, Owner.Creature, this);
            await PowerCmd.Apply<EnergyNextTurnPower>(Owner.Creature, DynamicVars.Energy.IntValue, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            DynamicVars["Army"].UpgradeValueBy(1m);
            DynamicVars.Energy.UpgradeValueBy(1m);
        }
    }
}
