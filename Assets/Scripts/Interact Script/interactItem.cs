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
                return; // Keluar dari fungsi Update jika obstacleObject masih ada
            }
            else
            {
                canPickup = true;
                Debug.Log("ambil barang = E");
            }
        }
        else
        {
            canPickup = false;
        }

        // Jika berada dalam radius dan tombol "E" ditekan, ambil objek
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            if (!isCarryingItem)
            {
                Pickup();
            }
        }

        // Jika pemain menekan tombol "R", drop objek
        if (isCarryingItem && Input.GetKeyDown(KeyCode.R))
        {
            Drop();
        }
    }

    void Pickup()
    {
        Debug.Log("membawa barang");

        // Memindahkan objek ke dekat karakter dengan arah yang sesuai (menggunakan rotasi karakter)
        transform.position = player.transform.TransformPoint(grabOffsetPlayer);

        // Reset rotasi objek hanya pada sumbu Y 
        float yPlayer = player.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, yPlayer, 0);

        // Menjadikan objek anak dari pemain, sehingga mengikuti pemain jika diperlukan
        transform.SetParent(player.transform);

        isCarryingItem = true;  // Menandakan bahwa pemain sedang membawa objek
        canPickup = false;      // Tidak bisa mengambil objek lagi sampai dilepaskan
    }


    void Drop()
    {
        Debug.Log("barang dilepas");

        // Memindahkan objek ke posisi pemain saat ini dengan arah yang sesuai (menggunakan rotasi karakter)
        transform.position = player.transform.TransformPoint(dropOffsetPlayer);

        // Reset rotasi objek hanya pada sumbu Y dan gunakan localRotation
        transform.localRotation = Quaternion.Euler(0, 0, 0);

        // Menghapus objek sebagai anak dari pemain (tidak mengikuti pemain lagi)
        transform.SetParent(null);

        isCarryingItem = false;  // Menandakan bahwa pemain tidak lagi membawa objek
        canPickup = true;        // Pemain bisa mengambil objek kembali
    }
}