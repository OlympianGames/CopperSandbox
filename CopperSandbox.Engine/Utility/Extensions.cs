using System.Numerics;
using Jitter2.Dynamics;
using Jitter2.LinearMath;
using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

namespace CopperSandbox.Engine.Utility;

public static class Extensions
{
    public static void WithTexture(this ref Material material, Texture texture)
    {
        unsafe
        {
            material.maps[(int)MATERIAL_MAP_DIFFUSE].texture = texture;
        }
    }

    public static void WithTexture(this ref Model model, Texture texture)
    {
        unsafe
        {
            model.materials[0].WithTexture(texture);
        }
    }

    public static void WithGridTexture(this ref Model model, int width, int height) => model.WithTexture(Utils.GridTexture(width, height));
    public static void WithGridTexture(this ref Model model, int width, int height, Color color1, Color color2) => model.WithTexture(Utils.GridTexture(width, height, color1, color2));
    public static void WithGridTexture(this ref Material material, int width, int height) => material.WithTexture(Utils.GridTexture(width, height));
    public static void WithGridTexture(this ref Material material, int width, int height, Color color1, Color color2) => material.WithTexture(Utils.GridTexture(width, height, color1, color2));
    public static Matrix4x4 GetTransformMatrix(this RigidBody body) => Utils.GetRayLibTransformMatrix(body);
    public static Matrix4x4 GetRayLibTransformMatrix(this RigidBody body) => Utils.GetRayLibTransformMatrix(body);
    public static Vector3 AreaInSphere(this Random random) => Utils.RandomAreaInSphere(random);
    public static Vector3 ToVector(this JVector jVector) => new(jVector.X, jVector.Y, jVector.Z);
    public static JVector ToJVector(this Vector3 vector) => new(vector.X, vector.Y, vector.Z);
    public static Vector3 Scale(this Vector3 vector, float scale) => Utils.Scale(vector, scale);
    public static Vector3 Scale(this Vector3 vec1, Vector3 vec2) => Utils.Scale(vec1, vec2);
    public static Vector3 WithX(this Vector3 vector, float value) => vector with { X = value };
    public static Vector3 WithY(this Vector3 vector, float value) => vector with { Y = value };
    public static Vector3 WithZ(this Vector3 vector, float value) => vector with { Z = value };
    public static Vector3 Clamp(this Vector3 value, Vector3 min, Vector3 max) => MathUtil.Clamp(value, min, max);
    public static string ToFancyString(this IEnumerable<byte> array) => array.Aggregate("", (current, item) => current + $"<{item}>,");

    public static string CapitalizeFirstLetter(this string message) => Utils.CapitalizeFirstLetter(message);
}