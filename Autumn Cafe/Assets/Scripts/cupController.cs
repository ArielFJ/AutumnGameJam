using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cupController : MonoBehaviour
{
    public LayerMask planeLayer;
    public LayerMask layerMask;

    public OpenCup cupVisuals;
    public Meal mealScript;

    public float fillSpeed;
    public float totalFill;
    public float maxFill;

    [System.Serializable]
    public class fillEntry
    {
        public string substance;
        public float amount;
    }

    public List<fillEntry> filledWith = new List<fillEntry>();

    public List<DrinkEntry> recipies = new List<DrinkEntry>();

    [System.Serializable]
    public class DrinkEntry
    {
        public List<string> Ingredients = new List<string>();
        public List<float> Amounts = new List<float>();
        public MealType product;
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
    }

    private void OnDisable()
    {
        gameObject.GetComponent<Rigidbody>().freezeRotation = false;
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

        foreach (DrinkEntry entry in recipies)
        {
            if (filledWith.Count == entry.Ingredients.Count)
            {
                bool falied = false;

                foreach (string ingredient in entry.Ingredients)
                {
                    int i = 0;
                    foreach (fillEntry FE in filledWith)
                    {
                        if (FE.substance == ingredient && entry.Amounts[i] >= FE.amount - 15 && entry.Amounts[i] <= FE.amount + 15)
                        {
                            falied = false; // our ingredients match up so we havent failed yet
                            break;
                        }
                        else
                        {
                            falied = true; // there is an ingredient missing so we fail
                            i++;
                        }
                    }
                }

                if (falied == false) // we've gone through each ingredient and they all match up
                {
                    mealScript.mealType = entry.product;
                }
            }
        }
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

                bool failed = true;
                foreach(fillEntry entry in filledWith)
                {
                    if (entry.substance == fillHit.collider.GetComponent<Fillable>().containing)
                    {
                        entry.amount += fillSpeed * Time.deltaTime;
                        totalFill += fillSpeed * Time.deltaTime;

                        if(entry.amount >= maxFill)
                        {
                            entry.amount = maxFill;
                        }

                        if (totalFill >= maxFill)
                        {
                            totalFill = maxFill;
                        }
                        failed = false;
                    }
                    else
                    {
                        failed = true;
                    }
                }

                if(failed == true)
                {
                    fillEntry newEntry = new fillEntry();
                    newEntry.substance = fillHit.collider.GetComponent<Fillable>().containing;
                    newEntry.amount = fillSpeed * Time.deltaTime;
                    filledWith.Add(newEntry);
                }
            }
        }
    }
}
