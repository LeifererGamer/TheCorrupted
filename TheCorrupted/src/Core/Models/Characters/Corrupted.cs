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
using TheCorrupted.TheCorrupted.src.Core.Models.Relics;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Characters;


public sealed class Corrupted : CharacterModel
    {
        public const string energyColorName = "corrupted";

        public override CharacterGender Gender => CharacterGender.Masculine;

        protected override CharacterModel? UnlocksAfterRunAs => null;

        public override Color NameColor => StsColors.purple;

        public override int StartingHp => 80;

        public override int StartingGold => 99;

        public override CardPoolModel CardPool => ModelDb.CardPool<CorruptedCardPool>();

        public override PotionPoolModel PotionPool => ModelDb.PotionPool<IroncladPotionPool>();

        public override RelicPoolModel RelicPool => ModelDb.RelicPool<IroncladRelicPool>();

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

        public override IReadOnlyList<RelicModel> StartingRelics =>[ModelDb.Relic<CorruptedBladeRelic>()];
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
    }
#if false // Decompilation log
'167' items in cache
------------------
Resolve: 'System.Runtime, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Runtime.dll'
------------------
Resolve: 'GodotSharp, Version=4.5.1.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'GodotSharp, Version=4.5.1.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\GodotSharp.dll'
------------------
Resolve: 'System.Collections, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Collections.dll'
------------------
Resolve: 'System.Text.RegularExpressions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Text.RegularExpressions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Text.RegularExpressions.dll'
------------------
Resolve: 'System.Runtime.InteropServices, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.InteropServices, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Runtime.InteropServices.dll'
------------------
Resolve: 'System.Text.Json, Version=9.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Text.Json, Version=9.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Text.Json.dll'
------------------
Resolve: 'System.Collections.Concurrent, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Collections.Concurrent, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Collections.Concurrent.dll'
------------------
Resolve: 'System.Linq, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Linq, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Linq.dll'
------------------
Resolve: 'System.IO.Compression, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Found single assembly: 'System.IO.Compression, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.IO.Compression.dll'
------------------
Resolve: 'System.Net.Http, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.Http, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Net.Http.dll'
------------------
Resolve: 'Steamworks.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
Could not find by name: 'Steamworks.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
------------------
Resolve: 'System.Runtime.Numerics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.Numerics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Runtime.Numerics.dll'
------------------
Resolve: 'System.IO.Hashing, Version=9.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Could not find by name: 'System.IO.Hashing, Version=9.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
------------------
Resolve: 'System.Runtime.Loader, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.Loader, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Runtime.Loader.dll'
------------------
Resolve: '0Harmony, Version=2.4.2.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: '0Harmony, Version=2.4.2.0, Culture=neutral, PublicKeyToken=null'
Load from: 'D:\SteamLibrary\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\0Harmony.dll'
------------------
Resolve: 'System.Diagnostics.StackTrace, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.StackTrace, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Diagnostics.StackTrace.dll'
------------------
Resolve: 'SmartFormat, Version=3.0.0.0, Culture=neutral, PublicKeyToken=568866805651201f'
Could not find by name: 'SmartFormat, Version=3.0.0.0, Culture=neutral, PublicKeyToken=568866805651201f'
------------------
Resolve: 'Sentry, Version=5.0.0.0, Culture=neutral, PublicKeyToken=fba2ec45388e2af0'
Could not find by name: 'Sentry, Version=5.0.0.0, Culture=neutral, PublicKeyToken=fba2ec45388e2af0'
------------------
Resolve: 'System.Diagnostics.Process, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Diagnostics.Process, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Diagnostics.Process.dll'
------------------
Resolve: 'Vortice.DXGI, Version=3.6.2.0, Culture=neutral, PublicKeyToken=5431ec61a7e925da'
Could not find by name: 'Vortice.DXGI, Version=3.6.2.0, Culture=neutral, PublicKeyToken=5431ec61a7e925da'
------------------
Resolve: 'System.Console, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Console, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Console.dll'
------------------
Resolve: 'System.Text.Encodings.Web, Version=9.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Text.Encodings.Web, Version=9.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Text.Encodings.Web.dll'
------------------
Resolve: 'System.Threading, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Threading.dll'
------------------
Resolve: 'System.ComponentModel.Primitives, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.ComponentModel.Primitives, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.ComponentModel.Primitives.dll'
------------------
Resolve: 'System.Threading.Thread, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Threading.Thread, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Threading.Thread.dll'
------------------
Resolve: 'System.Data.Common, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Data.Common, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Data.Common.dll'
------------------
Resolve: 'System.Memory, Version=9.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Found single assembly: 'System.Memory, Version=9.0.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Memory.dll'
------------------
Resolve: 'SharpGen.Runtime, Version=2.2.0.0, Culture=neutral, PublicKeyToken=a7c0d43f556c6402'
Could not find by name: 'SharpGen.Runtime, Version=2.2.0.0, Culture=neutral, PublicKeyToken=a7c0d43f556c6402'
------------------
Resolve: 'System.Net.Primitives, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Net.Primitives, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Net.Primitives.dll'
------------------
Resolve: 'System.Runtime.CompilerServices.Unsafe, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null'
Found single assembly: 'System.Runtime.CompilerServices.Unsafe, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\9.0.14\ref\net9.0\System.Runtime.CompilerServices.Unsafe.dll'
#endif
