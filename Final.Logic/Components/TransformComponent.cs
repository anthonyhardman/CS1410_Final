using GlmSharp;

namespace Final.Logic
{
    public class TransformComponent : Component
    {
        public vec3 Translate;
        public vec3 Scale;
        public vec3 Rotation;

        public TransformComponent() : base()
        {
            Translate = new vec3(0.0f, 0.0f, 0.0f);
            Scale = new vec3(1.0f, 1.0f, 1.0f);
            Rotation = new vec3(0.0f, 0.0f, 0.0f);
        }
    }
}