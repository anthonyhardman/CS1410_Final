using System.Collections.Generic;
using ImGuiNET;
using GlmSharp;

namespace Final.Logic
{
    public class MaterialWidget : ImGuiWidget
    {
        MaterialComponent MaterialComponent_;
        private string MaterialPreset = "Custom";

        public MaterialWidget(RenderComponent renderComponent, MaterialComponent materialComponent)
        {
            MaterialComponent_ = materialComponent;
        }

        private List<string> MaterialNames = new List<string>()
        {
            "Emerald", "Jade", "Obsidian", "Pearl", "Ruby", "Turquoise", "Brass", "Bronze",
            "Chrome", "Copper", "Gold", "Silver", "Black Plastic", "Cyan Plastic", "Green Plastic",
            "Red Plastic", "White Plastic", "Yellow Plastic", "Black Rubber", "Cyan Rubber",
            "Green Rubber", "Red Rubber", "White Rubber", "Yellow Rubber"
        };

        public override void Run()
        {
            if (ImGui.CollapsingHeader("Material"))
            {
                if (ImGui.BeginCombo("Presets", MaterialPreset))
                {
                    foreach (string material in MaterialNames)
                    {
                        if (ImGui.Selectable(material))
                        {
                            MaterialPreset = material;
                        }
                    }
                    ImGui.EndCombo();
                }

                if (MaterialPreset != "Custom")
                {
                    switch (MaterialPreset)
                    {
                        case "Emerald":
                            MaterialComponent_.Ambient = new vec3(0.0215f, 0.1245f, 0.0215f);
                            MaterialComponent_.Diffuse = new vec3(0.07568f, 0.61424f, 0.07568f);
                            MaterialComponent_.Specular = new vec3(0.633f, 0.727811f, 0.633f);
                            MaterialComponent_.Shininess = 0.6f * 128;
                            break;
                        case "Jade":
                            MaterialComponent_.Ambient = new vec3(0.135f, 0.2225f, 0.1575f);
                            MaterialComponent_.Diffuse = new vec3(0.54f, 0.89f, 0.63f);
                            MaterialComponent_.Specular = new vec3(0.316228f, 0.316228f, 0.316228f);
                            MaterialComponent_.Shininess = 0.1f * 128;
                            break;
                        case "Obsidian":
                            MaterialComponent_.Ambient = new vec3(0.05375f, 0.05f, 0.06625f);
                            MaterialComponent_.Diffuse = new vec3(0.18275f, 0.17f, 0.22525f);
                            MaterialComponent_.Specular = new vec3(0.332741f, 0.328634f, 0.346435f);
                            MaterialComponent_.Shininess = 0.3f * 128;
                            break;
                        case "Pearl":
                            MaterialComponent_.Ambient = new vec3(0.25f, 0.20725f, 0.20725f);
                            MaterialComponent_.Diffuse = new vec3(1.0f, 0.829f, 0.829f);
                            MaterialComponent_.Specular = new vec3(0.296648f, 0.296648f, 0.296648f);
                            MaterialComponent_.Shininess = 0.088f * 128;
                            break;
                        case "Ruby":
                            MaterialComponent_.Ambient = new vec3(0.1745f, 0.01175f, 0.01175f);
                            MaterialComponent_.Diffuse = new vec3(0.61424f, 0.04136f, 0.04136f);
                            MaterialComponent_.Specular = new vec3(0.727811f, 0.626959f, 0.626959f);
                            MaterialComponent_.Shininess = 0.6f * 128;
                            break;
                        case "Turquoise":
                            MaterialComponent_.Ambient = new vec3(0.1f, 0.18725f, 0.1745f);
                            MaterialComponent_.Diffuse = new vec3(0.396f, 0.74151f, 0.69102f);
                            MaterialComponent_.Specular = new vec3(0.297254f, 0.30829f, 0.306678f);
                            MaterialComponent_.Shininess = 0.1f * 128;
                            break;
                        case "Brass":
                            MaterialComponent_.Ambient = new vec3(0.329412f, 0.223529f, 0.027451f);
                            MaterialComponent_.Diffuse = new vec3(0.780392f, 0.568627f, 0.113725f);
                            MaterialComponent_.Specular = new vec3(0.992157f, 0.941176f, 0.807843f);
                            MaterialComponent_.Shininess = 0.21794872f * 128;
                            break;
                        case "Bronze":
                            MaterialComponent_.Ambient = new vec3(0.2125f, 0.1275f, 0.054f);
                            MaterialComponent_.Diffuse = new vec3(0.714f, 0.4284f, 0.18144f);
                            MaterialComponent_.Specular = new vec3(0.393548f, 0.271906f, 0.166721f);
                            MaterialComponent_.Shininess = 0.2f * 128;
                            break;
                        case "Chrome":
                            MaterialComponent_.Ambient = new vec3(0.25f, 0.25f, 0.25f);
                            MaterialComponent_.Diffuse = new vec3(0.4f, 0.4f, 0.4f);
                            MaterialComponent_.Specular = new vec3(0.774597f, 0.774597f, 0.774597f);
                            MaterialComponent_.Shininess = 0.6f * 128;
                            break;
                        case "Copper":
                            MaterialComponent_.Ambient = new vec3(0.19125f, 0.0735f, 0.0225f);
                            MaterialComponent_.Diffuse = new vec3(0.7038f, 0.27048f, 0.0828f);
                            MaterialComponent_.Specular = new vec3(0.256777f, 0.137622f, 0.086014f);
                            MaterialComponent_.Shininess = 0.1f * 128;
                            break;
                        case "Gold":
                            MaterialComponent_.Ambient = new vec3(0.24725f, 0.1995f, 0.0745f);
                            MaterialComponent_.Diffuse = new vec3(0.75164f, 0.60648f, 0.22648f);
                            MaterialComponent_.Specular = new vec3(0.628281f, 0.55580f, 0.366065f);
                            MaterialComponent_.Shininess = 0.4f * 128;
                            break;
                        case "Silver":
                            MaterialComponent_.Ambient = new vec3(0.19225f, 0.19225f, 0.19225f);
                            MaterialComponent_.Diffuse = new vec3(0.50754f, 0.50754f, 0.50754f);
                            MaterialComponent_.Specular = new vec3(0.508273f, 0.508273f, 0.508273f);
                            MaterialComponent_.Shininess = 0.4f * 128;
                            break;
                        case "Black Plastic":
                            MaterialComponent_.Ambient = new vec3(0.0f, 0.0f, 0.0f);
                            MaterialComponent_.Diffuse = new vec3(0.01f, 0.01f, 0.01f);
                            MaterialComponent_.Specular = new vec3(0.50f, 0.50f, 0.50f);
                            MaterialComponent_.Shininess = 0.25f * 128;
                            break;
                        case "Cyan Plastic":
                            MaterialComponent_.Ambient = new vec3(0.0f, 0.1f, 0.06f);
                            MaterialComponent_.Diffuse = new vec3(0.0f, 0.50980392f, 0.50980392f);
                            MaterialComponent_.Specular = new vec3(0.50196078f, 0.50196078f, 0.50196078f);
                            MaterialComponent_.Shininess = 0.25f * 128;
                            break;
                        case "Green Plastic":
                            MaterialComponent_.Ambient = new vec3(0.0f, 0.0f, 0.0f);
                            MaterialComponent_.Diffuse = new vec3(0.1f, 0.35f, 0.1f);
                            MaterialComponent_.Specular = new vec3(0.45f, 0.55f, 0.45f);
                            MaterialComponent_.Shininess = 0.25f * 128;
                            break;
                        case "Red Plastic":
                            MaterialComponent_.Ambient = new vec3(0.0f, 0.0f, 0.0f);
                            MaterialComponent_.Diffuse = new vec3(0.5f, 0.0f, 0.0f);
                            MaterialComponent_.Specular = new vec3(0.7f, 0.6f, 0.6f);
                            MaterialComponent_.Shininess = 0.25f * 128;
                            break;
                        case "White Plastic":
                            MaterialComponent_.Ambient = new vec3(0.0f, 0.0f, 0.0f);
                            MaterialComponent_.Diffuse = new vec3(0.55f, 0.55f, 0.55f);
                            MaterialComponent_.Specular = new vec3(0.70f, 0.70f, 0.70f);
                            MaterialComponent_.Shininess = 0.25f * 128;
                            break;
                        case "Yellow Plastic":
                            MaterialComponent_.Ambient = new vec3(0.0f, 0.0f, 0.0f);
                            MaterialComponent_.Diffuse = new vec3(0.5f, 0.5f, 0.0f);
                            MaterialComponent_.Specular = new vec3(0.60f, 0.60f, 0.50f);
                            MaterialComponent_.Shininess = 0.25f * 128;
                            break;
                        case "Black Rubber":
                            MaterialComponent_.Ambient = new vec3(0.02f, 0.02f, 0.02f);
                            MaterialComponent_.Diffuse = new vec3(0.01f, 0.01f, 0.01f);
                            MaterialComponent_.Specular = new vec3(0.4f, 0.4f, 0.4f);
                            MaterialComponent_.Shininess = 0.078125f * 128;
                            break;
                        case "Cyan Rubber":
                            MaterialComponent_.Ambient = new vec3(0.0f, 0.05f, 0.05f);
                            MaterialComponent_.Diffuse = new vec3(0.4f, 0.5f, 0.5f);
                            MaterialComponent_.Specular = new vec3(0.04f, 0.7f, 0.7f);
                            MaterialComponent_.Shininess = 0.078125f * 128;
                            break;
                        case "Green Rubber":
                            MaterialComponent_.Ambient = new vec3(0.0f, 0.05f, 0.0f);
                            MaterialComponent_.Diffuse = new vec3(0.4f, 0.5f, 0.4f);
                            MaterialComponent_.Specular = new vec3(0.04f, 0.7f, 0.04f);
                            MaterialComponent_.Shininess = 0.078125f * 128;
                            break;
                        case "Red Rubber":
                            MaterialComponent_.Ambient = new vec3(0.05f, 0.0f, 0.0f);
                            MaterialComponent_.Diffuse = new vec3(0.5f, 0.4f, 0.4f);
                            MaterialComponent_.Specular = new vec3(0.7f, 0.04f, 0.04f);
                            MaterialComponent_.Shininess = 0.078125f * 128;
                            break;
                        case "White Rubber":
                            MaterialComponent_.Ambient = new vec3(0.05f, 0.05f, 0.05f);
                            MaterialComponent_.Diffuse = new vec3(0.5f, 0.5f, 0.5f);
                            MaterialComponent_.Specular = new vec3(0.7f, 0.7f, 0.7f);
                            MaterialComponent_.Shininess = 0.078125f * 128;
                            break;
                        case "Yellow Rubber":
                            MaterialComponent_.Ambient = new vec3(0.05f, 0.05f, 0.0f);
                            MaterialComponent_.Diffuse = new vec3(0.5f, 0.5f, 0.4f);
                            MaterialComponent_.Specular = new vec3(0.7f, 0.7f, 0.04f);
                            MaterialComponent_.Shininess = 0.078125f * 128;
                            break;

                    }
                }

                ImGui.PushItemWidth(100);
                ImGui.Text("Ambient");
                if (MyColorPicker(ref MaterialComponent_.Ambient, "ambient"))
                {
                    MaterialPreset = "Custom";
                }
                ImGui.Text("Diffuse");
                if (MyColorPicker(ref MaterialComponent_.Diffuse, "diffuse"))
                {
                    MaterialPreset = "Custom";
                }
                ImGui.Text("Specular");
                if (MyColorPicker(ref MaterialComponent_.Specular, "specular"))
                {
                    MaterialPreset = "Custom";
                }
                ImGui.Text("Shininess");
                if (IncreaseDecreaseDragFloat(ref MaterialComponent_.Shininess, "", "shininess"))
                {
                    MaterialPreset = "Custom";
                }
            }
        }
    }
}