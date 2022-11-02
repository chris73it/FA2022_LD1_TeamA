public struct Price
{
    public int Cost;

    public bool CanPlayerAfford()
    {
        return GameManager.ChosenPlayerCharacter.GetComponent<Wealth>().Money - Cost >= 0 ? true : false;
    }
}
