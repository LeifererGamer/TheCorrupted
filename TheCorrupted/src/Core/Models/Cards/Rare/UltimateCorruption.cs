using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Curse;
using TheCorrupted.TheCorrupted.src.Core.Models.Commands;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Rare
{


internal class UltimateCorruption() : CardModel(0, CardType.Skill, CardRarity.Rare, TargetType.Self), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [
            CardKeyword.Exhaust,
        ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [
            HoverTipFactory.FromPower<DoomPower>(),
            HoverTipFactory.ForEnergy(Owner),
            HoverTipFactory.FromCard<SpreadingCorruption>(),
        ];

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CardsVar(1),
            new EnergyVar(1),
            new ArmyVar(3),
            new PowerVar<DoomPower>(5),
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
            await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
            await ArmyCmd.Summon(choiceContext, Owner, DynamicVars["Army"].BaseValue, cardPlay.Card);
            await PowerCmd.Apply<DoomPower>(choiceContext, Owner.Creature, DynamicVars["DoomPower"].BaseValue, Owner.Creature, this);
            await SpreadingCorruption.CreateInHand(Owner, cardPlay.Card.CombatState);
        }

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Innate);
        }

    }

}