using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public LayerMask enemyMask;
    
    private Camera cam;
    private float distance = 50.0f;
    
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (GameManager.Instance.GameState != GameState.Pause)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);
                Ray ray = cam.ScreenPointToRay(point);
                RaycastHit hitObject;
               
                if (Physics.Raycast(ray, out hitObject, distance, enemyMask)) 
                {
                    EnemyManager.Instance.DestroyEnemyObject(hitObject.transform.gameObject);
                    GameManager.Instance.score++;
                }
            }
        }
    }

    private void OnGUI()
    {
        int size = 12;
        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 4;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}
