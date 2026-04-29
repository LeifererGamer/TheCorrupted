using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Timeline.Epochs;
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
                ModelDb.Relic<DuVuDoll>(),
                ModelDb.Relic<CharonsAshes>(),
                ModelDb.Relic<Armynomicon>(),
                ModelDb.Relic<CursedKey>(),
                ModelDb.Relic<Necronomicon>(),
                ModelDb.Relic<BlueCandle>(),
                ModelDb.Relic<DoomStone>(),
            ];
        }

        public override IEnumerable<RelicModel> GetUnlockedRelics(UnlockState unlockState)
        {
            var list = AllRelics.ToList();
            return list;
        }

       // public override IEnumerable<RelicModel> GetUnlockedRelics(UnlockState unlockState)
       // {
       //     List<RelicModel> list = base.AllRelics.ToList();
       //     if (!unlockState.IsEpochRevealed<Ironclad3Epoch>())
       //     {
       //         list.RemoveAll(delegate (RelicModel r)
       //         {
       //             RelicModel r3 = r;
       //             return Ironclad3Epoch.Relics.Any((RelicModel relic) => relic.Id == r3.Id);
       //         });
       //     }
       //
       //     if (!unlockState.IsEpochRevealed<Ironclad6Epoch>())
       //     {
       //         list.RemoveAll(delegate (RelicModel r)
       //         {
       //             RelicModel r2 = r;
       //             return Ironclad6Epoch.Relics.Any((RelicModel relic) => relic.Id == r2.Id);
       //         });
       //     }
       //
       //     return list;
       // }
    }
}
