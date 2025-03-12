using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private GameObject poolPrefab;
    private List<GameObject> listGameObject;

    private GameObject tempCreatePoolObj;
    private GameObject tempGetPoolObj;

    public ObjectPool(GameObject prefab, int prewarmCountObjects)
    {
        poolPrefab = prefab;
        listGameObject = new List<GameObject>();

        for (int i = 0; i < prewarmCountObjects; i++)
        {
            tempCreatePoolObj = null;
            tempCreatePoolObj = Create();
            tempCreatePoolObj.SetActive(false);
        }
    }

    private GameObject Create()
    {
        tempCreatePoolObj = Instantiate(poolPrefab);
        listGameObject.Add(tempCreatePoolObj);
        return tempCreatePoolObj;
    }

    public GameObject Get()
    {
        tempGetPoolObj = null;
        foreach (GameObject go in listGameObject)
        {
            if (!go.activeSelf)
            {
                tempGetPoolObj = go;
                break;
            }
        }

        if (tempGetPoolObj == null)
            tempGetPoolObj = Create();

        tempGetPoolObj.SetActive(true);

        return tempGetPoolObj;
    }

    public void Release(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
