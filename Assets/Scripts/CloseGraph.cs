using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseGraph : MonoBehaviour, IPointerClickHandler
{
    private GameObject graphPanel;

    private void Awake()
    {
        graphPanel = this.gameObject;
    }

    //when the user clicks somewhere on the graph interface the gameobjects is set inactive.
    public void OnPointerClick(PointerEventData eventData)
    {
        graphPanel.SetActive(false);
    }
}
