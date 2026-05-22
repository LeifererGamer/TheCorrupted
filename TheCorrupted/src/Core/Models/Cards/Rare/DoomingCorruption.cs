using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Token;
using TheCorrupted.TheCorrupted.src.Core.Models.Commands;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;
using TheCorrupted.TheCorrupted.src.Core.Models.Relics;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Rare
{
internal class DoomingCorruption() : TheCorruptedCardModel(2, CardType.Power, CardRarity.Rare, TargetType.Self), ICustomModel
    {
        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<DoomPower>(),
        ];

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new RitualVar(3),
        ];

        protected override async Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
            IEnumerable<CardModel> curses = CorruptedCardPool.GetRandomCurses(Owner, 2);
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(curses, PileType.Draw, Owner, CardPilePosition.Random));
            await PowerCmd.Apply<DoomingCorruptionPower>(choiceContext, base.Owner.Creature, DynamicVars["Ritual"].IntValue, base.Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            DynamicVars["Ritual"].UpgradeValueBy(2m);
        }
    }
}