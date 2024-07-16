// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RedCarMove : MonoBehaviour
// {
//     private int p;
//     public float timer;
//     private float lifeTime = 0f;

//     private float speed = 3.5f;

//     void Start()
//     {
//         gameObject.transform.position = new Vector3(799.32f, 0.0f, 991.6682f);
//         gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
//     }

//     void Update()
//     {
//         lifeTime += Time.deltaTime;
//         transform.position += speed * transform.forward * Time.deltaTime;
//         if (lifeTime >= 7f)
//         {
//             Destroy(gameObject);
//         }
//     }
// }
