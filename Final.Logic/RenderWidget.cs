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

            if (ImGui.CollapsingHeader("Render"))
            {
                ImGui.PushItemWidth(1);

                ImGui.PushItemWidth(250);

                ImGui.TreePush();
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

                    ImGui.PushItemWidth(100);

                    ImGui.TreePush();
                    if (ImGui.CollapsingHeader("Material"))
                    {
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
    }
}