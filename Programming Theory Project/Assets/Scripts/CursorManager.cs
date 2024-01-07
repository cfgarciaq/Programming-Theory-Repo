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


    private IInteractable objectSelected;
    // Start is called before the first frame update
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
        if (Input.GetMouseButtonUp(0))
        {
            SelectingObjects();
        }
    }

    private void SelectingObjects()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.blue);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            objectSelected = hitInfo.collider as IInteractable;
            if (objectSelected != null)
            {
                Debug.Log($"{objectSelected.GetAnimalData.AnimalType} was selected");
                animalInfoPanel.GetComponent<AnimalInfoPanel>().SetAnimalData(objectSelected.GetAnimalData);
                animalInfoPanel.SetActive(true);
            }
            else
            {
                Debug.Log($"deselected");
                animalInfoPanel.SetActive(false);
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
