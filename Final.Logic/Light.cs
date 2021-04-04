using GlmSharp;

namespace Final.Logic
{
    public class Light
    {
        public vec3 Position;
        public vec3 Ambient;
        public vec3 Diffuse;
        public vec3 Specular;

        public Light()
        {
            Position = new vec3(0.0f, 5.0f, 25.0f);
            Ambient = new vec3(1.0f, 1.0f, 1.0f);
            Diffuse = new vec3(1.0f, 1.0f, 1.0f);
            Specular = new vec3(1.0f, 1.0f, 1.0f);
        }
    }
}