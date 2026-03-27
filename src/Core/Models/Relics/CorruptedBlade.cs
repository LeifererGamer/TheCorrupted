using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.src.Core.Models.Cards.Curse;

namespace TheCorrupted.src.Core.Models.Relics
{
    public sealed class CorruptedBlade : RelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Starter;

        protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromCard<CorruptionCorrupted>()];


        public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
        {
            if (player == base.Owner && combatState.RoundNumber == 1)
            {
                Flash();
                await CorruptionCorrupted.CreateInDrawPile(player, combatState, false);
            }
        }
        //var curse = combatState.CreateCard<Injury>(Owner);
        //await CardPileCmd.AddGeneratedCardToCombat(curse, PileType.Hand, true);
       
    }
}
