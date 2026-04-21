using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Unlocks;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Ancient;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Basic;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Common;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Rare;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon;

namespace TheCorrupted.TheCorrupted.src.Core.Models.CardPools
{
    public sealed class CorruptedCardPool : CardPoolModel, ICustomModel
    {
        public override string Title => "corrupted";

        public override string EnergyColorName => "corrupted";

        public override string CardFrameMaterialPath => "card_frame_purple";

        public override Color DeckEntryCardColor => new("b778f3");

        public override Color EnergyOutlineColor => new("7d00f3");

        public override bool IsColorless => false;

        protected override CardModel[] GenerateAllCards()
        {
            return
            [
                ModelDb.Card<DefendCorrupted>(),
                ModelDb.Card<StrikeCorrupted>(),
                ModelDb.Card<CorruptingStrike>(),
                ModelDb.Card<DefensiveRitual>(),
                ModelDb.Card<DoomsdayStrike>(),
                ModelDb.Card<DoomsdayDoor>(),
                ModelDb.Card<DoomingStrength>(),
                ModelDb.Card<CorruptedStrike>(),
                ModelDb.Card<DoomedStrike>(),
                ModelDb.Card<StatusQuo>(),
                ModelDb.Card<HellsGateOpen>(),
                ModelDb.Card<HellfireBreathing>(),
                ModelDb.Card<EnergyCirculation>(),
                ModelDb.Card<SavedByCorruption>(),
                ModelDb.Card<CarefulPlanning>(),
                ModelDb.Card<CorruptedForm>(),
                ModelDb.Card<HellfireShockwave>(),
                ModelDb.Card<CorruptedMace>(),
                ModelDb.Card<HellfirePact>(),
                ModelDb.Card<CorruptedBlade>(),
                ModelDb.Card<DemonSword>(),
                ModelDb.Card<CorruptedBullets>(),
                ModelDb.Card<ShareTheWeight>(),
                ModelDb.Card<SoulReap>(),
                ModelDb.Card<EnergyStrike>(),
                ModelDb.Card<SurpriseAttack>(),
                ModelDb.Card<CounterBalancingStrike>(),
                ModelDb.Card<CleansingBlock>(),
                ModelDb.Card<CorruptionToAshes>(),
                ModelDb.Card<BlessingInDisguise>(),
                ModelDb.Card<DoomBarrier>(),
                ModelDb.Card<CorruptedAmulet>(),
                ModelDb.Card<CorruptedArmor>(),
                ModelDb.Card<HellfireBarrier>(),
                ModelDb.Card<DoubleShield>(),
                ModelDb.Card<CorruptedSteel>(),
                ModelDb.Card<ForbiddenAlchemy>(),
                ModelDb.Card<NeowsMight>(),
                ModelDb.Card<DrainStrength>(),
                ModelDb.Card<Grudge>(),
                ModelDb.Card<RitualOfGreaterDefence>(),
                ModelDb.Card<ParallelWorld>(),
                ModelDb.Card<PrepareRitual>(),
                ModelDb.Card<HellfireCardWall>(),
                ModelDb.Card<RitualOfStrength>(),
                ModelDb.Card<SmokeScreen>(),
                ModelDb.Card<CorruptedEnergy>(),
                ModelDb.Card<CorruptedMultiplication>(),
                ModelDb.Card<CloakOfCorruption>(),
                ModelDb.Card<SummonArmy>(),
                ModelDb.Card<RitualOfSummoning>(),
                ModelDb.Card<DoomedArmy>(),
                ModelDb.Card<CleansingRitual>(),
                ModelDb.Card<DelayedSummoning>(),
                ModelDb.Card<DoomingStrike>(),
                ModelDb.Card<SoulSplitter>(),
            ];
        }

        protected override IEnumerable<CardModel> FilterThroughEpochs(UnlockState unlockState, IEnumerable<CardModel> cards)
        {
            return cards.ToList();
        }
    }

}
