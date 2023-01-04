using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedKernel.Interfaces;

public interface IEntity: IEntity<Guid>
{
}

public interface IEntity<T>
{
    T Id { get; }
}
