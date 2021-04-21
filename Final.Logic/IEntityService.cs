using System;
using System.Collections.Generic;

namespace Final.Logic
{
    public interface IEntityService
    {
        IEnumerable<RenderComponent> GetRenderComponents();
        IEnumerable<string> GetEntityNames();
        IEnumerable<Entity> GetEntities();
    }
}