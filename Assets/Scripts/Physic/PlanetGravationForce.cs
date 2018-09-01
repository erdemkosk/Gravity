using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravationForce : MonoBehaviour
{

    private float maxGravDist;
    [SerializeField]
    public float maxGravity;
    List<GameObject> allObjectsInsideAtmpshphere = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        maxGravDist =Vector2.Distance(gameObject.GetComponent<Collider2D>().bounds.max , gameObject.GetComponent<Collider2D>().bounds.center);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        foreach (GameObject obj in allObjectsInsideAtmpshphere)
        {
            float dist = Vector3.Distance( transform.position, obj.transform.position);
            if (dist <= maxGravDist)
            {
                Vector3 v = transform.position - obj.transform.position;

                obj.GetComponent<Rigidbody2D>().AddForce(v.normalized * (1.0f - dist / maxGravDist) * maxGravity);

                if (obj.transform.position.y < gameObject.transform.position.y)
                {
                    v = -v;
                }

                Quaternion targetRotation = Quaternion.FromToRotation(obj.transform.position,v);
                obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, targetRotation, Time.deltaTime);
            }

        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (allObjectsInsideAtmpshphere.Contains(col.gameObject) != true)
        {
            allObjectsInsideAtmpshphere.Add(col.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (allObjectsInsideAtmpshphere.Contains(col.gameObject) == true)
        {
            allObjectsInsideAtmpshphere.Remove(col.gameObject);
        }
    }
}
