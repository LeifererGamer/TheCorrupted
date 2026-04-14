using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.src.Core.Models
{
    internal class CleansingVar : DynamicVar
    {
        public const string Key = "Cleansing";
        public CleansingVar(decimal baseValue) : base(Key, baseValue)
        {
            this.WithTooltip();
        }
    }
}