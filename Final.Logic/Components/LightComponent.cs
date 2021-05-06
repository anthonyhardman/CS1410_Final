using GlmSharp;

namespace Final.Logic
{
    public class LightComponent : Component
    {
        public TransformComponent TransformComponent_;
        public vec3 Ambient;
        public vec3 Diffuse;
        public vec3 Specular;
        public float Constant;
        public float Linear;
        public float Quadratic; 

        public LightComponent(TransformComponent transformComponent)
        {
            TransformComponent_ = transformComponent;
            Ambient = new vec3(0.1f, 0.1f, 0.1f);
            Diffuse = new vec3(0.5f, 1.0f, 0.5f);
            Specular = new vec3(1.0f, 1.0f, 1.0f);
            Constant = 1.0f;
            Linear = 0.09f;
            Quadratic = 0.032f;

            Mesh.Lights.Add(this);
        }
    }
}