using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
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
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Curse;
using TheCorrupted.TheCorrupted.src.Core.Models.Commands;
using TheCorrupted.TheCorrupted.src.Core.Models.RelicPools;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Relics
{
    internal class Armynomicon : RelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Common;

        public override bool SpawnsPets => true;

        protected override IEnumerable<DynamicVar> CanonicalVars => 
        [
            new ArmyVar(5)
        ];

        public override async Task BeforeCombatStart()
        {
            await ArmyCmd.Summon(new ThrowingPlayerChoiceContext(), base.Owner, DynamicVars["Army"].BaseValue, this);
        }
    }
}