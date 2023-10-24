using Raylib_CsLo;

namespace CopperEngine.Utility;

public static class MaterialUtil
{
    public static Material LoadDefault() => Raylib.LoadMaterialDefault();
    
    
    public static Material LoadMaterials(ref sbyte fileName, ref int materialCount)
    {
        unsafe
        {
            fixed (sbyte* fileNamePtr = &fileName)
            fixed (int* materialCountPtr = &materialCount)
                return *Raylib.LoadMaterials(fileNamePtr, materialCountPtr);
        }
    }
    
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe Material* LoadMaterials(sbyte* fileName, int* materialCount) => Raylib.LoadMaterials(fileName, materialCount);

    public static void Unload(Material material) => Raylib.UnloadMaterial(material);
        
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void SetTexture(Material* material, MaterialMapIndex mapType, Texture texture) => Raylib.SetMaterialTexture(material, mapType, texture);
    
    public static void SetTexture(ref Material material, MaterialMapIndex mapType, Texture texture)
    {
        unsafe
        {
            fixed(Material* materialPtr = &material)
                Raylib.SetMaterialTexture(materialPtr, mapType, texture);
        }
    }

    public static Material GenerateGridMaterial(Color color1, Color color2)
    {
        var mat = LoadDefault();
        mat.WithGridTexture(2, 2, color1, color2);
        return mat;
    }
}