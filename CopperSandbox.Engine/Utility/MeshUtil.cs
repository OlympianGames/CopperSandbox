using System.Numerics;
using Raylib_CsLo;

namespace CopperSandbox.Engine.Utility;

public static class MeshUtil
{
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void Upload(Mesh* mesh, bool dynamic) => Raylib.UploadMesh(mesh, dynamic);
    public static void Upload(ref Mesh mesh, bool dynamic)
    {
        unsafe
        {
            fixed (Mesh* meshPtr = &mesh)
                Raylib.UploadMesh(meshPtr, dynamic);
        }
    }

    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void UpdateBuffer(Mesh mesh, int index, void* data, int dataSize, int offset) => Raylib.UpdateMeshBuffer(mesh, index, data, dataSize, offset);
    public static void UpdateBuffer(Mesh mesh, int index, ref object data, int dataSize, int offset)
    {
        unsafe
        {
            fixed (void* dataPtr = &data)
                Raylib.UpdateMeshBuffer(mesh, index, dataPtr, dataSize, offset);
        }
    }

    public static void Unload(Mesh mesh) => Raylib.UnloadMesh(mesh);
    public static bool Export(Mesh mesh, string path) => Raylib.ExportMesh(mesh, path);
    public static BoundingBox GetBoundingBox(Mesh mesh) => Raylib.GetMeshBoundingBox(mesh);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void GenTangents(Mesh* mesh) => Raylib.GenMeshTangents(mesh);
    public static void GenTangents(ref Mesh mesh)
    {
        unsafe
        {
            fixed (Mesh* meshPtr = &mesh)
                Raylib.GenMeshTangents(meshPtr);
        }
    }

    public static void Draw(Mesh mesh, Material material, Matrix4x4 transform) => Raylib.DrawMesh(mesh, material, transform);
    public static void DrawInstanced(Mesh mesh, Material material, Matrix4x4[] transforms, int instances) => Raylib.DrawMeshInstanced(mesh, material, transforms, instances);
    public static Mesh GenPoly(int sides, float radius) => Raylib.GenMeshPoly(sides, radius);
    public static Mesh GenPlane(float width, float length, int resX, int resZ) => Raylib.GenMeshPlane(width, length, resX, resZ);
    public static Mesh GenCube(float width, float height, float length) => Raylib.GenMeshCube(width, height, length);
    public static Mesh GenCube(float size) => Raylib.GenMeshCube(size, size, size);
    public static Mesh GenCube(Vector3 size) => Raylib.GenMeshCube(size.X, size.Y, size.Z);
    public static Mesh GenSphere(float radius, int rings, int slices) => Raylib.GenMeshSphere(radius, rings, slices);
    public static Mesh GenHemiSphere(float radius, int rings, int slices) => Raylib.GenMeshHemiSphere(radius, rings, slices);
    public static Mesh GenCylinder(float radius, float height, int slices) => Raylib.GenMeshCylinder(radius, height, slices);
    public static Mesh GenCone(float radius, float height, int slices) => Raylib.GenMeshCone(radius, height, slices);
    public static Mesh GenTorus(float radius, float size, int radSeg, int sides) => Raylib.GenMeshTorus(radius, size, radSeg, sides);
    public static Mesh GenKnot(float radius, float size, int radSeg, int sides) => Raylib.GenMeshKnot(radius, size, radSeg, sides);
    public static Mesh GenHeightmap(Image heightMap, Vector3 size) => Raylib.GenMeshHeightmap(heightMap, size);
    public static Mesh GenCubicMap(Image cubicMap, Vector3 cubeSize) => Raylib.GenMeshCubicmap(cubicMap, cubeSize);

}