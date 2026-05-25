using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.Screens.Timeline;
using MegaCrit.Sts2.Core.Timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Relics;

namespace TheCorrupted.TheCorrupted.src.Core.Timeline.Epochs
{
    public class Corrupted6Epoch : EpochModel
    {
        public override string Id => "THECORRUPTED-CORRUPTED6_EPOCH";

        public override EpochEra Era => EpochEra.Flourish3;

        public override int EraPosition => 4;

        public override string StoryId => "Corrupted";

        public static List<RelicModel> Relics
        {
            get
            {
                int num = 3;
                List<RelicModel> list = new List<RelicModel>(num);
                CollectionsMarshal.SetCount(list, num);
                Span<RelicModel> span = CollectionsMarshal.AsSpan(list);
                int num2 = 0;
                span[num2] = ModelDb.Relic<DoomStone>();
                num2++;
                span[num2] = ModelDb.Relic<CharonsAshes>();
                num2++;
                span[num2] = ModelDb.Relic<CursedKey>();
                return list;
            }
        }

        public override string UnlockText => CreateRelicUnlockText(Relics);

        public override void QueueUnlocks()
        {
            NTimelineScreen.Instance.QueueRelicUnlock(Relics);
        }
    }
}
