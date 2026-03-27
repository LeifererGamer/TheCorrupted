using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
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

namespace TheCorrupted.src.Core.Models.Powers
{

    public sealed class DoomingStrengthPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;


        public override PowerStackType StackType => PowerStackType.Counter;
        protected override IEnumerable<IHoverTip> ExtraHoverTips => [

        HoverTipFactory.FromPower<DoomPower>(),
        HoverTipFactory.FromPower<StrengthPower>()
        ];

        public int amount = 0;
        public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
        {
            if (side == base.Owner.Side)
            {
                Flash();
                amount = combatState.Creatures.First().HasPower<DoomPower>() ? combatState.Creatures.First().GetPower<DoomPower>().Amount / 2 * base.Amount : 0;
                await PowerCmd.Apply<StrengthPower>(base.Owner, amount, base.Owner, null);
            }
        }
        public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
        {
            if (side == base.Owner.Side)
            {
                Flash();
                await PowerCmd.Apply<StrengthPower>(base.Owner, -amount, base.Owner, null);
            }
        }
    }
}
