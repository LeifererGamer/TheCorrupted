using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon
{

internal class DoubleShield() : CardModel(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self), ICustomModel
    {
        public override bool GainsBlock => true;
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();
        protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CardTag.Defend };

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new BlockVar(10m, ValueProp.Move),
            new RitualVar(1),
        ];


        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            decimal amount = await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
            await Ritual.PerformRitual(choiceContext, cardPlay, Owner, this, async (card) =>
            {
                await PowerCmd.Apply<BlockNextTurnPower>(Owner.Creature, amount, Owner.Creature, this);
            });
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(4m);
        }

    }

}