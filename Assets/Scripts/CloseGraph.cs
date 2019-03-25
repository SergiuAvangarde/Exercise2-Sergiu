using UnityEngine;
using UnityEngine.EventSystems;

public class CloseGraph : MonoBehaviour, IPointerClickHandler
{
    private GameObject graphPanel;

    private void Awake()
    {
        graphPanel = this.gameObject;
    }

    /// <summary>
    /// when the user clicks somewhere on the graph interface the gameobjects is set inactive
    /// </summary>
    /// <param name="eventData"> checks for the click event </param>
    public void OnPointerClick(PointerEventData eventData)
    {
        graphPanel.SetActive(false);
    }
}
