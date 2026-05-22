using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using TheCorrupted.TheCorrupted.src.Core.Models.Commands;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon
{
    internal class DoomingArmy() : DoomedCardModel(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self), ICustomModel
    {
        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<DoomPower>(),
            EnergyHoverTip,
        ];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new ArmyVar(8m),
            new DynamicVar ("DamageDiff", 4m),
            new DoomedVar(4),
            new EnergyVar(2),
        ];

        protected override async Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            decimal amount = getAmount(cardPlay, DynamicVars["DamageDiff"].BaseValue, DynamicVars["Army"].BaseValue);

            await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
            await ArmyCmd.Summon(choiceContext, Owner, amount, this);
        }

        protected override async Task OnNormalPlayExtra(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            CardPile hand = PileType.Hand.GetPile(Owner);
            if (hand.Cards.Where((c) => c.Type == CardType.Curse).ToList().Any())
            {
                await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars["Doomed"].UpgradeValueBy(2);
            DynamicVars["Army"].UpgradeValueBy(4m);
            DynamicVars["DamageDiff"].UpgradeValueBy(2m);
        }
    }

}