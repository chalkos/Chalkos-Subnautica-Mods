using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UWE;
using Logger = QModManager.Utility.Logger;

namespace CyclopsDriftFix.GameObjects;

public class GrowingPlantsCollision : MonoBehaviour
{
    public bool debug = false;
    public GameObject collisions;

    public void Awake()
    {
        collisions = new GameObject("DriftFixCollisions");
        var ctr = collisions.transform;
        ctr.SetParent(transform, false);
        collisions.gameObject.SetActive(false);

        if (Main.Config.InteractScale > 0 && gameObject.GetComponentInParent<GrowingPlant>() is { } growingPlant)
        {
            float progress = 1.0f;
            float num = growingPlant.isIndoor
                ? growingPlant.growthWidthIndoor.Evaluate(progress)
                : growingPlant.growthWidth.Evaluate(progress);
            float y = growingPlant.isIndoor
                ? growingPlant.growthHeightIndoor.Evaluate(progress)
                : growingPlant.growthHeight.Evaluate(progress);
            ctr.localScale = new Vector3(num, y, num);

            Vector3 localScale = ctr.localScale;
            Vector3 position = new Vector3(localScale.x * growingPlant.positionOffset.x,
                localScale.y * growingPlant.positionOffset.y, localScale.z * growingPlant.positionOffset.z);
            ctr.localPosition = growingPlant.positionOffset;

            collisions.gameObject.SetActive(true);
            Logger.Log(Logger.Level.Fatal, $"[{transform.name}] progress = {progress} scale = {ctr.localScale}");
        }
        else
            Logger.Log(Logger.Level.Fatal, $"[{transform.name}] progress = {Main.Config.InteractScale} -- inactive");
    }

    public void CombineTransform(Transform current, Transform stop, ref Transform accumulator)
    {
        if (current.parent == stop) return;

        CombineTransform(current.parent, stop, ref accumulator);

        accumulator.SetParent(current, true);
    }

    public void YoinkCollider(Collider collision)
    {
        // get the growing plant, because we need the original growth transform
        if (gameObject.GetComponentInParent<GrowingPlant>() is not { } growingPlant) return;

        var collisionTransform = collision.transform;

        // if not yet yoinked, grab it into our collisions object
        if (collisionTransform.parent != collisions.transform)
        {
            // TODO: can I yoink only the Collider? SMLHelper has a deep clone. need specific test cases
            // for some plants this also yoinks the model, which means the plant won't visually grow and instead appear
            // to be the size of Main.Config.InteractScale until it finishes growing
            // the problem is in the GrowingPlant's model design, when the collider is in the same GameObject as the model

            // apply all the transformations the collision transform was receiving from the hierarchy (up to
            // the growing transform) onto itself
            collisionTransform.SetParent(growingPlant.growingTransform, true);

            // them move it to the fixed growth transform, keeping all the transformations
            collisionTransform.SetParent(collisions.transform, false);
        }

        // add debug because otherwise we're blind
        collision.gameObject.AddComponent<ColliderVisualizer>()
            .Initialize(collision, collision.GetType().Name.Replace("Collider", ""), 12);
    }

    public void Enable()
    {
        if (collisions is { } go)
            go.SetActive(true);
    }

    public void Disable()
    {
        if (collisions is { } go)
            go.SetActive(false);
    }
}