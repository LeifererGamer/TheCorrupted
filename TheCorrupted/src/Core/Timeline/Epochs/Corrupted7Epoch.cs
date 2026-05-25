using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.Screens.Timeline;
using MegaCrit.Sts2.Core.Timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Common;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Rare;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon;

namespace TheCorrupted.TheCorrupted.src.Core.Timeline.Epochs
{
    public class Corrupted7Epoch : EpochModel
    {
        public override string Id => "THECORRUPTED-CORRUPTED7_EPOCH";

        public override EpochEra Era => EpochEra.Invitation5;

        public override int EraPosition => 4;

        public override string StoryId => "Corrupted";

        public static List<CardModel> Cards
        {
            get
            {
                int num = 3;
                List<CardModel> list = new List<CardModel>(num);
                CollectionsMarshal.SetCount(list, num);
                Span<CardModel> span = CollectionsMarshal.AsSpan(list);
                int num2 = 0;
                span[num2] = ModelDb.Card<DoomingStrike>();
                num2++;
                span[num2] = ModelDb.Card<YourDaysAreDoomed>();
                num2++;
                span[num2] = ModelDb.Card<DoomBarrier>();
                return list;
            }
        }

        public override string UnlockText => CreateCardUnlockText(Cards);

        public override void QueueUnlocks()
        {
            NTimelineScreen.Instance.QueueCardUnlock(Cards);
        }
    }
}
