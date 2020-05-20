using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public Centroid ItsGroup { get; set; }
    private Vector3 target;
    public bool OnPosition { get; set; }
    private KMeans manager;

    private void Start()
    {
        OnPosition = false;
        manager = FindObjectOfType<KMeans>();
        GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
    }

    public void GoLocation(Vector3 t)
    {
        target = t;
        transform.position = Vector3.MoveTowards(transform.position, target, 2f * Time.deltaTime);
        //transform.position = t;
    }

    private void Update()
    {
        if (target != null && transform.position - target != Vector3.zero)
        {
            GoLocation(target);
        }
        if (transform.position - target == Vector3.zero)
            OnPosition = true;
        if (OnPosition)
            ConnectionToCentroid();
    }

    public void ConnectionToCentroid()
    {
        float min = Vector3.Distance(manager.Centroids[0].transform.position, transform.position);
        ItsGroup = manager.Centroids[0];
        foreach(Centroid c in manager.Centroids)
        {
            float distance = Vector3.Distance(c.transform.position, transform.position);
            if(distance < min)
            {
                min = distance;
                ItsGroup = c;
            }
        }
        ItsGroup.AddDot(this);
        GetComponent<Renderer>().material.color = ItsGroup.GetComponent<Renderer>().material.color;
    }

}
