using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Interactable focus;

    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        //left mouse
		if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);
                motor.MoveToPoint(hit.point);

                RemoveFocus();
            }
        }
        //right mouse
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if(interactable != null)
                {
                    SetFocus(interactable);
                }
                // Check if we hit an inteactable
            }
        }
    }
    void SetFocus (Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
    
    newFocus.OnFocused(transform);
    
    }
    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        motor.StopFollowTarget();
    }
}
