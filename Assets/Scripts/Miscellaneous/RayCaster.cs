using UnityEngine;

public static class RayCaster
{
    public static bool TryGetRayCastPoint (Vector3 origin, Vector3 direction, LayerMask layer, out RaycastHit point)
    {
        Ray ray = new Ray(origin, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layer))
        {
            point = hit;
            return true;
        }

        point = new RaycastHit();
        return false;
    }
}
