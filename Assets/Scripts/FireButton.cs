using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // ==============================================================================================
    private Player _player;
    private bool _fireNow;


    // ==============================================================================================
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        _fireNow = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        _fireNow = false;
    }

    protected virtual void Start()
    {
        _player = FindObjectOfType<Player>();
    }
    
    protected virtual void Update()
    {
        if (_fireNow && _player != null)
            _player.Fire();
    }
}
