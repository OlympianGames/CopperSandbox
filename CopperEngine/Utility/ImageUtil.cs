using System.Numerics;
using Raylib_CsLo;

namespace CopperEngine.Utility;

public static class ImageUtil
{
    public static Image Load(string path) => Raylib.LoadImage(path);
    public static Image Load(string path, int width, int height, PixelFormat format, int headerSize) => Raylib.LoadImageRaw(path, width, height, format, headerSize);
    
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe Image Load(string path, int* frames) => Raylib.LoadImageAnim(path, frames);
    public static Image Load(string path, ref int frames)
    {
        unsafe
        {
            fixed(int* framesPtr = &frames)
                return Raylib.LoadImageAnim(path, framesPtr);
        }
    }

    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe Image Load(string fileType, byte* fileData, int dataSize) => Raylib.LoadImageFromMemory(fileType, fileData, dataSize);
    public static Image Load(string fileType, ref byte fileData, int dataSize)
    {
        unsafe
        {
            fixed(byte* fileDataPtr = &fileData)
                return Raylib.LoadImageFromMemory(fileType, fileDataPtr, dataSize);
        }
    }

    public static Image Load(Texture texture) => Raylib.LoadImageFromTexture(texture);
    public static Image LoadFromScreen() => Raylib.LoadImageFromScreen();
    public static void Unload(Image image) => Raylib.UnloadImage(image);
    public static void Export(Image image, string path) => Raylib.ExportImage(image, path);
    public static bool ExportAsCode(Image image, string path) => Raylib.ExportImageAsCode(image, path);
    
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe Color* LoadColorsUnsafe(Image image) => Raylib.LoadImageColors(image);
    
    public static Color LoadColors(Image image)
    {
        unsafe
        {
            return *Raylib.LoadImageColors(image);
        }
    }

    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe Color* LoadPalette(Image image, int maxPaletteSize, int* colorCount) => Raylib.LoadImagePalette(image, maxPaletteSize, colorCount);
    public static Color LoadPalette(Image image, int maxPaletteSize, ref int colorCount)
    {
        unsafe
        {
            fixed (int* colorCountPtr = &colorCount)
                return *Raylib.LoadImagePalette(image, maxPaletteSize, colorCountPtr);
        }
    }

    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void UnloadColors(Color* color) => Raylib.UnloadImageColors(color);
    public static void UnloadColors(ref Color color)
    {
        unsafe
        {
            fixed (Color* colorPtr = &color)
                Raylib.UnloadImageColors(colorPtr);
        }
    }

    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void UnloadPalette(Color* color) => Raylib.UnloadImagePalette(color);
    public static void UnloadPalette(ref Color color)
    {
        unsafe
        {
            fixed (Color* colorPtr = &color)
                Raylib.UnloadImagePalette(colorPtr);
        }
    }

    public static Image GenColor(int width, int height, Color color) => Raylib.GenImageColor(width, height, color);
    public static Image GenGradientH(int width, int height, Color left, Color right) => Raylib.GenImageGradientH(width, height, left, right);
    public static Image GenGradientV(int width, int height, Color top, Color bottom) => Raylib.GenImageGradientV(width, height, top, bottom);
    public static Image GenGradientRadial(int width, int height, float density, Color inner, Color outer) => Raylib.GenImageGradientRadial(width, height, density, inner, outer);
    public static Image GenChecked(int width, int height, int checksX, int checksY, Color col1, Color col2) => Raylib.GenImageChecked(width, height, checksX, checksY, col1, col2);
    public static Image GenWhiteNoise(int width, int height, float factor) => Raylib.GenImageWhiteNoise(width, height, factor);
    public static Image GenPerlinNoise(int width, int height, int offsetX, int offsetY, float scale) => Raylib.GenImagePerlinNoise(width, height, offsetX, offsetY, scale);
    public static Image GenCellular(int width, int height, int tileSize) => Raylib.GenImageCellular(width, height, tileSize);
    public static Image Copy(Image image) => Raylib.ImageCopy(image);
    public static Image FromImage(Image image, Rectangle rec) => Raylib.ImageFromImage(image, rec);
    public static Image Text(string text, int fontSize, Color color) => Raylib.ImageText(text, fontSize, color);
    public static Image Text(Font font, string text, float fontSize, float spacing, Color color) => Raylib.ImageTextEx(font, text, fontSize, spacing, color);
    public static Rectangle GetAlphaBorder(Image image, float threshold) => Raylib.GetImageAlphaBorder(image, threshold);
    public static Color GetColor(Image image, int x, int y) => Raylib.GetImageColor(image, x, y);
    
    public static void Format(ref Image image, PixelFormat newFormat)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageFormat(imagePtr, newFormat);
        }
    }

    public static void ToPOT(ref Image image, Color fill)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageToPOT(imagePtr, fill);
        }
    }

    public static void Crop(ref Image image, Rectangle crop)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageCrop(imagePtr, crop);
        }
    }

    public static void AlphaCrop(ref Image image, float threshold)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageAlphaCrop(imagePtr, threshold);
        }
    }

    public static void AlphaClear(ref Image image, Color color, float threshold)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageAlphaClear(imagePtr, color, threshold);
        }
    }

    public static void AlphaMask(ref Image image, Image alphaMask)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageAlphaMask(imagePtr, alphaMask);
        }
    }

    public static void AlphaPremultiply(ref Image image)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageAlphaPremultiply(imagePtr);
        }
    }

    public static void Resize(ref Image image, int newWidth, int newHeight)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageResize(imagePtr, newWidth, newHeight);
        }
    }

    public static void ResizeNN(ref Image image, int newWidth, int newHeight)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageResizeNN(imagePtr, newWidth, newHeight);
        }
    }

    public static void ResizeCanvas(ref Image image, int newWidth, int newHeight, int offsetX, int offsetY, Color color)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageResizeCanvas(imagePtr, newWidth, newHeight, offsetX, offsetY, color);
        }
    }

    public static void Mipmaps(ref Image image)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageMipmaps(imagePtr);
        }
    }

    public static void Dither(ref Image image, int rBpp, int gBpp, int bBpp, int aBpp)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageDither(imagePtr, rBpp, gBpp, bBpp, aBpp);
        }
    }

    public static void FlipVertical(ref Image image)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageFlipVertical(imagePtr);
        }
    }

    public static void FlipHorizontal(ref Image image)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageFlipHorizontal(imagePtr);
        }
    }

    public static void RotateCW(ref Image image)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageRotateCW(imagePtr);
        }
    }

    public static void RotateCCW(ref Image image)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageRotateCCW(imagePtr);
        }
    }

    public static void ColorTint(ref Image image, Color color)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageColorTint(imagePtr, color);
        }
    }

    public static void ColorInvert(ref Image image)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageColorInvert(imagePtr);
        }
    }

    public static void ColorGrayscale(ref Image image)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageColorGrayscale(imagePtr);
        }
    }

    public static void ColorContrast(ref Image image, float contrast)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageColorContrast(imagePtr, contrast);
        }
    }

    public static void ColorBrightness(ref Image image, int brightness)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageColorBrightness(imagePtr, brightness);
        }
    }

    public static void ColorReplace(ref Image image, Color color, Color replace)
    {
        unsafe
        {
            fixed (Image* imagePtr = &image)
                Raylib.ImageColorReplace(imagePtr, color, replace);
        }
    }

    public static void ClearBackground(ref Image dst, Color color)
    {
        unsafe
        {
            fixed (Image* dstPtr = &dst)
                Raylib.ImageClearBackground(dstPtr, color);
        }
    }

    public static void DrawPixel(ref Image dst, int posX, int posY, Color color)
    {
        unsafe
        {
            fixed (Image* dstPtr = &dst)
                Raylib.ImageDrawPixel(dstPtr, posX, posY, color);
        }
    }

    public static void DrawPixel(ref Image dst, Vector2 pos, Color color)
    {
        unsafe
        {
            fixed (Image* dstPtr = &dst)
                Raylib.ImageDrawPixelV(dstPtr, pos, color);
        }
    }

    public static void DrawLine(ref Image dst, int startPosX, int startPosY, int endPosX, int endPosY, Color color)
    {
        unsafe
        {
            fixed (Image* dstPtr = &dst)
                Raylib.ImageDrawLine(dstPtr, startPosX, startPosY, endPosX, endPosY, color);
        }
    }

    public static void DrawLine(ref Image dst, Vector2 start, Vector2 end, Color color)
    {
        unsafe
        {
            fixed (Image* dstPtr = &dst)
                Raylib.ImageDrawLineV(dstPtr, start, end, color);
        }
    }

    public static void DrawCircle(ref Image dst, int centerX, int centerY, int radius, Color color)
    {
        unsafe
        {
            fixed (Image* dstPtr = &dst)
                Raylib.ImageDrawCircle(dstPtr, centerX, centerY, radius, color);
        }
    }

    public static void DrawCircle(ref Image dst, Vector2 center, int radius, Color color)
    {
        unsafe
        {
            fixed (Image* dstPtr = &dst)
                Raylib.ImageDrawCircleV(dstPtr, center, radius, color);
        }
    }

    public static void DrawRectangle(ref Image dst, int posX, int posY, int width, int height, Color color)
    {
        unsafe
        {
            fixed (Image* dstPtr = &dst)
                Raylib.ImageDrawRectangle(dstPtr, posX, posY, width, height, color);
        }
    }

    public static void DrawRectangle(ref Image dst, Vector2 pos, Vector2 size, Color color)
    {
        unsafe
        {
            fixed (Image* dstPtr = &dst)
                Raylib.ImageDrawRectangleV(dstPtr, pos, size, color);
        }
    }

    public static void DrawRectangleRec(ref Image dst, Rectangle rec, Color color)
    {
        unsafe
        {
            fixed (Image* dstPtr = &dst)
                Raylib.ImageDrawRectangleRec(dstPtr, rec, color);
        }
    }

    public static void DrawRectangleLines(ref Image dst, Rectangle rec, int thick, Color color)
    {
        unsafe
        {
            fixed (Image* dstPtr = &dst)
                Raylib.ImageDrawRectangleLines(dstPtr, rec, thick, color);
        }
    }

    public static void Draw(ref Image dst, Image src, Rectangle srcRec, Rectangle dstRec, Color color)
    {
        unsafe
        {
            fixed (Image* dstPtr = &dst)
                Raylib.ImageDraw(dstPtr, src, srcRec, dstRec, color);
        }
    }

    public static void DrawText(ref Image dst, string text, int x, int y, int fontSize, Color color)
    {
        unsafe
        {
            fixed (Image* dstPtr = &dst)
                Raylib.ImageDrawText(dstPtr, text, x, y, fontSize, color);
        }
    }

    public static void DrawText(ref Image dst, Font font, string text, Vector2 pos, int fontSize, float spacing, Color color)
    {
        unsafe
        {
            fixed (Image* dstPtr = &dst)
                Raylib.ImageDrawTextEx(dstPtr, font, text, pos, fontSize, spacing, color);
        }
    }


    #region Unsafe Functions

    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void Format(Image* image, PixelFormat newFormat) => Raylib.ImageFormat(image, newFormat);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void ToPOT(Image* image, Color fill) => Raylib.ImageToPOT(image, fill);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void Crop(Image* image, Rectangle crop) => Raylib.ImageCrop(image, crop);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void AlphaCrop(Image* image, float threshold) => Raylib.ImageAlphaCrop(image, threshold);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void AlphaClear(Image* image, Color color, float threshold) => Raylib.ImageAlphaClear(image, color, threshold);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void AlphaMask(Image* image, Image alphaMask) => Raylib.ImageAlphaMask(image, alphaMask);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void AlphaPremultiply(Image* image) => Raylib.ImageAlphaPremultiply(image);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void Resize(Image* image, int newWidth, int newHeight) => Raylib.ImageResize(image, newWidth, newHeight);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void ResizeNN(Image* image, int newWidth, int newHeight) => Raylib.ImageResizeNN(image, newWidth, newHeight);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void ResizeCanvas(Image* image, int newWidth, int newHeight, int offsetX, int offsetY, Color color) => Raylib.ImageResizeCanvas(image, newWidth, newHeight, offsetX, offsetY, color);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void Mipmaps(Image* image) => Raylib.ImageMipmaps(image);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void Dither(Image* image, int rBpp, int gBpp, int bBpp, int aBpp) => Raylib.ImageDither(image, rBpp, gBpp, bBpp, aBpp);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void FlipVertical(Image* image) => Raylib.ImageFlipVertical(image);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void FlipHorizontal(Image* image) => Raylib.ImageFlipHorizontal(image);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void RotateCW(Image* image) => Raylib.ImageRotateCW(image);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void RotateCCW(Image* image) => Raylib.ImageRotateCCW(image);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void ColorTint(Image* image, Color color) => Raylib.ImageColorTint(image, color);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void ColorInvert(Image* image) => Raylib.ImageColorInvert(image);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void ColorGrayscale(Image* image) => Raylib.ImageColorGrayscale(image);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void ColorContrast(Image* image, float contrast) => Raylib.ImageColorContrast(image, contrast);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void ColorBrightness(Image* image, int brightness) => Raylib.ImageColorBrightness(image, brightness);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void ColorReplace(Image* image, Color color, Color replace) => Raylib.ImageColorReplace(image, color, replace);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void ClearBackground(Image* dst, Color color) => Raylib.ImageClearBackground(dst, color);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void DrawPixel(Image* dst, int posX, int posY, Color color) => Raylib.ImageDrawPixel(dst, posX, posY, color);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void DrawPixel(Image* dst, Vector2 pos, Color color) => Raylib.ImageDrawPixelV(dst, pos, color);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void DrawLine(Image* dst, int startPosX, int startPosY, int endPosX, int endPosY, Color color) => Raylib.ImageDrawLine(dst, startPosX, startPosY, endPosX, endPosY, color);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void DrawLine(Image* dst, Vector2 start, Vector2 end, Color color) => Raylib.ImageDrawLineV(dst, start, end, color);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void DrawCircle(Image* dst, int centerX, int centerY, int radius, Color color) => Raylib.ImageDrawCircle(dst, centerX, centerY, radius, color);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void DrawCircle(Image* dst, Vector2 center, int radius, Color color) => Raylib.ImageDrawCircleV(dst, center, radius, color);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void DrawRectangle(Image* dst, int posX, int posY, int width, int height, Color color) => Raylib.ImageDrawRectangle(dst, posX, posY, width, height, color);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void DrawRectangle(Image* dst, Vector2 pos, Vector2 size, Color color) => Raylib.ImageDrawRectangleV(dst, pos, size, color);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void DrawRectangleRec(Image* dst, Rectangle rec, Color color) => Raylib.ImageDrawRectangleRec(dst, rec, color);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void DrawRectangleLines(Image* dst, Rectangle rec, int thick, Color color) => Raylib.ImageDrawRectangleLines(dst, rec, thick, color);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void Draw(Image* dst, Image src, Rectangle srcRec, Rectangle dstRec, Color color) => Raylib.ImageDraw(dst, src, srcRec, dstRec, color);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void DrawText(Image* dst, string text, int x, int y, int fontSize, Color color) => Raylib.ImageDrawText(dst, text, x, y, fontSize, color);
    [Obsolete("Now has a version that does not use unsafe code")]
    public static unsafe void DrawText(Image* dst, Font font, string text, Vector2 pos, int fontSize, float spacing, Color color) => Raylib.ImageDrawTextEx(dst, font, text, pos, fontSize, spacing, color);

    #endregion
}