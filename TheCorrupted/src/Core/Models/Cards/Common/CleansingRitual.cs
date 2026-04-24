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
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Common
{
    internal class CleansingRitual() : CardModel(0, CardType.Skill, CardRarity.Common, TargetType.Self), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [
                HoverTipFactory.FromPower<DoomPower>(),
            ];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new RitualVar(1),
            new CleansingVar(3),
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await Ritual.PerformRitual(choiceContext, cardPlay, Owner, this, async (card) =>
            {
                var amount = DynamicVars["Cleansing"].BaseValue;
                if (card.Owner.Creature.HasPower<DoomPower>())
                {
                    if (card.Owner.Creature.GetPower<DoomPower>().Amount <= DynamicVars["Cleansing"].BaseValue)
                        amount = card.Owner.Creature.GetPower<DoomPower>().Amount;
                    await PowerCmd.Apply<DoomPower>(Owner.Creature, -amount, Owner.Creature, this);
                }
            });

        }

        protected override void OnUpgrade()
        {
            DynamicVars["Cleansing"].UpgradeValueBy(2m);
        }

    }
}