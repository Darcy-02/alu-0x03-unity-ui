using UnityEngine;

public class Rotator : MonoBehaviour
{
    void Update()
    {
        // Rotate the coin on the X axis over time
        transform.Rotate(45f * Time.deltaTime, 0f, 0f);
    }
}
