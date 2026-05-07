using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Afflictions
{
    public abstract class CustomAfflictionModel : AfflictionModel
    {
        public override bool HasExtraCardText => true;
    }
}
