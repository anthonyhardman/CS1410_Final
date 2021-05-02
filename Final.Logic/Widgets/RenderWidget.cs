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

            if (RenderComponent_.Model_ != null)
            {
                textBuffer = RenderComponent_.Model_.File;
            }
            else
            {
                textBuffer = "";
            }
        }

        public override void Run()
        {
            if (textBuffer == "")
            {
                textBuffer = RenderComponent_.Model_.File;
            }

            ImGui.PushItemWidth(250);
            ImGui.PushID("Model");
            if (ImGui.CollapsingHeader("Model"))
            {
                if (ImGui.InputText("", ref textBuffer, 200))
                {
                    RenderComponent_.Model_.File = textBuffer;
                }
                if (ImGui.Button("Change"))
                {
                    try
                    {
                        MaterialComponent material = RenderComponent_.Model_.MaterialComponent_;
                        RenderComponent_.Model_ = new Model(textBuffer, new Shader("Shaders\\basicLightingVert.glsl", "Shaders\\basicLightingFrag.glsl"));
                        RenderComponent_.Model_.MaterialComponent_ = material;
                        ErrorState = false;
                        ErrorText = "";
                    }
                    catch(System.IO.FileNotFoundException e)
                    {
                        ErrorText = e.Message;
                        ErrorState = true;
                    }
                    catch(System.IO.DirectoryNotFoundException e)
                    {
                        ErrorText = e.Message;
                        ErrorState = true;
                    }
                }
                if (ErrorState)
                {
                    ImGui.TextWrapped(ErrorText);
                }
            }
        }
    }
}