using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.TheCorrupted.src.Core.Models
{
    internal class ArmyVar : DynamicVar
    {
        public const string Key = "Army";
        public ArmyVar(decimal baseValue) : base(Key, baseValue)
        {
            this.WithTooltip();
        }
    }
}
