using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Unlocks;
using TheCorrupted.TheCorrupted.src.Core.Models.Relics;

namespace TheCorrupted.TheCorrupted.src.Core.Models.RelicPools
{
    internal class CorruptedRelicPool : RelicPoolModel
    {
        public override string EnergyColorName => "corrupted";

        public override Color LabOutlineColor => StsColors.purple;


        protected override IEnumerable<RelicModel> GenerateAllRelics()
        {
            return
            [
                ModelDb.Relic<CorruptedBladeRelic>(),
            ];
        }

        public override IEnumerable<RelicModel> GetUnlockedRelics(UnlockState unlockState)
        {
            var list = AllRelics.ToList();
            return list;
        }
    }
}
