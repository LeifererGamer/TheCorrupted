using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.src.Core.Models.CardPools;
using static Godot.HttpRequest;

namespace TheCorrupted.src.Core.Models.Cards.Uncommon
{
    internal class CleansingBlock() : CardModel(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        public override bool GainsBlock => true;

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new BlockVar(8m, ValueProp.Move),
            new PowerVar<DoomPower>(5m),
            new DamageVar(3, ValueProp.Move), //Healing
        ];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => [

        HoverTipFactory.FromPower<DoomPower>(),
        ];

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [
            CardKeyword.Exhaust,
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
            if (Owner.Creature.HasPower<DoomPower>()) { 
                var doomPower = Owner.Creature.GetPower<DoomPower>();
                if (doomPower.Amount < DynamicVars.Doom.BaseValue)
                    await PowerCmd.ModifyAmount(doomPower, -doomPower.Amount, Owner.Creature, cardPlay.Card);
                else
                    await PowerCmd.ModifyAmount(doomPower, -DynamicVars.Doom.BaseValue, Owner.Creature, cardPlay.Card);
                await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Damage.BaseValue);
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(2m);
            DynamicVars.Doom.UpgradeValueBy(3m);
            DynamicVars.Damage.UpgradeValueBy(1m);
        }

    }

}