using GlmSharp;
using ImGuiNET;

namespace Final.Logic
{
    public abstract class ImGuiWidget : IImguiWidget
    {
        public string ErrorText = "";
        public bool ErrorState = false;
        public static bool IncreaseDecreaseDragFloat(ref float value, string label, string id)
        {
            bool changed = false;

            ImGui.PushItemWidth(100);
            ImGui.Button("-");
            if (ImGui.IsItemActive())
            {
                value -= 0.01f;
                changed = true;
            }
            ImGui.SameLine();
            ImGui.Button("+");
            if (ImGui.IsItemActive())
            {
                value += 0.01f;
                changed = true;
            }
            ImGui.SameLine();
            ImGui.PushID(id);
            ImGui.SameLine();

            if (!changed)
            {
                changed = ImGui.DragFloat(label, ref value, 0.01f);
            }
            else
            {
                ImGui.DragFloat(label, ref value, 0.01f);
            }

            return changed;
        }

        public static bool MyColorPicker(ref vec3 color, string id)
        {
            bool changed = false;

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

                changed = true;
            }

            return changed;
        }
        public virtual void Run()
        {
            throw new System.NotImplementedException();
        }
    }
}