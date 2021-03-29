using System.Collections.Generic;

namespace Final.Logic
{
    public interface IRenderer
    {
        int SCR_Width {get; set;}
        int SCR_Height {get; set;}
        List<RenderComponent> RenderComponents {get; set;}
        public void Draw();
    }
}