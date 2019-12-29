using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    Transform target;
    // Start is called before the first frame update
    private void Awake()
    {
        if (!target) target = FindObjectOfType<Character>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = target.position; position.z = -10.0f;
        transform.position = Vector3.Lerp(transform.position, position, 2.0f*Time.deltaTime);
    }
}
