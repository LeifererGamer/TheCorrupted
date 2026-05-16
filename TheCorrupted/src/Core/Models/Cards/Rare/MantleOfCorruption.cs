using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Token;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;
using TheCorrupted.TheCorrupted.src.Core.Models.Relics;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Rare
{
internal class MantleOfCorruption() : CardModel(1, CardType.Power, CardRarity.Rare, TargetType.Self), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [
            HoverTipFactory.FromPower<DoomPower>(),
            HoverTipFactory.Static(StaticHoverTip.Block),
            HoverTipFactory.FromCard<CommandArmy>()
        ];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new PowerVar<DoomPower>(5),
            new BlockVar(6, ValueProp.Move),
            new RitualVar(),
            new ArmyVar(3),
            new CleansingVar(),
        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            NPowerUpVfx.CreateGhostly(base.Owner.Creature);
            await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
            IEnumerable<CardModel> curses = CorruptedCardPool.GetRandomCurses(Owner, 2);
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(curses, PileType.Draw, Owner, CardPilePosition.Random));
            await PowerCmd.Apply<MantleOfCorruptionPower>(choiceContext, Owner.Creature, DynamicVars.Block.IntValue, Owner.Creature, this);
        }


        protected override void OnUpgrade()
        {
           DynamicVars.Block.UpgradeValueBy(2);
        }

    }
}