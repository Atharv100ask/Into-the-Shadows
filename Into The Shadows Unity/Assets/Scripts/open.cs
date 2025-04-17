using UnityEngine;

public class open : MonoBehaviour
{
    [SerializeField] private GameObject panelToOpen;

    public void HidePanel()
    {
        if (panelToOpen != null)
        {
            panelToOpen.SetActive(true);
        }
    }
}
