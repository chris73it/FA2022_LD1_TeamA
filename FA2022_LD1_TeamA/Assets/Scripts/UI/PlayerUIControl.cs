using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUIControl : MonoBehaviour
{
    public static PlayerUIControl Instance;
    public VisualElement Root;
    public VisualElement HealthGroup;
    public ProgressBar StaminaBar;
    public Label CoinAmount;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Root = GetComponent<UIDocument>().rootVisualElement;
        HealthGroup = Root.Q<VisualElement>("HealthGroup");
        StaminaBar = Root.Q<ProgressBar>("StaminaBar");
        CoinAmount = Root.Q<Label>("CoinAmount");
    }

    // method to intialize basic heart count
    public void InitializeHealth()
    {
        int currentHealth = GameManager.ChosenPlayerCharacter.GetComponent<Health>().CurrentHealth;
        //Debug.Log("Current Health: " + currentHealth);
        RemoveHeart(20);
        AddHeart(currentHealth);
    }

    // method to take away haert
    public void RemoveHeart(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (HealthGroup.Q<VisualElement>("HealthRowTwo").childCount >= 1)
            {
                HealthGroup.Q<VisualElement>("HealthRowTwo").RemoveAt(HealthGroup.Q<VisualElement>("HealthRowTwo").childCount - 1);
            }
            else if (HealthGroup.Q<VisualElement>("HealthRowOne").childCount >= 1)
            {
                HealthGroup.Q<VisualElement>("HealthRowOne").RemoveAt(HealthGroup.Q<VisualElement>("HealthRowOne").childCount - 1);
            }
        }
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

    public void UpdateStamina(float currentStamina, float maxStamina)
    {
        StaminaBar.value = currentStamina / maxStamina;
    }

    public void UpdateCoin(int money)
    {
        CoinAmount.text = "" + money;
    }
}


