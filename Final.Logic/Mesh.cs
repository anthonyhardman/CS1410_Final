using System;
using System.Collections.Generic;
using GlmSharp;
using OpenTK.Graphics.OpenGL4;

namespace Final.Logic
{
    public class Mesh
    {
        public List<Vertex> Vertices;
        public List<uint> Indices;
        public Texture [] Textures;

        uint VAO, VBO, EBO;

        public Mesh(List<Vertex> vertices, List<uint> indices, Texture [] textures)
        {
            Vertices = vertices;
            Indices = indices;
            Textures = textures;
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

            GL.BindVertexArray(VAO);
            GL.DrawElements(BeginMode.Triangles, Indices.Count, DrawElementsType.UnsignedInt, 0);
        }
    }
}