using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conditionalObjectInteract : MonoBehaviour
{
    public float interactionRadius;  // Radius interaksi dengan panda
    public GameObject player;        // Referensi ke pemain
    public GameObject taskItem;        // Referensi ke objek bambu yang harus dibawa
    public GameObject floatingClue;  // Referensi ke objek melayang (clue interaksi)
    private bool isCarryingTheItem = false; // Flag apakah pemain sedang membawa bambu

    void Start()
    {
        // Pastikan objek melayang (clue) tidak aktif di awal
        floatingClue.SetActive(false);
    }

    void Update()
    {
        // Cek apakah pemain sedang membawa bambu (taskItem sebagai child dari player)
        if (taskItem.transform.parent == player.transform)
        {
            isCarryingTheItem = true;
        }
        else
        {
            isCarryingTheItem = false;
        }

        // Menghitung jarak antara pemain dan panda (gameObject ini adalah panda)
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // Tampilkan objek melayang jika pemain membawa bambu dan dekat dengan panda
        if (isCarryingTheItem)
        {
            floatingClue.SetActive(true);  // Menampilkan clue melayang di atas panda
        }
        else
        {
            floatingClue.SetActive(false); // Sembunyikan clue jika pemain terlalu jauh atau tidak membawa bambu
        }

        // Jika jarak dekat dan pemain membawa bambu, tampilkan instruksi untuk interaksi
        if (distance <= interactionRadius && isCarryingTheItem && Input.GetKeyDown(KeyCode.F))
        {
            Interact();  // Fungsi untuk interaksi dengan panda
        }
    }

    void Interact()
    {
        Debug.Log("interaksi objek berhasil");

        Destroy(taskItem);
        Destroy(gameObject);

        // Menghilangkan clue
        floatingClue.SetActive(false);
    }
}