using System.Collections.Generic;
using UnityEngine;

namespace CyclopsDriftFix.GameObjects;

public class CollisionIgnorer : MonoBehaviour
{
    private Collider[] _colliders;
    private HashSet<int> _handledColliders;

    /// <summary>
    /// - make sure we have a rigidbody together with the colliders so OnCollisionEnter gets called
    /// - store colliders in this gameobject for later use
    /// </summary>
    private void Start()
    {
        gameObject.EnsureComponent<Rigidbody>().isKinematic = true;
        
        _colliders = gameObject.GetComponents<Collider>();
        _handledColliders = new HashSet<int>();
        
        //_colliders.ForEach(collider => Debug.Log($"ColliderPatcher: monitoring our collider {collider.GetInstanceID():X}"));
    }

    /// <summary>
    /// called when any colliders in this gameobject colide with something
    /// </summary>
    /// <param name="collision">collision info</param>
    public void OnCollisionEnter(Collision collision)
    {
        var collider = collision.collider;
        var colliderId = collider.GetInstanceID();

        // if we already handled this collider, do nothing
        if (_handledColliders.Contains(colliderId)) return;

        //Debug.Log($"ColliderPatcher: Handling collision with {colliderId:X}");
        if (collider.gameObject.GetComponentInParent<SeaTruckSegment>())
        {
            // make ALL our colliders ignore collisions with the seatruck/cyclops we just hit
            //Debug.Log($"ColliderPatcher: That's a SeaTruckSegment collision!");
            _colliders.ForEach(ourCollider =>
            {
                //Debug.Log($"ColliderPatcher: Ignoring between {ourCollider.GetInstanceID():X} and {colliderId}");
                Physics.IgnoreCollision(ourCollider, collider);
            });
        }
        // else
        //     Debug.Log($"ColliderPatcher: irrelevant");

        // exit early next time we collide with this one, since it has been handled
        _handledColliders.Add(colliderId);
    }
}