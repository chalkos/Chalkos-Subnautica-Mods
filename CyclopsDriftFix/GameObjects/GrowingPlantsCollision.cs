using UnityEngine;

namespace CyclopsDriftFix.GameObjects;

public class GrowingPlantsCollision : MonoBehaviour
{
    public GameObject collisions;

    public void Awake()
    {
        collisions = new GameObject("DriftFixCollisions");
        collisions.transform.SetParent(transform);

        if (Main.Config.InteractScale > 0 && gameObject.GetComponentInParent<GrowingPlant>() is { } growingPlant)
        {
            float progress = Main.Config.InteractScale;
            float num = growingPlant.isIndoor
                ? growingPlant.growthWidthIndoor.Evaluate(progress)
                : growingPlant.growthWidth.Evaluate(progress);
            float y = growingPlant.isIndoor
                ? growingPlant.growthHeightIndoor.Evaluate(progress)
                : growingPlant.growthHeight.Evaluate(progress);
            collisions.transform.localScale = new Vector3(num, y, num);
        }
    }

    public void YoinkCollider(Collider c)
    {
        c.transform.SetParent(collisions.transform);
    }
}