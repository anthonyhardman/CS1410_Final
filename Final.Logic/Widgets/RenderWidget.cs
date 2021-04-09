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
            textBuffer = RenderComponent_.Model_.File;
        }

        public override void Run()
        {
            ImGui.Separator();

            ImGui.PushItemWidth(1);

            ImGui.PushItemWidth(250);
            if (ImGui.CollapsingHeader("Model"))
            {
                if (ImGui.InputText("", ref textBuffer, 200))
                {
                    RenderComponent_.Model_.File = textBuffer;
                }

                ImGui.SameLine();
                if (ImGui.Button("Change"))
                {
                    RenderComponent_.Model_ = new Model(textBuffer, new Shader("Shaders\\basicLightingVert.glsl", "Shaders\\basicLightingFrag.glsl"));
                }
            }
            if (ImGui.CollapsingHeader("Material"))
            {
                ImGui.PushItemWidth(100);
                ImGui.Text("Ambient");
                MyColorPicker(ref RenderComponent_.Model_.Material_.Ambient, "ambient");
                ImGui.Text("Diffuse");
                MyColorPicker(ref RenderComponent_.Model_.Material_.Diffuse, "diffuse");
                ImGui.Text("Specular");
                MyColorPicker(ref RenderComponent_.Model_.Material_.Specular, "specular");
                ImGui.Text("Shininess");
                IncreaseDecreaseDragFloat(ref RenderComponent_.Model_.Material_.Shininess, "", "shininess");
            }
        }
    }
}