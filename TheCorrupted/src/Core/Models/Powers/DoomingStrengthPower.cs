using BaseLib.Cards.Variables;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Powers
{

    public sealed class DoomingStrengthPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;


        public override PowerStackType StackType => PowerStackType.Counter;
        protected override IEnumerable<IHoverTip> ExtraHoverTips => [

        HoverTipFactory.FromPower<DoomPower>(),
        HoverTipFactory.FromPower<StrengthPower>()
        ];

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DynamicVar("Divider", 3),
        ];

        public int amount = 0;
        public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
        {
            if (side != Owner.Side) return;

            Flash();
            amount = combatState.Creatures.First().HasPower<DoomPower>() ? combatState.Creatures.First().GetPower<DoomPower>().Amount / DynamicVars["Divider"].IntValue * Amount : 0;
            await PowerCmd.Apply<StrengthPower>(Owner, amount, Owner, null);                                        
        }
        
        public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
        {
            if (side != Owner.Side) return;

            Flash();
            await PowerCmd.Apply<StrengthPower>(Owner, -amount, Owner, null);
        }
    }
}
