using System;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshAgentController : Controller
{
    private INavMeshMovable _movable;
    private NavMeshPath _path;
    Vector3 _target;
    NavMeshQueryFilter _filter;

    public NavMeshAgentController (INavMeshMovable movable, NavMeshQueryFilter filter)
    {
        _movable = movable;
        _filter  = filter;
        _path    = new NavMeshPath();
    }

    public override void UpdateTarget (Vector3 target)
    {
        _target = target;
    }

    protected override void UpdateInternal ()
    {
        if (NavMeshUtilities.TryGetPath(_movable.Position, _target, _filter, _path))
        {
            _movable.WalkTo(_target);
        }
    }
}
