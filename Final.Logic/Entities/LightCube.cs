using GlmSharp;

namespace Final.Logic
{
    public class LightCube : Cube
    {
        public LightCube() : base()
        {
            Name = $"Light{Name}";
            AddComponent<LightComponent>();

            GetComponent<RenderComponent>().Model_.Shader_ =  new Shader("Shaders\\basicVert.glsl", "Shaders\\basicFrag.glsl");
        }
    }
}