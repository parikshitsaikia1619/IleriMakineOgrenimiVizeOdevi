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
    private int no;
    private void Start()
    {
        inde += 1;
        no = inde;
        OnPosition = true;
        targetPos = transform.position;
        Dots = new List<Dot>();
        GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
    }

    private void Update()
    {
        if (!OnPosition && GoDotsCenterPos())
            //transform.position = targetPos;
            transform.position = Vector3.MoveTowards(prevLoc, targetPos, 1.5f * Time.deltaTime);
        if (targetPos - transform.position == Vector3.zero)
        {
            OnPosition = true;
            transform.position += new Vector3(0.2f, 0);
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
        //Dots.Clear();
        foreach(Dot d in FindObjectOfType<KMeans>().Dots)
        {
            d.ConnectionToCentroid();
        }

    }

    public bool GoDotsCenterPos()
    {
        Vector3 tmpPrevPos = transform.position;
        //prevLoc = transform.position;
        Vector3 tmpPos = CalculateCenter();
        //targetPos = CalculateCenter();
        if (Vector3.Distance(tmpPrevPos, tmpPos) <= 0.1f)
            return false;
        Debug.Log(no + " -> Dot Count : " + Dots.Count);
        targetPos = tmpPos;
        prevLoc = tmpPrevPos;
        OnPosition = false;
        return true;
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
