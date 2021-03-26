using StbImageSharp;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System;

namespace Final.Logic
{
    public class Texture
    {
        public uint ID;
        public string Name;

        public Texture(string imagePath, string name)
        {
            ID = (uint)GL.GenTexture();
            Name = name;
            byte [] buffer = File.ReadAllBytes(imagePath);
            StbImageSharp.StbImage.stbi_set_flip_vertically_on_load(1);
            ImageResult image = StbImageSharp.ImageResult.FromMemory(buffer, ColorComponents.RedGreenBlueAlpha);
            GL.BindTexture(TextureTarget.Texture2D, ID);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }
    }
}