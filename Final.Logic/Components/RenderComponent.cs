using System.Collections.Generic;
using GlmSharp;

namespace Final.Logic
{
    public class RenderComponent : Component
    {
        public Model Model_;
        private TransformComponent Transform;
        public List<Uniform<MyRef<int>>> UniformInts = new List<Uniform<MyRef<int>>>();
        public List<Uniform<MyRef<float>>> UniformFloats = new List<Uniform<MyRef<float>>>();
        public List<Uniform<MyRef<vec3>>> UniformVec3s = new List<Uniform<MyRef<vec3>>>(); 

        mat4 ModelMatrix 
        {
            get
            {
                mat4 translateMatrix = mat4.Translate(Transform.Translate);

                mat4 scaleMatrix = mat4.Scale(Transform.Scale);

                mat4 xRotation = mat4.RotateX(glm.Radians(Transform.Rotation.x));
                mat4 yRotation = mat4.RotateY(glm.Radians(Transform.Rotation.y));
                mat4 zRotation = mat4.RotateZ(glm.Radians(Transform.Rotation.z));

                mat4 rotationMatrix = mat4.Identity * xRotation * yRotation * zRotation;

                return mat4.Identity * translateMatrix * scaleMatrix * rotationMatrix;
            }
        }

        public RenderComponent() : base()
        {
            
        }

        public RenderComponent(Model model, TransformComponent transform) : base()
        {
            Model_ = model;
            Transform = transform;
        }

        public RenderComponent(TransformComponent transform)
        {
            Transform = transform;
        }

        public void Draw(mat4 viewMatrix, mat4 projectionMatrix)
        {
            Model_?.draw(ModelMatrix, viewMatrix, projectionMatrix, UniformInts, UniformFloats, UniformVec3s);
        }
    }
}