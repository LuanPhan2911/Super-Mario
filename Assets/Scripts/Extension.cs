

using UnityEngine;

public static class Extension
{

    private static LayerMask layerMask = LayerMask.GetMask("Default");

    // Checks if the rigidbody is colliding with an object in a given direction.
    // For example, if you want to check if the player is touching the ground,
    // you can pass Vector2.down. If you want to check if the player is running
    // into a wall, you can pass Vector2.right or Vector2.left.
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        if (rigidbody.isKinematic)
        {
            return false;
        }

        float radius = 0.25f;
        float distance = 0.375f;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, layerMask);
        return hit.collider != null && hit.rigidbody != rigidbody;
    }

    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection)
    {
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
    }
}
