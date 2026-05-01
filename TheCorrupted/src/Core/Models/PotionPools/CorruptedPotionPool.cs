using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Unlocks;

namespace TheCorrupted.TheCorrupted.src.Core.Models.PotionPools
{
    internal class CorruptedPotionPool : CustomPotionPoolModel
    {
        public override string EnergyColorName => "corrupted";

        public override Color LabOutlineColor => StsColors.purple;

       // protected override IEnumerable<PotionModel> GenerateAllPotions()
       // {
       //     return AllPotions;
       // }

        public override IEnumerable<PotionModel> GetUnlockedPotions(UnlockState unlockState)
        {
            var progress = SaveManager.Instance?.Progress;
       
            // Custom check bypassing the generic <T> crash
            bool isEpoch4Revealed = progress != null && progress.Epochs.Any(e =>
                e.Id == "CORRUPTED4_EPOCH" &&
                (e.State == EpochState.Revealed || e.State == EpochState.Obtained));
       
            if (!isEpoch4Revealed)
            {
                return Array.Empty<PotionModel>();
            }
       
            return GenerateAllPotions();
        }
    }
}