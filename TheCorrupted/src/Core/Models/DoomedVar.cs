using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib.Cards.Variables;

public class DoomedVar : DynamicVar
{
    public const string Key = "Doomed";
    public DoomedVar(decimal baseValue) : base(Key, baseValue)
    {
        this.WithTooltip();
    }
}
