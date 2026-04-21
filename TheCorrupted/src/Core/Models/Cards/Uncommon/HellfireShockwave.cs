using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
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
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon
{

internal class HellfireShockwave() : CardModel(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new DamageVar(10m, ValueProp.Move),
        ];


        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {

            var repeat = 1;
            CardPile hand = PileType.Hand.GetPile(Owner);
            if (hand.Cards.Any((c) => c.Type == CardType.Curse))
                repeat = 2;
            for (int i = 0; i < repeat; i++)
            {
                foreach (Creature hittableEnemy in CombatState.HittableEnemies)
                {
                    NFireBurstVfx child = NFireBurstVfx.Create(hittableEnemy, 0.75f);
                    NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(child);

                }
                await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(CombatState).Execute(choiceContext); ;
            }

        }

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(3m);
        }
    }
}