using UnityEngine;

public class PowerupButton : MonoBehaviour
{
    public PowerupType powerupType;

    public void ActivatePowerup()
    {
        PowerupManager.Instance.SetActivePowerup(powerupType);
    }
}
