using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Kernel;

public class DomainException : Exception
{
    internal DomainException(string businessMessage) : base(businessMessage) { }
}
