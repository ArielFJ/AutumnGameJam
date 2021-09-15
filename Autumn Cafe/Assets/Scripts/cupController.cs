using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cupController : MonoBehaviour
{
    public LayerMask planeLayer;
    public LayerMask layerMask;

    public OpenCup cupVisuals;

    public float fillSpeed;
    public float totalFill;
    public float maxFill;

    public Dictionary<string, float> filledWith = new Dictionary<string, float>();

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3,planeLayer))
        {
            transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }

        if (Input.GetMouseButton(0))
        {
            Fill();
        }

        cupVisuals.fillAmount = totalFill;
    }
    
    private void Fill()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit fillHit;
        if (Physics.Raycast(ray, out fillHit, 3f, layerMask))
        {
            if (fillHit.collider.GetComponent<Fillable>()) // && fillHit.collider.GetComponent<Fillable>().amount > 0 but we aren't doing that for now
            {
                fillHit.collider.GetComponent<Fillable>().amount -= fillSpeed * Time.deltaTime;

                if (filledWith.ContainsKey(fillHit.collider.GetComponent<Fillable>().containing))
                {
                    filledWith[fillHit.collider.GetComponent<Fillable>().containing] = filledWith[fillHit.collider.GetComponent<Fillable>().containing] + fillSpeed * Time.deltaTime;
                    totalFill += fillSpeed * Time.deltaTime;
                }
                else
                {
                    filledWith.Add(fillHit.collider.GetComponent<Fillable>().containing, fillSpeed * Time.deltaTime);
                    totalFill += fillSpeed * Time.deltaTime;
                }
            }
        }
    }
}
