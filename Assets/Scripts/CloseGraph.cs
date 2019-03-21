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

    public void OnPointerClick(PointerEventData eventData)
    {
        graphPanel.SetActive(false);
    }
}
