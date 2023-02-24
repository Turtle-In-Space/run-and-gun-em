using UnityEngine;

public static class RotateVector3
{
    public static Vector3 Rotate(this Vector3 vector, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tempX = vector.x;
        float tempY = vector.y;

        vector.x = (cos * tempX) - (sin * tempY);
        vector.y = (sin * tempX) + (cos * tempY);

        return vector;
    }

    public static Vector2 Rotate(this Vector2 vector, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tempX = vector.x;
        float tempY = vector.y;

        vector.x = (cos * tempX) - (sin * tempY);
        vector.y = (sin * tempX) + (cos * tempY);

        return vector;
    }

    public static Vector3 RotateWithRad(this Vector3 vector, float radians)
    {
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);

        float tempX = vector.x;
        float tempY = vector.y;

        vector.x = (cos * tempX) - (sin * tempY);
        vector.y = (sin * tempX) + (cos * tempY);

        return vector;
    }
}