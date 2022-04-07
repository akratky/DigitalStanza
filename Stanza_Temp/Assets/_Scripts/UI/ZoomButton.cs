using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomButton : MonoBehaviour
{

    private Image _image;
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        
        
    }

    public void ToggleImage()
    {
        _image.enabled = !_image.IsActive();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
