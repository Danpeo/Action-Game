using UnityEngine;

public static class PhysicsDebug
{
    public static void DrawDebug(Vector3 worldPosition, float radius, Color color, float duration = 1f)
    {
        Debug.DrawRay(worldPosition, radius * Vector3.up, color, duration);
        Debug.DrawRay(worldPosition, radius * Vector3.down, color, duration);
        Debug.DrawRay(worldPosition, radius * Vector3.left, color, duration);
        Debug.DrawRay(worldPosition, radius * Vector3.right, color, duration);
        Debug.DrawRay(worldPosition, radius * Vector3.forward, color, duration);
        Debug.DrawRay(worldPosition, radius * Vector3.back, color, duration);
    }
}