using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.Screens.Timeline;
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

namespace TheCorrupted.TheCorrupted.src.Core.Timeline.Epochs
{
    internal class Corrupted2Epoch : EpochModel
    {
        public override string Id => "CORRUPTED2_EPOCH";

        public override EpochEra Era => EpochEra.Blight1;

        public override int EraPosition => 4;

        public override string StoryId => "Corrupted";

        public override bool IsArtPlaceholder => false;

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

        public override EpochModel[] GetTimelineExpansion()
        {
            return new EpochModel[5]
            {
            EpochModel.Get(EpochModel.GetId<Corrupted3Epoch>()),
            EpochModel.Get(EpochModel.GetId<Corrupted4Epoch>()),
            EpochModel.Get(EpochModel.GetId<Corrupted5Epoch>()),
            EpochModel.Get(EpochModel.GetId<Corrupted6Epoch>()),
            EpochModel.Get(EpochModel.GetId<Corrupted7Epoch>())
            };
        }

        public override string UnlockText => CreateCardUnlockText(Cards);

        public override void QueueUnlocks()
        {
            NTimelineScreen.Instance.QueueCardUnlock(Cards);
            EpochModel.QueueTimelineExpansion(GetTimelineExpansion());
        }
    }
}