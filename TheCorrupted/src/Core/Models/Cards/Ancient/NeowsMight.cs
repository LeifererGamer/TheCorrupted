using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Ancient
{

internal class NeowsMight() : TheCorruptedCardModel(1, CardType.Skill, CardRarity.Ancient, TargetType.Self), ICustomModel
    {
        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new RitualVar(),
            new BlockVar(10m,ValueProp.Move),
            new CardsVar(2),
            new CalculationBaseVar(0m),
            new ExtraDamageVar(1m),
            new CalculatedDamageVar(ValueProp.Move).WithMultiplier(static (card, _) => card.Owner.Creature.HasPower<DoomPower>() ? card.Owner.Creature.GetPower<DoomPower>().Amount : 0)
        ];

        protected override async Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
            var success = await Ritual.PerformRitual(choiceContext, Owner, this, async (card) =>
            {
                foreach (CardModel item in PileType.Hand.GetPile(Owner).Cards)
                {
                    item.EnergyCost.SetThisTurnOrUntilPlayed(item.EnergyCost.Canonical - 1, reduceOnly: true);
                }
            }, true);
            if (!success)
            {
                foreach (Creature hittableEnemy in CombatState.HittableEnemies)
                {
                    NFireBurstVfx child = NFireBurstVfx.Create(hittableEnemy, 0.75f);
                    NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(child);

                }
                await DamageCmd.Attack(DynamicVars.CalculatedDamage).FromCard(this).TargetingAllOpponents(CombatState).Execute(choiceContext);
            }
        }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

    }

}