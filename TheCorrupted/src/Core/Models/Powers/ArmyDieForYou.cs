using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Powers;

public sealed class ArmyDieForYouPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    public override bool ShouldPlayVfx => false;

    public override Creature ModifyUnblockedDamageTarget(Creature target, decimal _, ValueProp props, Creature? __)
    {
        if (target != Owner.PetOwner?.Creature)
        {
            return target;
        }

        if (Owner.IsDead)
        {
            return target;
        }

        if (!props.IsPoweredAttack())
        {
            return target;
        }

        return Owner;
    }

    public override bool ShouldAllowHitting(Creature creature)
    {
        return creature.IsAlive;
    }

    public override bool ShouldCreatureBeRemovedFromCombatAfterDeath(Creature creature)
    {
        if (creature != Owner)
        {
            return true;
        }

        return false;
    }

    public override bool ShouldPowerBeRemovedAfterOwnerDeath()
    {
        return false;
    }
}