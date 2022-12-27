using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Kernel;

public interface IEntity
{
    Guid Id { get; }
}
