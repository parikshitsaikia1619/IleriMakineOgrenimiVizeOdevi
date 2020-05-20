using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centroid : MonoBehaviour
{
    public List<Dot> Dots { get; set; }
    public bool OnPosition { get; set; }
    private Vector3 targetPos;
    private Vector3 prevLoc;
    private static int inde = 0;
    private void Start()
    {
        inde += 1;
        OnPosition = true;
        targetPos = transform.position;
        Dots = new List<Dot>();
        GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
    }

    private void Update()
    {
        if (!OnPosition)
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.8f * Time.deltaTime);
        if (targetPos - transform.position == Vector3.zero)
        {
            OnPosition = true;
            transform.position += new Vector3(0.1f, 0);
            SetDots();
        }
    }

    public void AddDot(Dot d)
    {
        if (!Dots.Contains(d))
            Dots.Add(d);
    }

    private void SetDots()
    {
        Dots.Clear();
        foreach(Dot d in FindObjectOfType<KMeans>().Dots)
        {
            d.ConnectionToCentroid();
        }

    }

    public void GoDotsCenterPos()
    {
        prevLoc = transform.position;
        targetPos = CalculateCenter();
        if (Vector3.Distance(prevLoc, targetPos) <= 0.2f)
            return;
        OnPosition = false;
    }

    private Vector3 CalculateCenter()
    {
        Vector3 pos = Vector3.zero;
        float x = 0, y = 0, z = 0;
        foreach(Dot d in Dots)
        {
            x += d.transform.position.x;
            y += d.transform.position.y;
            z += d.transform.position.z;
        }
        x /= Dots.Count;
        y /= Dots.Count;
        z /= Dots.Count;
        pos = new Vector3(x, y, z);

        return pos;
    }

}
