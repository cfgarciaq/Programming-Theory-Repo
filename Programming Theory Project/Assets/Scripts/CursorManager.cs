using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    private new Camera camera;
    //private GameObject AnimalInfoPanelPrefab;
    [SerializeField]
    private GameObject animalInfoPanel;

    private GameObject hoveredObject = null;

    private Ray ray;
    private Animal animal = null;

    //private IInteractable objectSelected;

    void Start()
    {
        camera = Camera.main;
        animalInfoPanel.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        SelectingObjects(mousePos);
    }

    private void IsMouseOverAnimal(GameObject obj)
    {
        if (LayerMask.LayerToName(obj.layer).Equals("Animals"))
        {
            obj.GetComponent<Animal>().HasMouseOver = true;
        }
    }

    private void HasMouseExitAnimal(GameObject previousObj, GameObject currentObj)
    {
        
        if (currentObj != previousObj && previousObj != null)
        {
            if (LayerMask.LayerToName(previousObj.layer).Equals("Animals"))
            {
                previousObj.GetComponent<Animal>().HasMouseOver = false;
            }
        }
    }

    private void SelectingObjects(Vector3 mousePosition)
    {
        ray = camera.ScreenPointToRay(mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);
        
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100))
        {

            HasMouseExitAnimal(hoveredObject, hitInfo.collider.gameObject);

            hoveredObject = hitInfo.collider.gameObject;

            IsMouseOverAnimal(hoveredObject);            

            if (Input.GetMouseButtonUp(0))
            {

                if (hoveredObject.GetComponent<Animal>())
                {
                    UnselectAnimal(); //this to prevent last clicked animal to stay selected

                    SelectAnimal(hoveredObject);
                }
                else
                {
                    // if clicked object is not in UI layer deselect it
                    // this to prevent the Animal Info Panel to hide when trying to click a button

                    UnselectAnimal();
                    Debug.Log($"Nothing Selected");
                }
            }
        }        
    }

    private void UnselectAnimal()
    {
        if (animal != null)
        {
            animal.IsSelected = false; //set isSelected false to turn off selection marker
            animal = null; //set reference to null
        }
    }

    private void SelectAnimal(GameObject go)
    {
        if(animal != go.GetComponent<Animal>())
        {
            animal = go.GetComponent<Animal>();
            animal.IsSelected = true;

            animalInfoPanel.GetComponent<AnimalInfoPanel>().Animal = animal;
            animalInfoPanel.SetActive(true);

            Debug.Log($"{animal.GetAnimalData.AnimalType} was selected");
        }        
    }
}
