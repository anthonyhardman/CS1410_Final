using GlmSharp;
using ImGuiNET;

namespace Final.Logic
{
    public abstract class ImGuiWidget : IImguiWidget
    {
        public string ErrorText = "";
        public bool ErrorState = false;
        public static void IncreaseDecreaseDragFloat(ref float value, string label, string id)
        {
            ImGui.PushItemWidth(100);
            ImGui.Button("-");
            if (ImGui.IsItemActive())
            {
                value -= 0.01f;
            }
            ImGui.SameLine();
            ImGui.Button("+");
            if (ImGui.IsItemActive())
            {
                value += 0.01f;
            }
            ImGui.SameLine();
            ImGui.PushID(id);
            ImGui.SameLine();
            ImGui.DragFloat(label, ref value, 0.01f);
        }

        public static void MyColorPicker(ref vec3 color, string id)
        {
            System.Numerics.Vector3 tempVec = new System.Numerics.Vector3();
            tempVec.X = color.x;
            tempVec.Y = color.y;
            tempVec.Z = color.z;
            
            ImGui.PushID(id);
            if (ImGui.ColorPicker3("Color", ref tempVec))
            {
                color.x = tempVec.X;
                color.y = tempVec.Y;
                color.z = tempVec.Z;
            }
        }
        public virtual void Run()
        {
            throw new System.NotImplementedException();
        }
    }
}