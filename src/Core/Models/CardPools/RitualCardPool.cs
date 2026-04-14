using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Unlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.src.Core.Models.Cards.Ancient;
using TheCorrupted.src.Core.Models.Cards.Basic;
using TheCorrupted.src.Core.Models.Cards.Common;
using TheCorrupted.src.Core.Models.Cards.Rare;
using TheCorrupted.src.Core.Models.Cards.Uncommon;

namespace TheCorrupted.src.Core.Models.CardPools
{
    internal class RitualCardPool : CardPoolModel, ICustomModel
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
                ModelDb.Card<CorruptionToAshes>(),
                ModelDb.Card<HellfireBarrier>(),
                ModelDb.Card<DoubleShield>(),
                ModelDb.Card<CorruptedSteel>(),
                ModelDb.Card<ForbiddenAlchemy>(),
                ModelDb.Card<RitualOfGreaterDefence>(),
                ModelDb.Card<HellfireCardWall>(),
                ModelDb.Card<RitualOfStrength>(),
                ModelDb.Card<SmokeScreen>(),
                ModelDb.Card<CleansingRitual>(),
            ];
        }

        protected override IEnumerable<CardModel> FilterThroughEpochs(UnlockState unlockState, IEnumerable<CardModel> cards)
        {
            return cards.ToList();
        }
    }

}