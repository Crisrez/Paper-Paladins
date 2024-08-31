using UnityEngine;

public class Fan : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up,Time.deltaTime*360*3,Space.World);
    }
}
