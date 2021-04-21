using System.Numerics;
using ImGuiNET;

namespace Final.Logic
{
    public class ShaderWidget : ImGuiWidget
    {
        ShaderComponent ShaderComponent_;
        string vertexFileBuffer;
        string fragmentFileBuffer;

        public ShaderWidget(ShaderComponent  shaderComponent)
        {
            ShaderComponent_ = shaderComponent;
            vertexFileBuffer = ShaderComponent_.VertexFile;
            fragmentFileBuffer = ShaderComponent_.FragmentFile;
        }

        public override void Run()
        {
            ImGui.PushItemWidth(250);
            if (ImGui.CollapsingHeader("Shader"))
            {
                if (ImGui.InputText("Vertex", ref vertexFileBuffer, 200))
                {
                    ShaderComponent_.VertexFile = vertexFileBuffer;
                }
                if (ImGui.InputText("Fragment", ref fragmentFileBuffer, 200))
                {
                    ShaderComponent_.FragmentFile = fragmentFileBuffer;
                }
                if (ImGui.Button("Change"))
                {
                    try
                    {
                        ShaderComponent_.RenderComponent_.Model_.Shader_ = new Shader(ShaderComponent_.VertexFile, ShaderComponent_.FragmentFile);
                        ErrorState = false;
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