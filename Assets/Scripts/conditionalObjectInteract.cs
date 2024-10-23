using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conditionalObjectInteract : MonoBehaviour
{
    public float interactionRadius; // Radius interaksi
    public GameObject player;
    public GameObject taskItem; // Referensi ke item quest
    private bool isCarryingTheItem = false;

    void Start()
    {

    }

    void Update()
    {
        // Check bawaan item
        if (taskItem.transform.parent == player.transform)
        {
            isCarryingTheItem = true;
        }
        else
        {
            isCarryingTheItem = false;
        }

        // Menghitung jarak pemain n objek
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= interactionRadius && isCarryingTheItem && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    void Interact()
    {
        Debug.Log("interaksi objek berhasil");

        Destroy(taskItem);
        Destroy(gameObject);
    }
}