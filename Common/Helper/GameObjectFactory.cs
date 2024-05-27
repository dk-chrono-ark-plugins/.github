namespace Mcm.Helper;

public static class GameObjectFactory
{
    public static void AlignToCenter(this RectTransform rect)
    {
        if (rect == null) {
            return;
        }

        rect.anchorMin = new(0.5f, 0.5f);
        rect.anchorMax = new(0.5f, 0.5f);
        rect.anchoredPosition = Vector2.zero;
        rect.pivot = new(0.5f, 0.5f);
    }

    public static void AlignToTop(this RectTransform rect, Vector2? offset = null)
    {
        if (rect == null) {
            return;
        }

        rect.anchorMin = new(0.5f, 1f);
        rect.anchorMax = new(0.5f, 1f);
        rect.anchoredPosition = offset ?? Vector2.zero;
        rect.pivot = new(0.5f, 1f);
    }

    public static void AlignToBottom(this RectTransform rect, Vector2? offset = null)
    {
        if (rect == null) {
            return;
        }

        rect.anchorMin = new(0.5f, 0f);
        rect.anchorMax = new(0.5f, 0f);
        rect.anchoredPosition = offset ?? Vector2.zero;
        rect.pivot = new(0.5f, 0f);
    }

    public static void AlignToLeft(this RectTransform rect, Vector2? offset = null)
    {
        if (rect == null) {
            return;
        }

        rect.anchorMin = new(0f, 0.5f);
        rect.anchorMax = new(0f, 0.5f);
        rect.anchoredPosition = offset ?? Vector2.zero;
        rect.pivot = new(0f, 0.5f);
    }

    public static void AlignToRight(this RectTransform rect, Vector2? offset = null)
    {
        if (rect == null) {
            return;
        }

        rect.anchorMin = new(1f, 0.5f);
        rect.anchorMax = new(1f, 0.5f);
        rect.anchoredPosition = offset ?? Vector2.zero;
        rect.pivot = new(1f, 0.5f);
    }

    public static void SetToStretch(this RectTransform rect)
    {
        if (rect == null) {
            return;
        }

        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        rect.sizeDelta = Vector2.zero;
    }

    public static RectTransform AttachRectTransformObject(this Transform parent, string name, bool center = true)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent);
        var rect = go.GetOrAddComponent<RectTransform>();
        if (center) {
            rect.AlignToCenter();
        }

        return rect;
    }

    public static T AddComponent<T>(this Transform parent) where T : Component
    {
        return parent.gameObject.GetOrAddComponent<T>();
    }
}