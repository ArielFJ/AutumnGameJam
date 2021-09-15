using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CoffeeStation : Interactible
{
    public bool isInteractingWithThis;

    public GameObject cupPrefab;

    public Transform cameraPosition;
    public Transform cupPosition;
    public Transform cupPullPosition;

    public GameObject cup;
    public bool holdCup;

    public LayerMask layerMask;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isInteractingWithThis)
        {
            stopInteracting();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!cup)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 2, layerMask))
                {
                    if (hit.collider.tag == "cupStack")
                    {
                        GameObject instance = Instantiate(cupPrefab, cupPullPosition.position, Quaternion.identity,transform);
                        cup = instance;
                        enableCupController();
                    }
                }
            }
        }
    }

    public override void interactFunction()
    {
        isInteractingWithThis = true;
        gameObject.GetComponent<Collider>().enabled = false;
        player.GetComponent<PickUpAndInteract>().canInteract = false;
        player.GetComponent<PickUpAndInteract>().tooltipText.gameObject.SetActive(false);
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponentInChildren<Camera>().gameObject.transform.position = cameraPosition.position;
        player.GetComponentInChildren<Camera>().gameObject.transform.rotation = cameraPosition.rotation;

        if (cup)
        {
            enableCupController();
        }
        Cursor.lockState = CursorLockMode.Confined;
    }

    void stopInteracting()
    {
        isInteractingWithThis = false;
        gameObject.GetComponent<Collider>().enabled = true;
        player.GetComponent<PickUpAndInteract>().canInteract = true;
        player.GetComponent<PickUpAndInteract>().tooltipText.gameObject.SetActive(true);
        player.GetComponent<FirstPersonController>().enabled = true;
        player.GetComponentInChildren<Camera>().gameObject.transform.position = player.GetComponent<PickUpAndInteract>().camReturnPosition.position;
        player.GetComponentInChildren<Camera>().gameObject.transform.rotation = player.GetComponent<PickUpAndInteract>().camReturnPosition.rotation;

        if (cup)
        {
            disableCupController();
        }
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<PickUpAndInteract>().enabled = false;
        player.GetComponent<PickUpAndInteract>().enabled = true; ///janky workaround to station disabling interaction
    }

    void enableCupController()
    {
        cup.GetComponent<cupController>().enabled = true;
    }

    void disableCupController()
    {
        cup.GetComponent<cupController>().enabled = false;
    }
}
