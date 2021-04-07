using System;
using System.Collections.Generic;
using GlmSharp;
using OpenTK.Graphics.OpenGL4;

namespace Final.Logic
{
    public class Mesh
    {
        public static Camera Camera_;
        public static List<LightComponent> Lights;
        public List<Vertex> Vertices;
        public List<uint> Indices;
        public Texture [] Textures;
        public Material Material_;

        uint VAO, VBO, EBO;

        static Mesh()
        {
            Lights = new List<LightComponent>();
        }

        public Mesh(List<Vertex> vertices, List<uint> indices, Material material, Texture [] textures)
        {
            Vertices = vertices;
            Indices = indices;
            Textures = textures;
            Material_ = material;
            Setup();
        }

        public void Setup()
        {
            VAO = (uint)GL.GenVertexArray();
            VBO = (uint)GL.GenBuffer();
            EBO = (uint)GL.GenBuffer();

            GL.BindVertexArray(VAO);
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Count * 8 * sizeof(float), Vertices.ToArray(), BufferUsageHint.StaticDraw);
            
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Count * sizeof(uint), Indices.ToArray(), BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), (IntPtr)0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), (IntPtr)(3 * sizeof(float)));
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), (IntPtr)(6 * sizeof(float)));
            GL.EnableVertexAttribArray(2);
        }

        public void Draw(mat4 modelMatix, mat4 viewMatrix, mat4 projectionMatrix, 
                         Shader shader, List<Uniform<MyRef<int>>> uniformInts, List<Uniform<MyRef<float>>> uniformFloats)
        {
            shader.use();
            for (int i = 0; i < Textures.Length; ++i)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + i);
                GL.BindTexture(TextureTarget.Texture2D, Textures[i].ID);
                shader.setInt(Textures[i].Name, i);
            }
            GL.ActiveTexture(TextureUnit.Texture0);

            foreach (Uniform<MyRef<int>> uniform in uniformInts)
            {
                shader.setInt(uniform.Name, uniform.Value.Val);
            }

            foreach (Uniform<MyRef<float>> uniform in uniformFloats)
            {
                shader.setFloat(uniform.Name, uniform.Value.Val);
            }

            shader.setMat4("model", modelMatix);
            shader.setMat4("view", viewMatrix);
            shader.setMat4("projection", projectionMatrix);

            shader.setVec3("viewPos", Camera_.GetComponent<TransformComponent>().Translate);

            if (Material_ != null)
            {
                shader.setVec3("material.ambient", Material_.Ambient);
                shader.setVec3("material.diffuse", Material_.Diffuse);
                shader.setVec3("material.specular", Material_.Specular);
                shader.setFloat("material.shininess", Material_.Shininess);
            }
            // if (Light_ != null)
            // {
            //     shader.setVec3("light.position", Light_.Position);
            //     shader.setVec3("light.ambient", Light_.Ambient);
            //     shader.setVec3("light.diffuse", Light_.Diffuse);
            //     shader.setVec3("light.specular", Light_.Specular);
            // }

            
            int count = 0;

            foreach (LightComponent light in Lights)
            {
                shader.setVec3($"lights[{count}].position", light.TransformComponent_.Translate);
                shader.setVec3($"lights[{count}].ambient", light.Ambient);
                shader.setVec3($"lights[{count}].diffuse", light.Diffuse);
                shader.setVec3($"lights[{count}].specular", light.Specular);

                ++count;
            }

            Console.WriteLine(Lights.Count);

            shader.setInt("lightCount", Lights.Count);

            GL.BindVertexArray(VAO);
            GL.DrawElements(BeginMode.Triangles, Indices.Count, DrawElementsType.UnsignedInt, 0);
        }
    }
}