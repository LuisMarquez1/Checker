using Checker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Application.Extensions
{
    public static class ContactStateExtensions
    {
        public static bool HasNc(this ContactState state)
        {
            return (state & ContactState.NC) == ContactState.NC;
        }

        public static bool HasNo(this ContactState state)
        {
            return (state & ContactState.NO) == ContactState.NO;
        }
    }
}
