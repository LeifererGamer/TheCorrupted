using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheCorrupted.TheCorrupted.src.Core.Models;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Commands;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon
{

internal class CleansingSummon() : CardModel(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override bool HasEnergyCostX => true;

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new CleansingVar(5),
            new ArmyVar(5),
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override IEnumerable<IHoverTip> ExtraHoverTips => [

        HoverTipFactory.FromPower<DoomPower>(),
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
            
            if (Owner.Creature.HasPower<DoomPower>())
            {
                int xValue = ResolveEnergyXValue();
                for (int i = 0; i < xValue; i++)
                {
                    var amount = await Cleansing.PerformCleansing(DynamicVars["Cleansing"].BaseValue, Owner.Creature, this);
                    if (amount <= 0)
                        return;
                    await ArmyCmd.Summon(choiceContext, Owner, amount, this);
                }
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(2m);
            DynamicVars.Doom.UpgradeValueBy(3m);
            DynamicVars.Damage.UpgradeValueBy(1m);
        }

    }

}