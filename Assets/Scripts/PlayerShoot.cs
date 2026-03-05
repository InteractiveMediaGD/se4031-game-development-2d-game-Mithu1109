using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public AudioSource shootSound;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
{
    Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    shootSound.Play();
}
    }
}