#region Assembly sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// D:\SteamLibrary\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.dll
// Decompiled with ICSharpCode.Decompiler 8.2.0.7535
#endregion

using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Basic;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;
using TheCorrupted.TheCorrupted.src.Core.Models.PotionPools;
using TheCorrupted.TheCorrupted.src.Core.Models.RelicPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Relics;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Characters;


public sealed class Corrupted : CustomCharacterModel
{
    public const string energyColorName = "corrupted";

    public override CharacterGender Gender => CharacterGender.Masculine;

    protected override CharacterModel? UnlocksAfterRunAs => null;

    public override Color NameColor => StsColors.purple;

    public override int StartingHp => 80;

    public override int StartingGold => 99;

    public override CardPoolModel CardPool => ModelDb.CardPool<CorruptedCardPool>();

    public override PotionPoolModel PotionPool => ModelDb.PotionPool<CorruptedPotionPool>();

    public override RelicPoolModel RelicPool => ModelDb.RelicPool<CorruptedRelicPool>();

    public override IEnumerable<CardModel> StartingDeck =>
    [
    ModelDb.Card<StrikeCorrupted>(),
        ModelDb.Card<StrikeCorrupted>(),
        ModelDb.Card<StrikeCorrupted>(),
        ModelDb.Card<StrikeCorrupted>(),
        ModelDb.Card<DefensiveRitual>(),
        ModelDb.Card<DefendCorrupted>(),
        ModelDb.Card<DefendCorrupted>(),
        ModelDb.Card<DefendCorrupted>(),
        ModelDb.Card<DefendCorrupted>(),
        ModelDb.Card<CorruptingStrike>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics => [ModelDb.Relic<CorruptedBladeRelic>()];
    protected override string CharacterSelectIconPath => ImageHelper.GetImagePath("packed/character_select/char_select_corrupted.png");

    protected override string CharacterSelectLockedIconPath => ImageHelper.GetImagePath("packed/character_select/char_select_corrupted_locked.png");
    public override float AttackAnimDelay => 0.15f;

    public override float CastAnimDelay => 0.25f;

    public override Color EnergyLabelOutlineColor => new Color("551FC9FF");

    public override Color DialogueColor => new Color("11035E");

    public override Color MapDrawingColor => new Color("A329CC");

    public override Color RemoteTargetingLineColor => new Color("7348E0FF");

    public override Color RemoteTargetingLineOutline => new Color("351280FF");

    public override string CharacterTransitionSfx => "event:/sfx/ui/wipe_ironclad";

    public override List<string> GetArchitectAttackVfx()
    {
        int num = 5;
        List<string> list = new List<string>(num);
        CollectionsMarshal.SetCount(list, num);
        Span<string> span = CollectionsMarshal.AsSpan(list);
        int num2 = 0;
        span[num2] = "vfx/vfx_attack_blunt";
        num2++;
        span[num2] = "vfx/vfx_heavy_blunt";
        num2++;
        span[num2] = "vfx/vfx_attack_slash";
        num2++;
        span[num2] = "vfx/vfx_bloody_impact";
        num2++;
        span[num2] = "vfx/vfx_rock_shatter";
        return list;
    }

    public override string CustomIconTexturePath => "res://images/ui/top_panel/character_icon_corrupted.png";

    public override string CustomIconOutlineTexturePath => "res://images/ui/top_panel/character_icon_corrupted_outline.png";


    public override string CustomCharacterSelectIconPath => "res://images/packed/char_select_corrupted.png";


    public override string CustomEnergyCounterPath => "res://scenes/combat/energy_counters/corrupted_energy_counter.tscn";

    public override string CustomCharacterSelectLockedIconPath =>
        "res://images/packed/char_select_corrupted_locked.png";

    public override string CustomVisualPath => "res://scenes/creature_visuals/corrupted.tscn";
    public override string CustomTrailPath => "res://scenes/vfx/card_trail_corrupted.tscn";
    public override string CustomIconPath => "res://scenes/ui/character_icons/corrupted_icon.tscn";
    public override string CustomRestSiteAnimPath => "res://scenes/rest_site/characters/corrupted_rest_site.tscn";
    public override string CustomMerchantAnimPath => "res://scenes/merchant/characters/corrupted_merchant.tscn";

    public override string CustomArmPointingTexturePath =>
        "res://images/ui/hands/multiplayer_hand_corrupted_point.png";

    public override string CustomArmRockTexturePath =>
        "res://images/ui/hands/multiplayer_hand_corrupted_rock.png";

    public override string CustomArmPaperTexturePath =>
        "res://images/ui/hands/multiplayer_hand_corrupted_paper.png";

    public override string CustomArmScissorsTexturePath =>
        "res://images/ui/hands/multiplayer_hand_corrupted_scissors.png";

    public override string CustomCharacterSelectBg => "res://scenes/screens/char_select/char_select_bg_corrupted.tscn";

    public override string CustomCharacterSelectTransitionPath =>
        "res://materials/transitions/corrupted_transition_mat.tres";

    public override string CustomMapMarkerPath => "res://images/packed/map/icons/map_marker_corrupted.png";

    public override string CustomAttackSfx => "event:/sfx/characters/ironclad/ironclad_attack";

}