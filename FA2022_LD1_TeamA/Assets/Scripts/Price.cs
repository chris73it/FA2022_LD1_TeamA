using UnityEngine;
public struct Price // switch this to deriving from MonoBehaviour, and then have Powerup and Pickup derive from this
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

    /*
     public void CreatePriceTag(int amount)
    {
        GameObject p = Instantiate(GameManager.Instance.TextObject, this.gameObject.transform, this.gameObject.rotation);
        p.getComponent<TextMesh>.text = "" + amount + "¢";
    }
     */
}
