using ImGuiNET;

namespace Final.Logic
{
    public class RenderWidget : ImGuiWidget
    {
        RenderComponent RenderComponent_;
        string textBuffer;

        public RenderWidget(RenderComponent renderComponent)
        {
            RenderComponent_ = renderComponent;

            if (RenderComponent_.Model_ != null)
            {
                textBuffer = RenderComponent_.Model_.File;
            }
            else
            {
                textBuffer = "";
            }
        }

        public override void Run()
        {
            ImGui.PushItemWidth(250);
            ImGui.PushID("Model");
            if (ImGui.CollapsingHeader("Model"))
            {
                if (ImGui.InputText("", ref textBuffer, 200))
                {
                    RenderComponent_.Model_.File = textBuffer;
                }
                if (ImGui.Button("Change"))
                {
                    MaterialComponent material = RenderComponent_.Model_.MaterialComponent_;
                    RenderComponent_.Model_ = new Model(textBuffer, new Shader("Shaders\\basicLightingVert.glsl", "Shaders\\basicLightingFrag.glsl"));
                    RenderComponent_.Model_.MaterialComponent_ = material;
                }
            }
        }
    }
}