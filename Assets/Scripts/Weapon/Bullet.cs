using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 120;
    public float lifeTime = 5;
    public float distance = 10;
    public int damage = 1;
    public float randomDirectAngle = 4.0f;
    public LayerMask targetLayer;

    public GameObject destroyEffectPrefab;

    private void Start()
    {
        Invoke("DestroyBullet", lifeTime);
        transform.Rotate(new Vector3(0, 0, Random.Range(-randomDirectAngle, randomDirectAngle)), Space.World);
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, targetLayer);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                int finalDanamge = Random.Range(1, 5) == 3? 2 * damage : damage;
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(finalDanamge, transform.rotation.eulerAngles.z > 180? 1 : -1, finalDanamge == (2 * damage));
            }

            DestroyBullet();
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void DestroyBullet()
    {
        GameObject destoryEffect = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        Destroy(destoryEffect, 0.1f);
        Destroy(gameObject);
    }
}
