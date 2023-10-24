using System.Numerics;
using Raylib_CsLo;

namespace CopperEngine.Utility;

public static class ModelUtil
{
    public static Model Load(string path) => Raylib.LoadModel(path);
    public static Model LoadFromMesh(Mesh mesh) => Raylib.LoadModelFromMesh(mesh);
    public static void Unload(Model model) => Raylib.UnloadModel(model);
    public static BoundingBox GetBoundingBox(Model model) => Raylib.GetModelBoundingBox(model);
    
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void SetMeshMaterial(Model* model, int meshId, int materialId) => Raylib.SetModelMeshMaterial(model, meshId, materialId);
    public static void SetMeshMaterial(ref Model model, int meshId, int materialId)
    {
        unsafe
        {
            fixed (Model* modelPtr = &model)
                Raylib.SetModelMeshMaterial(modelPtr, meshId, materialId);
        }
    }

    public static void DrawModel(Model model, Vector3 pos, float scale, Color color) => Raylib.DrawModel(model, pos, scale, color);
    public static void DrawModel(Model model, Vector3 pos, Vector3 rotationAxis, float rotationAngle, Vector3 scale, Color color) => Raylib.DrawModelEx(model, pos, rotationAxis, rotationAngle, scale, color);
    public static void DrawModelWires(Model model, Vector3 pos, float scale, Color color) => Raylib.DrawModelWires(model, pos, scale, color);
    public static void DrawModelWires(Model model, Vector3 pos, Vector3 rotationAxis, float rotationAngle, Vector3 scale, Color color) => Raylib.DrawModelWiresEx(model, pos, rotationAxis, rotationAngle, scale, color);
    public static void DrawBoundingBox(BoundingBox box, Color color) => Raylib.DrawBoundingBox(box, color);
    public static void DrawLine3D(Vector3 startPos, Vector3 endPos, Color color) => Raylib.DrawLine3D(startPos, endPos, color);
    public static void DrawPoint3D(Vector3 pos, Color color) => Raylib.DrawPoint3D(pos, color);
    public static void DrawCircle3D(Vector3 center, float radius, Vector3 rotationAxis, float rotationAngle, Color color) => Raylib.DrawCircle3D(center, radius, rotationAxis, rotationAngle, color);
    public static void DrawTriangle3D(Vector3 v1, Vector3 v2, Vector3 v3, Color color) => Raylib.DrawTriangle3D(v1, v2, v3, color);
    
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void DrawTriangleStrip3D(Vector3* points, int pointCount, Color color) => Raylib.DrawTriangleStrip3D(points, pointCount, color);
    public static void DrawTriangleStrip3D(ref Vector3 points, int pointCount, Color color)
    {
        unsafe
        {
            fixed (Vector3* pointsPtr = &points)
                Raylib.DrawTriangleStrip3D(pointsPtr, pointCount, color);
        }
    }

    public static void DrawCube(Vector3 pos, float width, float height, float length, Color color) => Raylib.DrawCube(pos, width, height, length, color);
    public static void DrawCube(Vector3 pos, Vector3 size, Color color) => Raylib.DrawCubeV(pos, size, color);
    public static void DrawCubeWires(Vector3 pos, float width, float height, float length, Color color) => Raylib.DrawCubeWires(pos, width, height, length, color);
    public static void DrawCubeWires(Vector3 pos, Vector3 size, Color color) => Raylib.DrawCubeWiresV(pos, size, color);
    public static void DrawSphere(Vector3 centerPos, float radius, Color color) => Raylib.DrawSphere(centerPos, radius, color);
    public static void DrawSphere(Vector3 centerPos, float radius, int rings, int slices, Color color) => Raylib.DrawSphereEx(centerPos, radius, rings, slices, color);
    public static void DrawSphereWires(Vector3 centerPos, float radius, int rings, int slices, Color color) => Raylib.DrawSphereWires(centerPos, radius, rings, slices, color);
    public static void DrawCylinder(Vector3 pos, float radiusTop, float radiusBottom, float height, int slices, Color color) => Raylib.DrawCylinder(pos, radiusTop, radiusBottom, height, slices, color);
    public static void DrawCylinder(Vector3 startPos, Vector3 endPos, float startRadius, float endRadius, int sides, Color color) => Raylib.DrawCylinderEx(startPos, endPos, startRadius, endRadius, sides, color);
    public static void DrawCylinderWires(Vector3 pos, float radiusTop, float radiusBottom, float height, int slices, Color color) => Raylib.DrawCylinderWires(pos, radiusTop, radiusBottom, height, slices, color);
    public static void DrawCylinderWires(Vector3 startPos, Vector3 endPos, float startRadius, float endRadius, int sides, Color color) => Raylib.DrawCylinderWiresEx(startPos, endPos, startRadius, endRadius, sides, color);
    public static void DrawPlane(Vector3 centerPos, Vector2 size, Color color) => Raylib.DrawPlane(centerPos, size, color);
    public static void DrawRay(Ray ray, Color color) => Raylib.DrawRay(ray, color);
    public static void DrawGrid(int slices, float spacing) => Raylib.DrawGrid(slices, spacing);
    
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe ModelAnimation* LoadAnimations(string path, uint* animCount) => Raylib.LoadModelAnimations(path, animCount);
    public static ModelAnimation LoadAnimations(string path, ref uint animCount)
    {
        unsafe
        {
            fixed (uint* animCountPtr = &animCount)
                return *Raylib.LoadModelAnimations(path, animCountPtr);
        }
    }

    public static void UpdateAnimation(Model model, ModelAnimation anim, int frame) => Raylib.UpdateModelAnimation(model, anim, frame);
    public static void UnloadAnimation(ModelAnimation anim) => Raylib.UnloadModelAnimation(anim);
    
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void UnloadAnimations(ModelAnimation* animations, uint count) => Raylib.UnloadModelAnimations(animations, count);
    public static void UnloadAnimations(ref ModelAnimation animations, uint count)
    {
        unsafe
        {
            fixed (ModelAnimation* animationsPtr = &animations)
                Raylib.UnloadModelAnimations(animationsPtr, count);
        }
    }

    public static bool IsAnimationValid(Model model, ModelAnimation anim) => Raylib.IsModelAnimationValid(model, anim);
    public static bool CheckCollisionSpheres(Vector3 center1, float radius1, Vector3 center2, float radius2) => Raylib.CheckCollisionSpheres(center1, radius1, center2, radius2);
    public static bool CheckCollisionBoxes(BoundingBox box1, BoundingBox box2) => Raylib.CheckCollisionBoxes(box1, box2);
    public static bool CheckCollisionBoxSphere(BoundingBox box, Vector3 center, float radius) => Raylib.CheckCollisionBoxSphere(box, center, radius);
    public static RayCollision GetRayCollisionSphere(Ray ray, Vector3 center, float radius) => Raylib.GetRayCollisionSphere(ray, center, radius);
    public static RayCollision GetRayCollisionBox(Ray ray, BoundingBox box) => Raylib.GetRayCollisionBox(ray, box);
    public static RayCollision GetRayCollisionMesh(Ray ray, Mesh mesh, Matrix4x4 transform) => Raylib.GetRayCollisionMesh(ray, mesh, transform);
    public static RayCollision GetRayCollisionTriangle(Ray ray, Vector3 p1, Vector3 p2, Vector3 p3) => Raylib.GetRayCollisionTriangle(ray, p1, p2, p3);
    public static RayCollision GetRayCollisionQuad(Ray ray, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4) => Raylib.GetRayCollisionQuad(ray, p1, p2, p3, p4);

}