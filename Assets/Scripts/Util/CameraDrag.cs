using UnityEngine;
using System.Collections;

public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = 1;
    public float scrollSpeed = 75;

    public GameObject leftBound;
    public GameObject rightBound;
    public GameObject topBound;
    public GameObject bottomBound;

    float minZoom = 1;
    float maxZoom = 10;

    private void Start()
    {
        Camera.main.orthographicSize = 5;
    }

    public void setBounds()
    {
        leftBound = GameObject.Find("leftBound");
        rightBound = GameObject.Find("rightBound");
        topBound = GameObject.Find("topBound");
        bottomBound = GameObject.Find("bottomBound");
        this.transform.position = Vector3.Lerp(leftBound.transform.position, rightBound.transform.position, 0.5f);
        //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z-1);
    }



    void LateUpdate()
    {
        float xPos = Mathf.Clamp(this.transform.position.x, leftBound.transform.position.x, rightBound.transform.position.x);
        float yPos = Mathf.Clamp(this.transform.position.y, bottomBound.transform.position.y, topBound.transform.position.y);

        float zoomLevel = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);

        this.transform.position = new Vector3(xPos, yPos, -1f);
        if (Input.GetKey("w"))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + dragSpeed * Time.deltaTime, this.transform.position.z);
        }
        else if (Input.GetKey("s"))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - dragSpeed * Time.deltaTime, this.transform.position.z);
        }
        if (Input.GetKey("a"))
        {
            this.transform.position = new Vector3(this.transform.position.x - dragSpeed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
        }
        else if (Input.GetKey("d"))
        {
            this.transform.position = new Vector3(this.transform.position.x + dragSpeed * Time.deltaTime, this.transform.position.y, this.transform.position.z);
        }


        Camera.main.orthographicSize = zoomLevel;
        if (Input.mouseScrollDelta.y > 0 && (Camera.main.orthographicSize > minZoom)) //scroll up, zoom in
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize - scrollSpeed * Time.deltaTime;
            
        }
        else if (Input.mouseScrollDelta.y < 0 && (Camera.main.orthographicSize < maxZoom)) //scroll down, zoom out
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + scrollSpeed * Time.deltaTime;
            
        }
        
    }


}