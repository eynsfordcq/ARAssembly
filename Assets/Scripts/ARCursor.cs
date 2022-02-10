using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCursor : MonoBehaviour
{
    
    public GameObject cursorChildObject; //cursor image
    public GameObject objectToPlace; //the 3d object
    public ARRaycastManager raycastManager; //reference to raycast manager
    private GameObject placedObject; //to check if there's an instance of object already
    private bool useCursor = true;

    public GameObject stepManager; //reference to the text manager object in the scene
    private StepManager manager; //reference to TextManager script under text manager

    // Start is called before the first frame update
    void Start()
    {
        manager = stepManager.GetComponent<StepManager>();
        cursorChildObject.SetActive(useCursor);
    }

    // Update is called once per frame
    void Update()
    {
        if (useCursor) //cursor active?
        {
            UpdateCursor(); 
            //if touch is detected
            if (objectToPlace != null && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                placeObject();
            }
        }
    }

    void UpdateCursor()
    {
        //screen position to set cursor to, here, the middle of screen is used
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        //records the plane detected
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        //we only want to detect planes at screenposition, and store the detected planes into the list "hits"
        raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes); 

        //if hits are detected
        if (hits.Count > 0)
        {
            //update the transform of the cursor to the detected plane
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }

    void placeObject()
    {
        //if there is already an existing object
        if (placedObject != null)
        {
            Destroy(placedObject);
        }
        //instantiate a new object at the cursor's transform 
        placedObject = GameObject.Instantiate(objectToPlace, transform.position, transform.rotation);
        //sends a reference of the newly instantiate object to text manager
        manager.bindModel(placedObject);
        //disable the cursor
        useCursor = false;
        //disable the cursor image
        cursorChildObject.SetActive(useCursor);
    }

    public void EnableCursor()
    {
        //enable the cursor
        useCursor = true;
        //enable the cursor image
        cursorChildObject.SetActive(useCursor);
    }
}
