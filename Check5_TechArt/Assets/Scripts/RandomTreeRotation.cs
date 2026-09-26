using UnityEngine;

public class RandomTreeRotation : MonoBehaviour
{
    void Start()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(0f, 360f), 0);
    }
}
