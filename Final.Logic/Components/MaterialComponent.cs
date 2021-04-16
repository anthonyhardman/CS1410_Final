using GlmSharp;
namespace Final.Logic
{
    public class MaterialComponent : Component
    {
        public vec3 Ambient;
        public vec3 Diffuse;
        public vec3 Specular;
        public float Shininess;

        public MaterialComponent(RenderComponent renderComponent)
        {
            Ambient = new vec3(0.5f, 0.5f, 0.5f);
            Diffuse = new vec3(0.5f, 0.5f, 0.5f);
            Specular = new vec3(0.5f, 0.5f, 0.5f);
            Shininess = 1.0f;

            renderComponent.Model_.MaterialComponent_ = this;
        }
    }
}