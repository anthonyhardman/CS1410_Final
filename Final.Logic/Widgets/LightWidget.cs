using ImGuiNET;

namespace Final.Logic
{
    public class LightWidget : ImGuiWidget
    {
        LightComponent LightComponent_;

        public LightWidget(LightComponent lightComponent)
        {
            LightComponent_ = lightComponent;
        }

        public override void Run()
        {
            ImGui.PushItemWidth(250);

            ImGui.PushID("light");
            if (ImGui.CollapsingHeader("Light"))
            {
                ImGui.PushItemWidth(100);
                ImGui.Text("Ambient");
                MyColorPicker(ref LightComponent_.Ambient, "ambient");
                ImGui.Text("Diffuse");
                MyColorPicker(ref LightComponent_.Diffuse, "diffuse");
                ImGui.Text("Specular");
                MyColorPicker(ref LightComponent_.Specular, "specular");
                IncreaseDecreaseDragFloat(ref LightComponent_.Constant, "Constant", "Constant");
                IncreaseDecreaseDragFloat(ref LightComponent_.Linear, "Linear", "Linear");
                IncreaseDecreaseDragFloat(ref LightComponent_.Quadratic, "Quadratic", "Quadratic");
            }
        }
    }
}