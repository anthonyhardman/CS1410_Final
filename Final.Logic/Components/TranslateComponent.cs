using GlmSharp;

namespace Final.Logic
{
    public class TranslateComponent : Component
    {
        public vec3 Translate;

        public TranslateComponent() : base()
        {
            Translate = new vec3(1.0f, 1.0f, 1.0f);
        }
    }
}