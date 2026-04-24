using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib.Cards.Variables;

    internal class CleansingVar : DynamicVar
    {
        public const string Key = "Cleansing";
        public CleansingVar(decimal baseValue = 0m) : base(Key, baseValue)
        {
            this.WithTooltip();
        }
    }
