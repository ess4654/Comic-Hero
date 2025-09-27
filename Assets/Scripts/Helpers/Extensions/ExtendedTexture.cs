using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Extends the functionality of <see cref="Texture"/>.
/// </summary>
public static class ExtendedTexture
{
    public static Texture2D ToTexture2D(this Texture texture)
    {
        if(texture == null) return new Texture2D(1, 1);

        //Texture2D render = new Texture2D(texture.width, texture.height);
        //render.CopyPixels(texture);
        //render.Apply();

        //return Texture2D.CreateExternalTexture(
        //        texture.width,
        //        texture.height,
        //        TextureFormat.RGB24,
        //        false, false,
        //        texture.GetNativeTexturePtr());

        Texture2D render = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        render.Apply(false);
        Graphics.CopyTexture(texture, render);

        return render;
    }
}