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
using MegaCrit.Sts2.Core.Nodes.Vfx.Cards;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Rare
{

internal class DrainStrength() : CardModel(1, CardType.Skill, CardRarity.Rare, TargetType.AllEnemies)
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new DynamicVar("StrengthLoss", 1m),
        ];

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [
            CardKeyword.Exhaust,
        ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [
            HoverTipFactory.FromPower<StrengthPower>()
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.AttackAnimDelay);
            CardPile hand = PileType.Hand.GetPile(Owner);
            List<CardModel> items = hand.Cards.Where((c) => c.Type == CardType.Curse).ToList();
            var amount = items.Count;
            foreach (CardModel item in items)
            {
                if (item != null)
                {
                    await CardCmd.Exhaust(choiceContext, item);
                }
            }
            DynamicVars["StrengthLoss"].BaseValue += amount;
            IReadOnlyList<Creature> enemies = CombatState.HittableEnemies;
            foreach (Creature item in enemies)
            {
                NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NSpikeSplashVfx.Create(item));
            }
            await PowerCmd.Apply<CrushUnderPower>(enemies, DynamicVars["StrengthLoss"].BaseValue, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

    }

}