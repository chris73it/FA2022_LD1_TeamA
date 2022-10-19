using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUIControl : MonoBehaviour
{
    public static PlayerUIControl Instance;
    public VisualElement Root;
    public VisualElement HealthGroup;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Root = GetComponent<UIDocument>().rootVisualElement;
        HealthGroup = Root.Q<VisualElement>("HealthGroup");

        InitializeHealth();
    }

    // method to intialize basic heart count
    private void InitializeHealth()
    {
        int currentHealth = GameManager.ChosenPlayerCharacter.GetComponent<Health>().CurrentHealth;
        //Debug.Log("Current Health: " + currentHealth);
        AddHeart(currentHealth);
    }

    // method to take away haert
    public void RemoveHeart(int amount)
    {
       
    }
    // method to add a heart
    public void AddHeart(int amount)
    {
        //Debug.Log("Amount: " + amount);
        for (int i = 0; i < amount; i++)
        {
            HealthSprite Health = new HealthSprite();
            if (HealthGroup.Q<VisualElement>("HealthRowOne").childCount < 5) // 5 is just an example number
            {
                HealthGroup.Q<VisualElement>("HealthRowOne").Add(Health);
            }
            else if (HealthGroup.Q<VisualElement>("HealthRowTwo").childCount < 5)
            {

                HealthGroup.Q<VisualElement>("HealthRowTwo").Add(Health);
            }
            //Debug.Log("Index: " + i);
        }

        //Debug.Log("Child Count: " + HealthGroup.Q<VisualElement>("HealthRowOne").childCount);
    }
}


