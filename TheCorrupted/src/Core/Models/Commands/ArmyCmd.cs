using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.TestSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Token;
using TheCorrupted.TheCorrupted.src.Core.Models.Monsters;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Commands
{
    public static class ArmyCmd
    {
        public static async Task<SummonResult> Summon(PlayerChoiceContext choiceContext, Player summoner, decimal amount, AbstractModel? source)
        {
            Player summoner2 = summoner;
            CombatState combatState = summoner2.Creature.CombatState;
            amount = Hook.ModifySummonAmount(combatState, summoner2, amount, source);
            if (amount == 0m)
            {
                return new SummonResult(summoner2.Osty, 0m);
            }

            if (CombatManager.Instance.IsInProgress)
            {
                SfxCmd.Play("event:/sfx/characters/necrobinder/necrobinder_summon");
            }

            Creature army = combatState.Allies.FirstOrDefault((c) => c.PetOwner == summoner2);
            if (summoner2.IsOstyAlive)
            {
                await CreatureCmd.GainMaxHp(summoner2.Osty, amount);
            }
            else
            {
                await GiveCommandArmy(summoner2, source);
                bool isReviving = army != null;
                if (isReviving)
                {
                    if (army.IsAlive)
                    {
                        throw new InvalidOperationException("We shouldn't make it here if Osty is still alive!");
                    }

                    summoner2.PlayerCombatState.AddPetInternal(army);
                }
                else
                {
                    army = await PlayerCmd.AddPet<Army>(summoner2);
                    NCreature ostyNode = NCombatRoom.Instance?.GetCreatureNode(army);
                    if (ostyNode != null)
                    {
                        ostyNode.Modulate = Colors.Transparent;
                        Tween tween = ostyNode.CreateTween();
                        tween.TweenProperty(ostyNode, "modulate:a", 1, 0.34999999403953552).From(0);
                        ostyNode.StartReviveAnim();
                    }

                    await PowerCmd.Apply<DieForYouPower>(army, 1m, null, null);
                    ostyNode?.TrackBlockStatus(summoner2.Creature);
                }

                await CreatureCmd.SetMaxHp(army, amount);
                await CreatureCmd.Heal(army, amount, isReviving);
                if (isReviving)
                {
                    await Hook.AfterOstyRevived(combatState, army);
                }
            }

            if (TestMode.IsOff)
            {
                NCreature nCreature = NCombatRoom.Instance?.GetCreatureNode(army);
                nCreature.OstyScaleToSize(army.MaxHp, 0.75f);
            }

            CombatManager.Instance.History.Summoned(combatState, (int)amount, summoner2);
            await Hook.AfterSummon(combatState, choiceContext, summoner2, amount);
            return new SummonResult(summoner2.Osty, amount);
        }

        public static async Task<IEnumerable<CommandArmy>> GiveCommandArmy(Player player, AbstractModel? source)
        {
            if (CombatManager.Instance.IsOverOrEnding)
            {
                return Array.Empty<CommandArmy>();
            }

            List<CommandArmy> commandArmies = GetCommandArmy(player, includeExhausted: false).ToList();
            if (commandArmies.Count == 0)
            {
                CommandArmy commandCard = player.Creature.CombatState.CreateCard<CommandArmy>(player);
                await CardPileCmd.AddGeneratedCardToCombat(commandCard, PileType.Hand, addedByPlayer: true);
                commandArmies.Add(commandCard);
            }
            return commandArmies;
        }

        private static IEnumerable<CommandArmy> GetCommandArmy(Player player, bool includeExhausted)
        {
            return player.PlayerCombatState.AllCards.Where(delegate (CardModel c)
            {
                if (!c.IsDupe)
                {
                    if (!includeExhausted)
                    {
                        CardPile? pile = c.Pile;
                        if (pile == null)
                        {
                            return true;
                        }

                        return pile.Type != PileType.Exhaust;
                    }

                    return true;
                }

                return false;
            }).OfType<CommandArmy>();
        }
    }
}
