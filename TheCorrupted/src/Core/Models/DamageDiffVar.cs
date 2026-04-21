using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.TheCorrupted.src.Core.Models
{
    public class DamageDiffVar : DynamicVar
    {
        public const string Key = "DamageDiff";
        public DamageDiffVar(decimal baseValue) : base(Key, baseValue)
        {
        }
    }
}
