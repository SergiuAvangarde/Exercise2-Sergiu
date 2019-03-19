using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseGraph : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject GraphPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        GraphPanel.SetActive(false);
    }
}
