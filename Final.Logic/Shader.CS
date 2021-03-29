using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GlmSharp;
using System;
using OpenTK.Mathematics;

namespace Final.Logic
{
    public class Shader
    {
        private uint ShaderProgram;
        private uint VertexShader;
        private uint FragmentShader;
        private string VertexShaderSource;
        private string FragmentShaderSource;
        private int [] infoLog = new int [1024];

        public Shader(string VertexPath, string FragmentPath)
        {
            VertexShaderSource = File.ReadAllText(VertexPath);
            FragmentShaderSource = File.ReadAllText(FragmentPath);

            VertexShader = (uint)GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource((int)VertexShader, VertexShaderSource);
            GL.CompileShader(VertexShader);
            checkCompileErrors(VertexShader, "VERTEX");
            

            FragmentShader = (uint)GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource((int)FragmentShader, FragmentShaderSource);
            GL.CompileShader(FragmentShader);
            checkCompileErrors(FragmentShader, "FRAGMENT");

            ShaderProgram = (uint)GL.CreateProgram();
            GL.AttachShader(ShaderProgram, VertexShader);
            GL.AttachShader(ShaderProgram, FragmentShader);
            GL.LinkProgram(ShaderProgram);

            GL.DeleteShader(VertexShader);
            GL.DeleteShader(FragmentShader);
        }

        public void use()
        {
            GL.UseProgram(ShaderProgram);
        }

        private void checkCompileErrors(uint shader, string type)
        {
            int [] success = new int [1];
            string infoLog;
            int Length = 1024;
            if (type != "PROGRAM")
            {
                GL.GetShader((int)shader, ShaderParameter.CompileStatus, success);
                if (success[0] == 0)
                {
                    GL.GetShaderInfoLog((int)shader, 1024, out Length, out infoLog);
                    System.Console.WriteLine($"ERROR::SHADER_COMPILATION_ERROR of type: {type}\n {infoLog}");
                }
            }
        }

        public void setInt(string name, int value)
        {
            int loc = GL.GetUniformLocation(ShaderProgram, name);
            GL.Uniform1(loc, value);
        }

        public void setFloat(string name, float value)
        {
            int loc = GL.GetUniformLocation(ShaderProgram, name);
            GL.Uniform1(loc, value);
        }

        public void setMat4(string name, mat4 mat)
        {
            int loc = GL.GetUniformLocation(ShaderProgram, name);
            GL.UniformMatrix4(loc, 1, false, mat.Values1D);
        }

        public void setVec3(string name, vec3 value)
        {
            int loc = GL.GetUniformLocation(ShaderProgram, name);
            GL.Uniform3(loc, value.x, value.y, value.z);
        }

        public void setVec3(string name, int count, float [] value)
        {
            int loc = GL.GetUniformLocation(ShaderProgram, name);
            OpenTK.Graphics.OpenGL.GL.Uniform3(loc, count, value);
        }

        public static vec3 NormalizedRGB(float r, float g, float b)
        {
            return new vec3(r / 255, g / 255, b / 255);
        }
    }
}