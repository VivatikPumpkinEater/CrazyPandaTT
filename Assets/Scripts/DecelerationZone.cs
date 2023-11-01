using UnityEngine;

public class DecelerationZone : MonoBehaviour
{
    private const float SlowdownFactor = 0.1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var slowdowner = other.GetComponent<ISlowdowner>();
        if (slowdowner == null)
            return;

        slowdowner.SlowdownFactor = 0.1f;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        var slowdowner = other.GetComponent<ISlowdowner>();
        if (slowdowner == null)
            return;

        slowdowner.SlowdownFactor = 1f;
    }
}