using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Rare
{
internal class CorruptedDelivery() : CardModel(1, CardType.Power, CardRarity.Rare, TargetType.Self)
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
            await PowerCmd.Apply<CorruptedDeliveryPower>(base.Owner.Creature, 1m, base.Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }
    }
}