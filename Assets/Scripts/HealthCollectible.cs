using UnityEngine;
public class HealthCollectible : MonoBehaviour
{
    public int amount = 1;

    void OnTriggerEnter2D(Collider2D col)
    {
        RubyController controller = col.GetComponent<RubyController>();

        if (controller == null)
            return;

        if (controller.CurrentHealth >= controller.maxHealth)
            return;

        controller.UpdateHealth(amount);
        Destroy(gameObject);
    }
}