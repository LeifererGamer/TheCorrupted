using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib.Cards.Variables;

    internal class ArmyVar : DynamicVar
    {
        public const string Key = "Army";
        public ArmyVar(decimal baseValue = 0m) : base(Key, baseValue)
        {
            this.WithTooltip();
        }
    }

