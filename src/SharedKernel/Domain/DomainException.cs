using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedKernel.Domain;

public class DomainException : Exception
{
    public DomainException(string businessMessage) : base(businessMessage) { }
}
