using UnityEngine;

public class DecelerationZone : MonoBehaviour
{
    private const float SlowdownFactor = 0.1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var decelerate = other.GetComponent<IDecelerate>();
        if (decelerate == null)
            return;

        decelerate.SlowdownFactor = 0.1f;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        var decelerate = other.GetComponent<IDecelerate>();
        if (decelerate == null)
            return;

        decelerate.SlowdownFactor = 1f;
    }
}