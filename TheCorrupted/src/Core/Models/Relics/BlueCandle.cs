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
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.GameInfo.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Curse;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Rare;
using TheCorrupted.TheCorrupted.src.Core.Models.Enchantments;
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Relics
{

internal class BlueCandle : RelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Uncommon;

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromKeyword(CardKeyword.Unplayable),
            HoverTipFactory.FromKeyword(CardKeyword.Exhaust),
        ];

        public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
        {
            if (player == Owner && combatState.RoundNumber == 1)
            {
                    IEnumerable<CardModel> cards = base.Owner.PlayerCombatState.AllCards.Where((CardModel c) => c.Type == CardType.Curse || (c.Type == CardType.Status && c.Owner.Creature.HasPower<StatusQuoPower>()));
                    foreach (CardModel card in cards)
                    {
                        await CardCmd.Afflict<Entangled>(card, 1m);
                        card.RemoveKeyword(CardKeyword.Unplayable);
                        card.AddKeyword(CardKeyword.Exhaust);
                    }
            }
        }

        public override async Task AfterCardEnteredCombat(CardModel card)
        {
            if (card.Owner == Owner && card.Type == CardType.Curse)
            {
                card.RemoveKeyword(CardKeyword.Unplayable);
                card.AddKeyword(CardKeyword.Exhaust);
            }
        }

        //  public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
        //  {
        //      modifiedCost = originalCost;
        //      if (card.Owner.Creature != Owner.Creature || card.Type != CardType.Curse)
        //      {
        //          return false;
        //      }
        //
        //      modifiedCost = originalCost + 2m;
        //      return true;
        //  }

        public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
        {
            if (cardPlay.Card.Owner == base.Owner && cardPlay.Card.Type == CardType.Curse && !cardPlay.IsAutoPlay && cardPlay.Card is not Enthralled)
            {
                await CreatureCmd.Damage(context, Owner.Creature, 1m, ValueProp.Move, cardPlay.Card);
            }
        }
    }
}