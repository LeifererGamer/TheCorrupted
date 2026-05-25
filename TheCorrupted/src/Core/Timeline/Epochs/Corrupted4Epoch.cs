using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Potions;
using MegaCrit.Sts2.Core.Nodes.Screens.Timeline;
using MegaCrit.Sts2.Core.Timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Potions;

namespace TheCorrupted.TheCorrupted.src.Core.Timeline.Epochs
{
    public class Corrupted4Epoch : EpochModel
    {
        public override string Id => "THECORRUPTED-CORRUPTED4_EPOCH";

        public override EpochEra Era => EpochEra.Seeds2;

        public override int EraPosition => 4;

        public override string StoryId => "Corrupted";

        public static List<PotionModel> Potions
        {
            get
            {
                int num = 3;
                List<PotionModel> list = new List<PotionModel>(num);
                CollectionsMarshal.SetCount(list, num);
                Span<PotionModel> span = CollectionsMarshal.AsSpan(list);
                int num2 = 0;
                span[num2] = ModelDb.Potion<SoulBrew>();
                num2++;
                span[num2] = ModelDb.Potion<DoomedPotion>();
                num2++;
                span[num2] = ModelDb.Potion<Ashwater>();
                return list;
            }
        }

        public override string UnlockText => CreatePotionUnlockText(Potions);

        public override void QueueUnlocks()
        {
            NTimelineScreen.Instance.QueuePotionUnlock(Potions);
            LocString locString = new LocString("epochs", Id + ".unlock");
            NTimelineScreen.Instance.QueueMiscUnlock(locString.GetFormattedText() ?? "");
        }
    }
}