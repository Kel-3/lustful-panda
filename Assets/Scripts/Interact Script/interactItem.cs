using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactItem : MonoBehaviour
{
    public float pickupRadius;
    private bool canPickup = false;
    private bool isCarryingItem = false;
    public GameObject player;
    public GameObject obstacleObject; 
    public Vector3 grabOffsetPlayer; // jarak objek setelah diambil karakter
    public Vector3 dropOffsetPlayer; // jarak objek setelah ditaro karakter

    void Start()
    {

    }

    void Update()
    {
        // Menghitung jarak antara pemain dan objek
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // Mengecek apakah jarak pemain kurang dari radius pickup
        if (distance <= pickupRadius)
        {
            if (obstacleObject != null)
        {
            Debug.Log("belum bisa interact");
            return; // Keluar dari fungsi Update jika panda masih ada
        }
            else{
                canPickup = true;
            Debug.Log("ambil barang = E");
            }
            
        }
        else{
            canPickup = false;
        }

        // Jika berada dalam radius dan tombol "E" ditekan, ambil objek
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();  // Fungsi untuk mengambil objek
        }

        // Jika pemain menekan tombol "R", drop objek
        if (isCarryingItem && Input.GetKeyDown(KeyCode.R))
        {
            Drop();  // Fungsi untuk drop objek
        }
    }

    void Pickup()
    {
        Debug.Log("membawa barang");
        // Kamu bisa menambahkan logika lain di sini, seperti menambahkannya ke inventory
        
        // Memindahkan objek ke dekat karakter dengan offset
        transform.position = player.transform.position + grabOffsetPlayer;

        // Menjadikan objek anak dari pemain, sehingga mengikuti pemain jika diperlukan
        transform.SetParent(player.transform);

        isCarryingItem = true;  // Menandakan bahwa pemain sedang membawa objek
        canPickup = false;      // Tidak bisa mengambil objek lagi sampai dilepaskan
    }

    void Drop()
    {
        Debug.Log("barang dilepas");
        // Memindahkan objek ke posisi pemain saat ini
        transform.position = player.transform.position + dropOffsetPlayer;

        // Menghapus objek sebagai anak dari pemain (tidak mengikuti pemain lagi)
        transform.SetParent(null);

        isCarryingItem = false;  // Menandakan bahwa pemain tidak lagi membawa objek
        canPickup = true;        // Pemain bisa mengambil objek kembali
    }
}
