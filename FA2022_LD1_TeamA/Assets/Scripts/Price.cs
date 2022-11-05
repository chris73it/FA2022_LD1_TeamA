using UnityEngine;
public struct Price
{
    public int Cost;

    public bool CanPlayerAfford()
    {
        if (GameManager.ChosenPlayerCharacter.GetComponent<Wealth>().Money - Cost >= 0)
        {
            GameManager.ChosenPlayerCharacter.GetComponent<Wealth>().Withdraw(Cost);
            return true;
        }

        Debug.Log("Cost: " + Cost);
        return false;
    }
}
