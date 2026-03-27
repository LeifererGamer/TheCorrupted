using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.src.Core.Models.Powers
{

internal class HellfireBreathingPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;


        public override PowerStackType StackType => PowerStackType.Counter;


        public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
        {
            if (card.Type.Equals(CardType.Curse) || card.Type.Equals(CardType.Status) && base.Owner.HasPower<StatusQuoPower>())
            {
                foreach (Creature hittableEnemy in base.CombatState.HittableEnemies)
                {
                    NFireBurstVfx child = NFireBurstVfx.Create(hittableEnemy, 0.75f);
                    NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(child);
                }

                await CreatureCmd.Damage(choiceContext, base.CombatState.HittableEnemies, base.Amount, ValueProp.Unpowered, base.Owner, null);
            }
        }
    }
}