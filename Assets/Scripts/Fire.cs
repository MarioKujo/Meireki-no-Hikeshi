using UnityEngine;
public class Fire : MonoBehaviour
{
    [SerializeField]
    float spawnTime;
    BoxCollider2D box;
    SpriteRenderer sr;
    float timer;
    private void Awake()
    {
        timer = spawnTime;
        box = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (!box.isActiveAndEnabled && !sr.isVisible)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                box.enabled = true;
                sr.enabled = true;
                timer = spawnTime;
            }
        }
    }
}
