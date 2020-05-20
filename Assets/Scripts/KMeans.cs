using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KMeans : MonoBehaviour
{
    public float maxX;
    public float maxY;
    public float maxZ;
    public int dotCount;
    public int groupCount;
    public List<Dot> Dots { get; set; }
    public List<Centroid> Centroids { get; set; }
    private int count;
    private bool c = false;
    public bool isStarted;
    public GameObject canvas;
    private void Awake()
    {
        Dots = new List<Dot>();
        Centroids = new List<Centroid>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ClearAll();
        }
        if (!isStarted)
            return;
        count = 0;
        foreach(Dot d in Dots)
        {
            if (d.OnPosition)
                count += 1;
        }
        
        if(count == Dots.Count && !c)
        {
            foreach(Centroid c in Centroids)
            {
                c.GoDotsCenterPos();
            }
            c = true;
        }
        
    }
    private void ClearAll()
    {
        Dots.Clear();
        Centroids.Clear();
        isStarted = false;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(obj);
        }
        canvas.SetActive(true);
        FindObjectOfType<Camera>().transform.position = Vector3.zero;
        c = false;
    }
    public void Initialize()
    {
        try
        {
            dotCount = Convert.ToInt32(GameObject.Find("DotCount").GetComponentInChildren<Text>().text);
            groupCount = Convert.ToInt32(GameObject.Find("GroupCount").GetComponentInChildren<Text>().text);
        }
        catch
        {

        }
        GameObject.Find("Canvas").SetActive(false);
        // Random generate centroid
        for(int i = 0; i < groupCount; i++)
        {
            GameObject centroidObj = Instantiate(Resources.Load<GameObject>("Centroid"), GameObject.Find("Centroids").transform);
            centroidObj.GetComponent<Centroid>().transform.position = new Vector3(UnityEngine.Random.Range(maxX * -1, maxX), UnityEngine.Random.Range(maxY * -1, maxY), UnityEngine.Random.Range(maxZ * -1, maxZ));
            Centroids.Add(centroidObj.GetComponent<Centroid>());
        }

        // Random generate dot
        for(int i = 0; i < dotCount; i++)
        {
            GameObject dotObj = Instantiate(Resources.Load<GameObject>("Dot"), GameObject.Find("Dots").transform);
            dotObj.GetComponent<Dot>().GoLocation(new Vector3(UnityEngine.Random.Range(maxX * -1, maxX), UnityEngine.Random.Range(maxY * -1, maxY), UnityEngine.Random.Range(maxZ * -1, maxZ)));
            Dots.Add(dotObj.GetComponent<Dot>());
        }
        isStarted = true;
    }

    public void NewCentroid(Centroid c)
    {
        if (!Centroids.Contains(c))
            Centroids.Add(c);
    }

    public void AddDot(Dot d)
    {
        if (!Dots.Contains(d))
            Dots.Add(d);
    }
    bool isSkybox = true;
    public void ChangeBackground()
    {
        if (isSkybox)
        {
            isSkybox = false;
            FindObjectOfType<Camera>().clearFlags = CameraClearFlags.SolidColor;
        }
        else
        {
            isSkybox = true;
            FindObjectOfType<Camera>().clearFlags = CameraClearFlags.Skybox;
        }
    }

}