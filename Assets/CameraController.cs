using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset = 0.8f;
    public float offsetSmooth = 0.1f;
    public SpriteRenderer background; // بک‌گراند را اینجا بده
        public GameObject backgroundsParent; // آبجکت والد بک‌گراندها
    private Vector3 playerPos;

    private float minX, maxX, minY, maxY;

    private Bounds GetBackgroundsBounds()
    {
        var renderers = backgroundsParent.GetComponentsInChildren<SpriteRenderer>();
        if (renderers.Length == 0) return new Bounds(Vector3.zero, Vector3.zero);

        Bounds bounds = renderers[0].bounds;
        foreach (var r in renderers)
            bounds.Encapsulate(r.bounds);
        return bounds;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (backgroundsParent != null)
        {
            float camHeight = Camera.main.orthographicSize;
            float camWidth = camHeight * Camera.main.aspect;

            Bounds bgBounds = GetBackgroundsBounds();

            minX = bgBounds.min.x + camWidth;
            maxX = bgBounds.max.x - camWidth;
            minY = bgBounds.min.y + camHeight;
            maxY = bgBounds.max.y - camHeight;
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        if (player.transform.localScale.x > 0)
        {
            playerPos = new Vector3(playerPos.x + offset, playerPos.y, transform.position.z);
        }
        else
        {
            playerPos = new Vector3(playerPos.x - offset, playerPos.y, transform.position.z);
        }

        // محدود کردن موقعیت دوربین به محدوده بک‌گراند
        playerPos.x = Mathf.Clamp(playerPos.x, minX, maxX);
        playerPos.y = Mathf.Clamp(playerPos.y, minY, maxY);

        transform.position = Vector3.Lerp(transform.position, playerPos, offsetSmooth * Time.deltaTime);
    }
}
