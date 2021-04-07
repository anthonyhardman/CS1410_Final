using GlmSharp;

namespace Final.Logic
{
    public class ScaleComponent : Component
    {
        public vec3 Scale;

        public ScaleComponent() : base()
        {
            Scale = new vec3(1.0f, 1.0f, 1.0f);
        }
    }
}