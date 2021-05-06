using ImGuiNET;

namespace Final.Logic
{
    public class TransformWidget : ImGuiWidget
    {
        public TransformComponent transformComponent;

        public TransformWidget(TransformComponent transform)
        {
            transformComponent = transform;
        }

        public override void Run()
        {
            ImGui.Separator();
            if (ImGui.CollapsingHeader("Transform"))
            {
                ImGui.Text("Translate");
                IncreaseDecreaseDragFloat(ref transformComponent.Translate.x, "X ", "Translate X");
                IncreaseDecreaseDragFloat(ref transformComponent.Translate.y, "Y ", "Translate Y");
                IncreaseDecreaseDragFloat(ref transformComponent.Translate.z, "Z ", "Translate Z");

                ImGui.Text("Rotate");
                IncreaseDecreaseDragFloat(ref transformComponent.Rotation.x, "X ", "Rotate X");
                IncreaseDecreaseDragFloat(ref transformComponent.Rotation.y, "Y ", "Rotate Y");
                IncreaseDecreaseDragFloat(ref transformComponent.Rotation.z, "Z ", "Rotate Z");

                ImGui.Text("Scale");
                IncreaseDecreaseDragFloat(ref transformComponent.Scale.x, "X ", "Scale X");
                IncreaseDecreaseDragFloat(ref transformComponent.Scale.y, "Y ", "Scale Y");
                IncreaseDecreaseDragFloat(ref transformComponent.Scale.z, "Z ", "Scale Z");
            }
        }
    }
}