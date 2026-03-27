using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.src.Core.Models.Powers;


namespace TheCorrupted.src.Core.Models.Cards.Rare
{

internal class DoomingStrength() : CardModel(3, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();


        protected override IEnumerable<IHoverTip> ExtraHoverTips => [

        HoverTipFactory.FromPower<StrengthPower>(),
        HoverTipFactory.FromPower<DoomPower>(),
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<DoomingStrengthPower>(base.Owner.Creature, 1m, base.Owner.Creature, this);
        }


        protected override void OnUpgrade()
        {
            base.EnergyCost.UpgradeBy(-1);
        }

    }
}
