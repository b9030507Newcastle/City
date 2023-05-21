using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    public GameObject buildingPrefab;
    public GameObject slotPrefab;
    public GameObject[] modulePrefabs;
    public int cityWidth = 10;
    public int cityLength = 10;
    public float buildingSpacing = 2f;
    public float buildingHeightMin = 1f;
    public float buildingHeightMax = 10f;
    public float extraBuildingChance = 0.1f;


    void Start()
    {
        GenerateCity();
    }

    void GenerateCity()
    {
        for (int x = 0; x < cityWidth; x++)
        {
            for (int z = 0; z < cityLength; z++)
            {
                float height = Random.Range(buildingHeightMin, buildingHeightMax);
                
                Vector3 pos = new Vector3(x * buildingSpacing, height / 2, z * buildingSpacing);
                
                GameObject newBuilding = Instantiate(buildingPrefab, pos, Quaternion.identity);
                
                newBuilding.transform.localScale = new Vector3(1, height, 1);
                
                if (Random.value < extraBuildingChance)
                {
                    float extraHeight = Random.Range(buildingHeightMin, buildingHeightMax);
                    Vector3 extraPos = new Vector3((x + 0.5f) * buildingSpacing, extraHeight / 2, z * buildingSpacing);
                    GameObject extraBuilding = Instantiate(buildingPrefab, extraPos, Quaternion.identity);
                    extraBuilding.transform.localScale = new Vector3(1, extraHeight, 1);
                }
                
                if (Random.value < extraBuildingChance)
                {
                    float extraHeight = Random.Range(buildingHeightMin, buildingHeightMax);
                    Vector3 extraPos = new Vector3(x * buildingSpacing, extraHeight / 2, (z + 0.5f) * buildingSpacing);
                    GameObject extraBuilding = Instantiate(buildingPrefab, extraPos, Quaternion.identity);
                    extraBuilding.transform.localScale = new Vector3(1, extraHeight, 1);
                }
                
                int slots = Random.Range(1, 3);
                for (int i = 0; i < slots; i++)
                {
                    GenerateSlot(newBuilding);
                }
            }
        }
    }

    void GenerateSlot(GameObject building)
    {
        int direction = Random.Range(0, 4);
        Vector3 slotPos;
        Quaternion slotRot;
        switch (direction)
        {
            case 0: // front
                slotPos = building.transform.position + new Vector3(0, 0, 0.5f) * building.transform.localScale.z;
                slotRot = Quaternion.Euler(0, 180, 0);
                break;
            case 1: // back
                slotPos = building.transform.position + new Vector3(0, 0, -0.5f) * building.transform.localScale.z;
                slotRot = Quaternion.Euler(0, 0, 0);
                break;
            case 2: // left
                slotPos = building.transform.position + new Vector3(-0.5f, 0, 0) * building.transform.localScale.x;
                slotRot = Quaternion.Euler(0, 90, 0);
                break;
            default: // right
                slotPos = building.transform.position + new Vector3(0.5f, 0, 0) * building.transform.localScale.x;
                slotRot = Quaternion.Euler(0, -90, 0);
                break;
        }

        Vector3 modulePos = slotPos;
        Quaternion moduleRot = slotRot;
        GameObject modulePrefab = modulePrefabs[Random.Range(0, modulePrefabs.Length)];
        GameObject module = Instantiate(modulePrefab, modulePos, moduleRot);
        
        module.transform.parent = building.transform;
    }
}
