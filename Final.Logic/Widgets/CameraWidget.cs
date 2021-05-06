using ImGuiNET;

namespace Final.Logic
{
    public class CameraWidget : ImGuiWidget
    {
        CameraComponent CameraComponent_;

        public CameraWidget(CameraComponent cameraComponent)
        {
            CameraComponent_ = cameraComponent;
        }
        public override void Run()
        {
            ImGui.Separator();
            if (ImGui.CollapsingHeader("Camera"))
            {
                ImGui.Text("Front");
                IncreaseDecreaseDragFloat(ref CameraComponent_.Front.x, "X ", "Front X");
                IncreaseDecreaseDragFloat(ref CameraComponent_.Front.y, "Y ", "Front Y");
                IncreaseDecreaseDragFloat(ref CameraComponent_.Front.z, "Z ", "Front Z");

                ImGui.Text("Up");
                IncreaseDecreaseDragFloat(ref CameraComponent_.Up.x, "X ", "Up X");
                IncreaseDecreaseDragFloat(ref CameraComponent_.Up.y, "Y ", "Up Y");
                IncreaseDecreaseDragFloat(ref CameraComponent_.Up.z, "Z ", "Up Z");
                
                ImGui.Text("Right");
                IncreaseDecreaseDragFloat(ref CameraComponent_.Right.x, "X ", "Right X");
                IncreaseDecreaseDragFloat(ref CameraComponent_.Right.y, "Y ", "Right Y");
                IncreaseDecreaseDragFloat(ref CameraComponent_.Right.z, "Z ", "Right Z");

                ImGui.Text("World Up");
                IncreaseDecreaseDragFloat(ref CameraComponent_.WorldUp.x, "X ", "WorldUp X");
                IncreaseDecreaseDragFloat(ref CameraComponent_.WorldUp.y, "Y ", "WorldUp Y");
                IncreaseDecreaseDragFloat(ref CameraComponent_.WorldUp.z, "Z ", "WorldUp Z");
                
                ImGui.Spacing();
                ImGui.Spacing();
                IncreaseDecreaseDragFloat(ref CameraComponent_.MovementSpeed, "Movement Speed", "Movement Speed");
                IncreaseDecreaseDragFloat(ref CameraComponent_.MouseSensitivity, "Mouse Sensitivity", "Mouse Sensitivity");
                IncreaseDecreaseDragFloat(ref CameraComponent_.Zoom, "Zoom", "Zoom");

                if (ImGui.Button("Set Active"))
                {
                    Entity.ComponentManager_.ActiveCamera = CameraComponent_;
                }
            }

        }
    }
}