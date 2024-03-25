using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _destinationTarget;

    private FollowerEntity _followerEntity;

    private void Start()
    {
        _followerEntity = GetComponent<FollowerEntity>();

        var pos = _destinationTarget.position;

        var finalPos = pos - (pos - transform.position).normalized * 6;

        _followerEntity.SetDestination(finalPos);
    }
}