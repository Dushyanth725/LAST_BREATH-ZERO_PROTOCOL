using UnityEngine;

public class SirenSpin : MonoBehaviour
{
    public float speed = 180f;

    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}