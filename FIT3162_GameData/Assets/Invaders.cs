using UnityEngine;
using System.Collections;

public class Invaders : MonoBehaviour
{
    [Header("Prefabs & Layout")]
    public Invader[] prefabs;              // invader types
    public int rows = 5;
    public int columns = 11;
    public float spacing = 2.0f;
    public float yOffset = 3.0f;

    [Header("Movement")]
    public float speed = 1.0f;
    private Vector3 _direction = Vector2.right;

    [Header("Shooting")]
    public GameObject invaderBulletPrefab; // assign your enemy_projectile prefab here
    public float minFireInterval = 2f;
    public float maxFireInterval = 5f;

    void Start()
    {
        GenerateGrid();
        StartCoroutine(InvaderShootingRoutine());
    }

    void Update()
    {
        if (GameController.gameIsOver) return;

        // Move entire group
        transform.position += _direction * speed * Time.deltaTime;

        // Check edges
        Vector3 leftEdge  = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform inv in transform)
        {
            if (!inv.gameObject.activeInHierarchy) continue;

            if ((_direction == Vector3.right  && inv.position.x >= rightEdge.x  - spacing) ||
                (_direction == Vector3.left   && inv.position.x <= leftEdge.x  + spacing))
            {
                AdvanceRow();
                break;
            }
        }
    }

    void GenerateGrid()
    {
        float width  = spacing * (columns - 1);
        float height = spacing * (rows    - 1);
        Vector2 centering = new Vector2(-width / 2, -height / 2);

        for (int row = 0; row < rows; row++)
        {
            Vector3 rowPos = new Vector3(centering.x, centering.y + row * spacing + yOffset, 0f);

            for (int col = 0; col < columns; col++)
            {
                // cycle through your prefab array
                Invader prefab = prefabs[row % prefabs.Length];
                Invader inv = Instantiate(prefab, rowPos + Vector3.right * col * spacing, Quaternion.identity, transform);
                inv.name = $"Invader_{row}_{col}";
            }
        }
    }

    void AdvanceRow()
    {
        _direction.x *= -1f;
        Vector3 pos = transform.position;
        pos.y -= spacing * 0.5f;
        transform.position = pos;
    }

    private IEnumerator InvaderShootingRoutine()
    {
        while (!GameController.gameIsOver)
        {
            yield return new WaitForSeconds(Random.Range(minFireInterval, maxFireInterval));

            // choose a random column
            int col = Random.Range(0, columns);
            Invader shooter = null;

            // scan from bottom up for a live invader
            for (int row = 0; row < rows; row++)
            {
                int idx = row * columns + col;
                if (idx < transform.childCount)
                {
                    Transform t = transform.GetChild(idx);
                    if (t.gameObject.activeInHierarchy)
                    {
                        shooter = t.GetComponent<Invader>();
                    }
                }
            }

            if (shooter != null)
            {
                // fire your existing prefab
                Instantiate(invaderBulletPrefab, shooter.transform.position, Quaternion.identity);
            }
        }
    }
}
