using ImGuiNET;
namespace Final.Logic
{
    public class MaterialWidget : ImGuiWidget
    {
        MaterialComponent MaterialComponent_;

        public MaterialWidget(RenderComponent renderComponent, MaterialComponent materialComponent)
        {
            MaterialComponent_ = materialComponent;
        }

        public override void Run()
        {
            if (ImGui.CollapsingHeader("Material"))
            {
                ImGui.PushItemWidth(100);
                ImGui.Text("Ambient");
                MyColorPicker(ref MaterialComponent_.Ambient, "ambient");
                ImGui.Text("Diffuse");
                MyColorPicker(ref MaterialComponent_.Diffuse, "diffuse");
                ImGui.Text("Specular");
                MyColorPicker(ref MaterialComponent_.Specular, "specular");
                ImGui.Text("Shininess");
                IncreaseDecreaseDragFloat(ref MaterialComponent_.Shininess, "", "shininess");
            }
        }
    }
}