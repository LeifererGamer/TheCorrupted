using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Nodes.Screens.Timeline;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Timeline;
using MegaCrit.Sts2.Core.Timeline.Epochs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Common;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Rare;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon;
using TheCorrupted.TheCorrupted.src.Core.Models.Characters;

namespace TheCorrupted.TheCorrupted.src.Core.Timeline.Epochs
{
    public class Corrupted2Epoch : EpochModel
    {
        public override string Id => "CORRUPTED2_EPOCH";

        public override EpochEra Era => EpochEra.Blight1;

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
                span[num2] = ModelDb.Card<CorruptedSteel>();
                num2++;
                span[num2] = ModelDb.Card<RitualOfStrength>();
                num2++;
                span[num2] = ModelDb.Card<EnergyCirculation>();
                return list;
            }
        }

      //  public override EpochModel[] GetTimelineExpansion()
      //  {
      //      return new EpochModel[5]
      //      {
      //  // Use the raw strings here as well!
      //  EpochModel.Get("CORRUPTED3_EPOCH"),
      //  EpochModel.Get("CORRUPTED4_EPOCH"),
      //  EpochModel.Get("CORRUPTED5_EPOCH"),
      //  EpochModel.Get("CORRUPTED6_EPOCH"),
      //  EpochModel.Get("CORRUPTED7_EPOCH")
      //      };
      //  }

        public override string UnlockText => CreateCardUnlockText(Cards);

        public override void QueueUnlocks()
        {
            NTimelineScreen.Instance.QueueCardUnlock(Cards);
           // EpochModel.QueueTimelineExpansion(GetTimelineExpansion());
        }
    }
}