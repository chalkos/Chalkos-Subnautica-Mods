using System.Collections.Generic;
using UnityEngine;

namespace CyclopsDriftFix.GameObjects;

/// <summary>
/// Adds a Rigidbody to the gameobject that has the collider
/// (BZ) Also monitors collisions to make colliders that collided with SeaTruck (which happens as well when they are
/// growing inside the seatruck segment) ignore the seatruck segment's colliders
/// </summary>
public class CollisionIgnorer : MonoBehaviour
{
    private void Start()
    {
        //gameObject.EnsureComponent<CollisionLogger>(); //debug

        var rb = gameObject.EnsureComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        rb.isKinematic = true;
#if SN1
    }
}
#elif BZ
        _colliders = gameObject.GetComponents<Collider>();
        _handledColliders = new HashSet<int>();

        //_colliders.ForEach(collider => //debug
            //Debug.Log($"ColliderPatcher: monitoring our collider {collider.GetInstanceID():X}")); //debug
    }

    private Collider[] _colliders;
    private HashSet<int> _handledColliders;


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

        //Debug.Log($"ColliderPatcher: Handling collision with {colliderId:X}"); //debug
        if (collider.gameObject.GetComponentInParent<SeaTruckSegment>())
        {
            // make ALL our colliders ignore collisions with the seatruck/cyclops we just hit
            //Debug.Log($"ColliderPatcher: That's a SeaTruckSegment collision!"); //debug
            _colliders.ForEach(ourCollider =>
            {
                //Debug.Log( //debug
                    //$"ColliderPatcher: Ignoring between {ourCollider.GetInstanceID():X} and {colliderId:X}"); //debug
                Physics.IgnoreCollision(ourCollider, collider);
            });
        }
        //else //debug
            //Debug.Log($"ColliderPatcher: irrelevant"); //debug

        // exit early next time we collide with this one, since it has been handled
        _handledColliders.Add(colliderId);
    }
}
#endif