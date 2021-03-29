using GlmSharp;

namespace Final.Logic
{
    public class GUI
    {
        
    }

    public class GuiWindow : Entity
    {
        GuiWindow Parent;
        float Left;

        public GuiWindow(vec3 scale, GuiAlignment alignment, GuiWindow parent) : base()
        {
            AddComponent<TransformComponent>();
            AddComponent<RenderComponent>();

            GetComponent<RenderComponent>().Model_ = new Model("Models\\GuiWindow.dae", new Shader("Shaders\\GuiWindowVert.glsl", "Shaders\\GuiWindowFrag.glsl"));
            
            Parent = parent;

            if (Parent != null)
            {
                scale = scale * parent.GetComponent<TransformComponent>().Scale;
            }

            switch(alignment)
            {
                case GuiAlignment.LEFT:
                if (Parent != null)
                {
                    Left = parent.GetComponent<TransformComponent>().Scale.x;
                }
                else
                {
                    Left = -1.0f;
                }
                break;
                
                case GuiAlignment.RIGHT:
                if (Parent != null)
                {
                    Left = parent.Left + scale.x;
                }
                else
                {
                    Left = -1.0f + 2.0f - scale.x;
                }
                break;

                case GuiAlignment.CENTER:
                if (Parent != null)
                {
                    Left = parent.Left;
                }
                else
                {
                    Left = -1.0f + 2.0f - scale.x;
                }
                break;
            }

            // switch(alignment)
            // {
            //     case GuiAlignment.LEFT:
            //     GetComponent<TransformComponent>().Translate.x = Left;
            //     break;
                
            //     case GuiAlignment.RIGHT:
            //     GetComponent<TransformComponent>().Translate.x = 1.0f - scale.x;
            //     break;

            //     case GuiAlignment.CENTER:
            //     GetComponent<TransformComponent>().Translate.x = 0.0f;
            //     break;
            // }
            
            GetComponent<TransformComponent>().Translate.x = Left;
            GetComponent<TransformComponent>().Scale = scale;
        }
    }

    public enum GuiAlignment
    {
        LEFT,
        RIGHT,
        CENTER
    }
}