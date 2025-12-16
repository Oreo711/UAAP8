using UnityEngine;
using UnityEngine.AI;


public static class NavMeshUtilities
{
    public static float GetPathLength (NavMeshPath path)
    {
        float pathLength = 0;

        if (path.corners.Length > 1)
        {
            for (int i = 1; i < path.corners.Length; i++)
            {
                pathLength += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
        }

        return pathLength;
    }

    public static bool TryGetPath (Vector3 source, Vector3 destination, NavMeshQueryFilter filter, NavMeshPath path)
    {
        if (NavMesh.CalculatePath(source, destination, filter, path) && path.status != NavMeshPathStatus.PathInvalid)
        {
            return true;
        }
        return false;
    }
}
