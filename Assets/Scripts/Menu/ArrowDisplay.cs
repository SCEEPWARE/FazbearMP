using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowDisplay : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private GameObject selector; // Les flŠches qui repr‚sentent le s‚lecteur
    [SerializeField] AudioClip buttonClip; // se joue dŠs que le pointeur se d‚place
    private MenuManager instance;

    void Start(){
        instance = MenuManager.instance;
    }
    
    // Affiche la flŠche si le pointeur passe sur le bouton
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(selector.transform.position != transform.position){ instance.fxAudio.PlayOneShot(buttonClip, 0.7f);}
        selector.transform.position = transform.position; // On met le s‚lecteur … la position du bouton (le d‚calage est d‚j… pris en compte dans le texte)
    }
}
