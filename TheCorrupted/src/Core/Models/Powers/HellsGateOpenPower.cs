using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Powers
{
    internal class HellsGateOpenPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;


        public override PowerStackType StackType => PowerStackType.Counter;


        public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
        {
            Flash();
            IEnumerable<CardModel> distinctForCombat = CardFactory.GetDistinctForCombat(Owner.Player, ModelDb.CardPool<CurseCardPool>().GetUnlockedCards(Owner.Player.UnlockState, CombatState.RunState.CardMultiplayerConstraint), 1, CombatState.RunState.Rng.CombatCardGeneration);
            foreach (CardModel item in distinctForCombat)
            {
                await CardPileCmd.AddGeneratedCardToCombat(item, PileType.Hand, addedByPlayer: true);
            }
        }
    }
}