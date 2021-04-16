namespace Final.Logic
{
    public class ShaderComponent : Component
    {
        public string VertexFile;
        public string FragmentFile;
        public RenderComponent RenderComponent_;

        public ShaderComponent() : base()
        {

        }

        public ShaderComponent(RenderComponent renderComponent) : base()
        {
            VertexFile = renderComponent.Model_.Shader_.VertexFile;
            FragmentFile = renderComponent.Model_.Shader_.FragmentFile;
            RenderComponent_ = renderComponent;
        }
    }
}