using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Asteroid : MonoBehaviour
{
    [SerializeField] private float speed = 4f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
