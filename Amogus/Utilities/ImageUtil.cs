﻿using UnityEngine;

namespace Amogus.Utilities;

public static class ImageUtil
{
    public static Texture2D TintImage(Texture2D image, Color tint)
    {
        var pixels = image.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i].r *= tint.r;
            pixels[i].g *= tint.g;
            pixels[i].b *= tint.b;
        }

        var newImage = new Texture2D(image.width, image.height);
        newImage.SetPixels(pixels);
        newImage.Apply();
        return newImage;
    }
}