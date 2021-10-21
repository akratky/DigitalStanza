using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLine : MonoBehaviour
{

    public GameObject LineStart;
    private Vector3 startPoint;
    private Vector3 endPoint;
    public Camera cam;
    public GameObject bookLine;
    private LineRenderer LineRend;
    public float appearanceTime = 0.7f;
    public float disAppearanceTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        bookLine.SetActive(false);
        LineRend = bookLine.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MakeLine()
    {
        startPoint = LineStart.transform.position;

        //Ray ray = new Ray(transform.position, Vector3.forward);
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.7F, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            endPoint = hit.point;
            // Debug.DrawLine(startPoint, endPoint, Color.green, 1000);

            LineRend.SetPosition(0, startPoint);
            LineRend.SetPosition(1, endPoint);

            StartCoroutine(AppearanceTimer());

        }

    }

    IEnumerator AppearanceTimer()
    {

        float elapsedTime = 0;

        while (elapsedTime <= appearanceTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bookLine.SetActive(true);
        StartCoroutine(DisAppearanceTimer());
    }


    IEnumerator DisAppearanceTimer()
    {

        float elapsedTime = 0;

        while (elapsedTime <= disAppearanceTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bookLine.SetActive(false);
    }

}

