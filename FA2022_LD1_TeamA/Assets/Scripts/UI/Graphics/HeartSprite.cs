using UnityEngine;
using UnityEngine.UIElements;

public class HealthSprite : VisualElement 
{
    public Image Sprite;

    public HealthSprite()
    {
        Sprite = new Image();
        Add(Sprite);

        AddToClassList("HealthSprite");
    }
}