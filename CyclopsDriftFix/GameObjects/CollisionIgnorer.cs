using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Logger = QModManager.Utility.Logger;

namespace CyclopsDriftFix.GameObjects;

public class CollisionIgnorer : MonoBehaviour
{
    private Collider[] _colliders;
    private HashSet<int> _handledColliders;

    private void Start()
    {
        _colliders = gameObject.GetComponents<Collider>();
        _handledColliders = new HashSet<int>();
        
        //_colliders.ForEach(collider => Debug.Log($"ColliderPatcher: monitoring our collider {collider.GetInstanceID():X}"));
    }

    public void OnCollisionEnter(Collision collision)
    {
        var collider = collision.collider;
        var colliderId = collider.GetInstanceID();
        if (_handledColliders.Contains(colliderId)) return;

        //Debug.Log($"ColliderPatcher: Handling collision with {colliderId:X}");

        if (collider.gameObject.GetComponentInParent<SeaTruckSegment>())
        {
            //Debug.Log($"ColliderPatcher: That's a SeaTruckSegment collision!");
            _colliders.ForEach(ourCollider =>
            {
                //Debug.Log($"ColliderPatcher: Ignoring between {ourCollider.GetInstanceID():X} and {colliderId}");
                Physics.IgnoreCollision(ourCollider, collider);
            });
        }
        // else
        //     Debug.Log($"ColliderPatcher: irrelevant");

        _handledColliders.Add(colliderId);
    }
}