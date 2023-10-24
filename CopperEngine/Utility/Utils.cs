using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using ImGuizmoNET;
using Jitter2.Dynamics;
using Raylib_CsLo;

namespace CopperEngine.Utility;

public static class Utils
{
    private struct GridTextureData
    {
        public readonly int Width;
        public readonly int Height;
        public readonly Color Color1;
        public readonly Color Color2;

        public GridTextureData(int width, int height, Color color1, Color color2)
        {
            Width = width;
            Height = height;
            Color1 = color1;
            Color2 = color2;
        }
    }

    private static Texture GenerateGridTexture(GridTextureData data)
    {
        var img = ImageUtil.GenChecked(data.Width, data.Height, 1, 1, data.Color1, data.Color2);
        var gridTexture = TextureUtil.LoadFromImage(img);
        ImageUtil.Unload(img);
        TextureUtil.GenMipmaps(ref gridTexture);
        TextureUtil.SetFilter(gridTexture, TextureFilter.TEXTURE_FILTER_ANISOTROPIC_16X);
        TextureUtil.SetWrap(gridTexture, TextureWrap.TEXTURE_WRAP_CLAMP);
        return gridTexture;
    }

    public static Texture GridTexture(int width, int height, Color color1, Color color2)
    {
        var data = new GridTextureData(width, height, color1, color2);

        return GenerateGridTexture(data);
    }

    public static Texture GridTexture(int width, int height)
    {
        return GridTexture(width, height, ColorUtil.Gray, ColorUtil.LightGray);
    }

    public static Matrix4x4 GetRayLibTransformMatrix(RigidBody body)
    {
        var ori = body.Orientation;
        var pos = body.Position;

        return new Matrix4x4(ori.M11, ori.M12, ori.M13, pos.X,
            ori.M21, ori.M22, ori.M23, pos.Y,
            ori.M31, ori.M32, ori.M33, pos.Z,
            0, 0, 0, 1.0f);
    }

    public static Vector3 RandomAreaInSphere(Random random)
    {
        var xVal = (random.NextDouble() * 2) - 1;
        var yVal = (random.NextDouble() * 2) - 1;
        var zVal = (random.NextDouble() * 2) - 1;
        return Vector3.Normalize(new Vector3((float)xVal, (float)yVal, (float)zVal));
    }

    public static Vector3 Scale(Vector3 vector, float scale)
    {
        return Scale(vector, new Vector3(scale));
    }

    public static Vector3 Scale(Vector3 vec1, Vector3 vec2)
    {
        return new Vector3(vec1.X * vec2.X, vec1.Y * vec2.Y, vec1.Z * vec2.Z);
    }
    
    public static void DecomposeMatrixToComponents(Matrix4x4 matrix, ref Vector3 translation, ref Vector3 rotation, ref Vector3 scale)
    {
        unsafe
        {
            fixed (Vector3* pTrans = &translation, pRotate = &rotation, pScale = &scale)
            {
                ImGuizmo.DecomposeMatrixToComponents(
                    ref matrix.M11,
                    ref pTrans->X,
                    ref pRotate->X,
                    ref pScale->X
                );
            }
        }
    }
    
    public static Matrix4x4 RecomposeMatrixFromComponents(Vector3 translation, Vector3 rotation, Vector3 scale) 
    {
        var result = new Matrix4x4();
        ImGuizmo.RecomposeMatrixFromComponents
        (
            ref translation.X,
            ref rotation.X,
            ref scale.X,
            ref result.M11
        );
        return result;
    }

    public static Vector4 ToVector4(this Quaternion quaternion)
    {
        return new Vector4(quaternion.X, quaternion.Y, quaternion.Z, quaternion.W);
    }

    public static Quaternion ToQuaternion(this Vector4 vector4)
    {
        return new Quaternion(vector4.X, vector4.Y, vector4.Z, vector4.W);
    }

    public static Vector3 ToVector3(this Vector4 vector4)
    {
        return new Vector3(vector4.X, vector4.Y, vector4.Z);
    }

    public static Quaternion ToQuaternion(this Vector3 vector3)
    {
        return new Quaternion(vector3.X, vector3.Y, vector3.Z, 0);
    }

    public static string GetProjection(this Camera3D camera3D)
    {
        return camera3D.projection switch
        {
            (int)CameraProjection.CAMERA_PERSPECTIVE => "Perspective",
            (int)CameraProjection.CAMERA_ORTHOGRAPHIC => "Orthographic",
            _ => "Unset"
        };
    }
    
    // IntToBytes version that doesn't allocate a new byte[4] each time.
    // -> important for MMO scale networking performance.
    public static void IntToBytesBigEndianNonAlloc(int value, byte[] bytes, int offset = 0)
    {
        bytes[offset + 0] = (byte)(value >> 24);
        bytes[offset + 1] = (byte)(value >> 16);
        bytes[offset + 2] = (byte)(value >> 8);
        bytes[offset + 3] = (byte)value;
    }

    public static int BytesToIntBigEndian(byte[] bytes)
    {
        return (bytes[0] << 24) |
               (bytes[1] << 16) |
               (bytes[2] << 8) |
               bytes[3];
    }

    public static string CapitalizeFirstLetter(string message)
    {
        return message.Length switch
        {
            0 => "",
            1 => char.ToUpper(message[0]).ToString(),
            _ => char.ToUpper(message[0]) + message[1..]
        };
    }
}