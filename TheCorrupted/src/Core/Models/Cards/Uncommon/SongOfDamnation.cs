using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Token;
using TheCorrupted.TheCorrupted.src.Core.Models.Commands;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon
{
    internal class SongOfDamnation() : CardModel(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self), ICustomModel
    {
        protected override bool HasEnergyCostX => true;
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();
        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<DoomPower>(),
            HoverTipFactory.FromCard<CommandArmy>()
        ];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
        [
            CardKeyword.Exhaust
        ];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new ArmyVar(3m),
            new PowerVar<DoomPower>(4),
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
            int xValue = ResolveEnergyXValue();
            for (int i = 0; i < xValue; i++)
            {
                await ArmyCmd.Summon(choiceContext, base.Owner, base.DynamicVars["Army"].BaseValue, this);
            }
            await PowerCmd.Apply<DoomPower>(Owner.Creature, DynamicVars["DoomPower"].BaseValue * xValue, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            DynamicVars["DoomPower"].UpgradeValueBy(1);
            DynamicVars["Army"].UpgradeValueBy(1m);
        }
    }

}