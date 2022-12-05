using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerupUIControl : MonoBehaviour
{
    // Start is called before the first frame update
    private float lifeTime = 3f;
    private bool active = false;
    public VisualElement Root;
    public Label ItemText;
    public Label DescriptionText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (lifeTime > 0)
            {
                lifeTime -= Time.deltaTime;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Activate(string item, string description)
    {
        Root = GetComponent<UIDocument>().rootVisualElement;

        ItemText = Root.Q<Label>("ItemText");
        DescriptionText = Root.Q<Label>("DescriptionText");

        ItemText.text = item;
        DescriptionText.text = description;

        active = true;
    }
}
