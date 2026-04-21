using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace TheCorrupted.TheCorrupted.src.Core.Models
{
    internal static class Cleansing
    {
        internal static async Task<decimal> PerformCleansing(decimal amount, Creature creature, CardModel card)
        {
            if (creature.HasPower<DoomPower>())
            {
                if (creature.GetPower<DoomPower>().Amount <= amount)
                    amount = creature.GetPower<DoomPower>().Amount;

                await PowerCmd.Apply<DoomPower>(creature, -amount, creature, card);
            }
            else
            {
                amount = 0;
            }
            return amount;
        }
    }
}

