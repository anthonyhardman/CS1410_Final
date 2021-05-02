using System.Collections.Generic;
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

        private List<string> MaterialNames = new List<string>()
        {
            "Emerald", "Jade", "Obsidian", "Pearl" 
        };

        public override void Run()
        {
            string Material;
            if (ImGui.CollapsingHeader("Material"))
            {
                if (ImGui.BeginCombo("Presets", "Custom"))
                {
                    foreach(string material in MaterialNames)
                    {
                        if (ImGui.Selectable(material))
                        {
                            Material = material;
                        }
                    }
                    ImGui.EndCombo();
                }

                

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