using MegaCrit.Sts2.Core.Timeline;
using TheCorrupted.TheCorrupted.src.Core.Timeline.Epochs;

namespace TheCorrupted.TheCorrupted.src.Core.Timeline.Stories
{
    internal class CorruptedStory : StoryModel
    {
        // Use lowercase so StringHelper.Slugify() doesn't break it
        protected override string Id => "corrupted";

        public override EpochModel[] Epochs => new EpochModel[6]
        {
            EpochModel.Get<Corrupted2Epoch>(),
            EpochModel.Get<Corrupted3Epoch>(),
            EpochModel.Get<Corrupted4Epoch>(),
            EpochModel.Get<Corrupted5Epoch>(),
            EpochModel.Get<Corrupted6Epoch>(),
            EpochModel.Get<Corrupted7Epoch>()
        };
    }
}