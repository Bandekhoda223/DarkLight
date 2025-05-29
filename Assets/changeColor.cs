using UnityEngine;

public class changeColor : MonoBehaviour
{
    public Color newColor = Color.red;

    void Start()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.color = newColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
