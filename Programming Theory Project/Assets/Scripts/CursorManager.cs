using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    private new Camera camera;
    //private GameObject AnimalInfoPanelPrefab;
    [SerializeField]
    private GameObject animalInfoPanel;

    private Ray ray;
    private Animal animal = null;

    //private IInteractable objectSelected;

    void Start()
    {
        camera = Camera.main;

        //animalInfoPanel = Instantiate(AnimalInfoPanelPrefab);
        //animalInfoPanel.transform.SetParent(this.transform);
        animalInfoPanel.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        SelectingObjects(mousePos);        
    }

    private void SelectingObjects(Vector3 mousePosition)
    {
        ray = camera.ScreenPointToRay(mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);

        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100))
            {
                GameObject objectSelected = hitInfo.collider.gameObject;

                if(objectSelected == null) 
                {
                    Debug.Log($"nothing selected");
                    return;
                }

                if (objectSelected.GetComponent<Animal>())
                {
                    animal = objectSelected.GetComponent<Animal>();
                    animal.IsSelected = true;

                    Debug.Log($"{animal.GetAnimalData.AnimalType} was selected");
                    
                    animalInfoPanel.GetComponent<AnimalInfoPanel>().AnimalData = animal.GetAnimalData;
                    animalInfoPanel.SetActive(true);
                }
                else
                {
                    Debug.Log($"deselected");
                    
                    animal.IsSelected = false; //set isSelected false to turn off selection marker
                    animal = null; //set reference to null

                    animalInfoPanel.SetActive(false);
                }
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
    //    if (Physics.Raycast(ray, out RaycastHit hitInfo))
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawLine(ray.origin, ray.direction * 150);
    //    }
    //}
}
