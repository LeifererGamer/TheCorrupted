using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Curse;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Relics
{

    internal class Necronomicon : RelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Shop;

        private bool _activatedThisTurn;

        public override async Task AfterObtained()
        {
            await CardPileCmd.AddCurseToDeck<Necronomicurse>(base.Owner);
            await Cmd.Wait(0.75f);
        }

        private bool ActivatedThisTurn
        {
            get
            {
                return _activatedThisTurn;
            }
            set
            {
                AssertMutable();
                _activatedThisTurn = value;
            }
        }

        public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            if (player == Owner)
            {
                ActivatedThisTurn = false;
                await Cmd.Wait(0.25f);
            }
        }

        public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
        {
            if (CombatManager.Instance.IsInProgress && cardPlay.Card.Owner == base.Owner && cardPlay.Card.Type == CardType.Attack && !ActivatedThisTurn && !cardPlay.IsAutoPlay)
            {
                Flash();
                ActivatedThisTurn = true;
                await CardCmd.AutoPlay(context, cardPlay.Card, cardPlay.Target);
               
            }
        }
    }
    }
