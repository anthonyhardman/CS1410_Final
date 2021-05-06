using System;
using System.Collections.Generic;
using GlmSharp;

namespace Final.Logic
{
    public class Model
    {
        public List<Mesh> Meshes = new List<Mesh>();
        public Shader Shader_;
        Texture [] Textures;
        public string File;
        public MaterialComponent MaterialComponent_;

        public Model(string file, Shader shader, params Texture [] textures)
        {
            Shader_ = shader;
            Textures = textures;
            File = file;
            LoadModel(file);
        }

        public void LoadModel(string file)
        {
            Assimp.AssimpContext importer = new Assimp.AssimpContext();
            Assimp.Scene scene = importer.ImportFile(file, Assimp.PostProcessSteps.Triangulate 
                                                     | Assimp.PostProcessSteps.FlipUVs 
                                                     | Assimp.PostProcessSteps.CalculateTangentSpace);
            
            ProcessNode(scene.RootNode, scene);
        }

        public void ProcessNode(Assimp.Node rootNode, Assimp.Scene scene)
        {
            for (int i = 0; i < rootNode.MeshCount; ++i)
            {
               Assimp.Mesh mesh = scene.Meshes[rootNode.MeshIndices[i]];
               Meshes.Add(ProcessMesh(mesh, scene));
            }

            for (int i = 0; i < rootNode.ChildCount; ++i)
            {
                ProcessNode(rootNode.Children[i], scene);
            }
        }

        public Mesh ProcessMesh(Assimp.Mesh mesh, Assimp.Scene scene)
        {
            List<Vertex> vertices = new List<Vertex>();
            List<uint> indices = new List<uint>();

            for (int i = 0; i < mesh.VertexCount; ++i)
            {
                Vertex vertex = new Vertex();
                vec3 vector =  new vec3();
                // Positions
                vector.x = mesh.Vertices[i].X;
                vector.y = mesh.Vertices[i].Y;
                vector.z = mesh.Vertices[i].Z;
                vertex.Position = vector;

                // Normals
                if (mesh.HasNormals)
                {
                    vector.x = mesh.Normals[i].X;
                    vector.y = mesh.Normals[i].Y;
                    vector.z = mesh.Normals[i].Z;
                    vertex.Normal = vector;
                }

                // Texture Coordinates
                if (mesh.HasTextureCoords(0))
                {
                    vec2 vec;
                    vec.x = mesh.TextureCoordinateChannels[0][i].X;
                    vec.y = mesh.TextureCoordinateChannels[0][i].Y;
                    vertex.TextureCoordinate = vec;
                }
                else
                {
                    vertex.TextureCoordinate = new vec2(0.0f, 0.0f);
                }

                vertices.Add(vertex);
            }
            for (int i = 0; i < mesh.FaceCount; ++i)
            {
                Assimp.Face face = mesh.Faces[i];

                for (int j = 0; j < face.IndexCount; ++j)
                {
                    indices.Add((uint)face.Indices[j]);
                }
            }

            return new Mesh(vertices, indices, MaterialComponent_, Textures);
        }

        public void draw(mat4 modelMatrix, mat4 viewMatrix, mat4 projectionMatrix, vec3 cameraViewPosition,List<Uniform<MyRef<int>>> uniformInts, List<Uniform<MyRef<float>>> uniformFloats, List<Uniform<MyRef<vec3>>> uniformVec3s)
        {
            foreach (Mesh mesh in Meshes)
            {
                mesh.Draw(modelMatrix, viewMatrix, projectionMatrix, cameraViewPosition, Shader_, uniformInts, uniformFloats, uniformVec3s, MaterialComponent_);
            }
        }
    }
}