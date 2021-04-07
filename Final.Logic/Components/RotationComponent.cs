using GlmSharp;

namespace Final.Logic
{
    public class RotationComponent : Component
    {
        public vec3 Rotation;

        public RotationComponent() : base()
        {
            Rotation = new vec3(1.0f, 1.0f, 1.0f);
        }
    }
}