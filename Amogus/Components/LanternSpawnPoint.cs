using System;
using UnityEngine;

namespace Amogus.Components;

public class LanternSpawnPoint : MonoBehaviour
{
    public Color flameColor = Color.green;
    public Color ambientColor = Color.gray;
    public float amplifyFlameColor = 1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = flameColor;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}