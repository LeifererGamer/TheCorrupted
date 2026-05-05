using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using TheCorrupted.TheCorrupted.src.Core.Models.Relics;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Powers
{
    internal class HellsGateOpenPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;


        public override PowerStackType StackType => PowerStackType.Counter;


        public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
        {
            if (player != Owner.Player)
                return;

            Flash();
            IEnumerable<CardModel> curses = CardFactory.GetDistinctForCombat(Owner.Player, from c in ModelDb.CardPool<CurseCardPool>().GetUnlockedCards(Owner.Player.UnlockState, Owner.Player.RunState.CardMultiplayerConstraint)
                                                                                    where c.Type == CardType.Curse && (c is not Enthralled || c.Owner.Relics.Where(r => r is BlueCandle).Any())
                                                                                    select c, Amount, Owner.Player.RunState.Rng.CombatCardGeneration);

            await CardPileCmd.AddGeneratedCardsToCombat(curses, PileType.Hand, addedByPlayer: true);
        }
    }
}