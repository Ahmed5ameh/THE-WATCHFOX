using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowControls : MonoBehaviour
{
    [SerializeField] GameObject ObjectToHide;
    [SerializeField] GameObject ObjectToShow;
    public void ShowGameControls()
    {
        ObjectToHide.SetActive(false);
        ObjectToShow.SetActive(true);
    }

}
