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
            Position = new vec3(-20.0f, 5.0f, 20.0f);
            Ambient = new vec3(0.1f, 0.1f, 0.1f);
            Diffuse = new vec3(0.5f, 1.0f, 0.5f);
            Specular = new vec3(1.0f, 1.0f, 1.0f);
        }
    }
}