using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Shot : MonoBehaviour
{

    [SerializeField] private float speed = 3f;
    [SerializeField] private float lifeTime = 2f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
