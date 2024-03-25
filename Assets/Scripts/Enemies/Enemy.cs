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
        _followerEntity.SetDestination(_destinationTarget.position);
    }

    private void Update()
    {
    }
}