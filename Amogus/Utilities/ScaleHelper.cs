using UnityEngine;

namespace Amogus.Utilities;

public static class ScaleHelper
{
    public static void SetGlobalScale(this Transform transform, Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        Vector3 lossy = transform.lossyScale;
        transform.localScale = new Vector3 (globalScale.x/lossy.x, globalScale.y/lossy.y, globalScale.z/lossy.z);
    }
}