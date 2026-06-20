using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Powers;
using TheCorrupted.TheCorrupted.src.Core.Models.Commands;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Powers
{
    internal class GroupSummoningPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Single;

        public override async Task AfterSummon(PlayerChoiceContext choiceContext, Player summoner, decimal amount)
        {
            Player summoner2 = summoner;
            if (summoner2 != base.Owner.Player)
            {
                return;
            }
            await CreatureCmd.TriggerAnim(summoner.Creature, "Cast", summoner.Character.CastAnimDelay);
            await PowerCmd.Apply<GroupSummoningDonePower>(choiceContext, summoner.Creature, 1, summoner.Creature, null, silent: true);
            IEnumerable<Creature> enumerable = base.CombatState.PlayerCreatures.Where((Creature c) => (c?.IsAlive ?? false) && (c.Player != summoner)).ToList();
            foreach (Creature item in enumerable)
            {
                if (!item.Player.Creature.HasPower<GroupSummoningDonePower>())
                {
                    await PowerCmd.Apply<GroupSummoningDonePower>(choiceContext, [item.Player.Creature], 1, summoner.Creature, null, silent: true);
                    await ArmyCmd.Summon(choiceContext, item.Player, amount, this);
                }
            }
            foreach (Creature item in enumerable)
            {
                if (item.Player.Creature.HasPower<GroupSummoningDonePower>())
                {
                    await PowerCmd.Apply<GroupSummoningDonePower>(choiceContext, [item.Player.Creature], -1, summoner.Creature, null, silent: true);
                }
            }
            await PowerCmd.Apply<GroupSummoningDonePower>(choiceContext, summoner.Creature, -1, summoner.Creature, null, silent: true);
        }
    }
}