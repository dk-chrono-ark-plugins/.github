using Object = UnityEngine.Object;

namespace Mcm.Helper;

public static class TextureBlitter
{
    private static readonly Dictionary<string, Texture2D> _cached = [];

    /// <summary>
    ///     Blit a new Texture2D from a packed texture atlas with given rect.
    /// </summary>
    /// <param name="sourceTexture">packed texture atlas</param>
    /// <param name="textureRect">dst rect</param>
    /// <returns>Unpacked Texture2D from atlas. Result is cached.</returns>
    public static Texture2D Blit(this Texture2D sourceTexture, Rect textureRect)
    {
        var id = sourceTexture.name + textureRect;
        if (_cached.TryGetValue(id, out var cachedTexture)) {
            return cachedTexture;
        }

        var textureToReadFrom = sourceTexture;
        var createdTemporaryTexture = false;

        if (!sourceTexture.isReadable) {
            var renderTexture = RenderTexture.GetTemporary(
                sourceTexture.width,
                sourceTexture.height,
                0,
                RenderTextureFormat.Default,
                RenderTextureReadWrite.Linear
            );
            Graphics.Blit(sourceTexture, renderTexture);

            var backup = RenderTexture.active;
            RenderTexture.active = renderTexture;

            textureToReadFrom = new(sourceTexture.width, sourceTexture.height);
            textureToReadFrom.ReadPixels(new(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            textureToReadFrom.Apply();

            RenderTexture.active = backup;
            RenderTexture.ReleaseTemporary(renderTexture);

            createdTemporaryTexture = true;
        }

        var x = Mathf.FloorToInt(textureRect.x);
        var y = Mathf.FloorToInt(textureRect.y);
        var width = Mathf.FloorToInt(textureRect.width);
        var height = Mathf.FloorToInt(textureRect.height);
        Texture2D subTexture = new(width, height, TextureFormat.ARGB32, false);

        subTexture.SetPixels(textureToReadFrom.GetPixels(x, y, width, height));
        subTexture.Apply();

        if (createdTemporaryTexture) {
            Object.DestroyImmediate(textureToReadFrom);
        }

        _cached[id] = subTexture;
        return subTexture;
    }
}