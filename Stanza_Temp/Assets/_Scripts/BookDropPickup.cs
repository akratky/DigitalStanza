using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookDropPickup : MonoBehaviour
{
    public GameObject dropBookPrefab;
    public GameObject currentBook;  //ref to Book prefab under main camera
    public float bookYoffset;            //y offset of dropped book
    public float bookZoffset;            //z offset of dropped book
    
    private KeyCode _bookDropKey = KeyCode.Space;
    private KeyCode _bookPickUpKey = KeyCode.Space;
    private GameObject _bookInRange;


    // Update is called once per frame
    void Update()
    {
        //if button is pressed and are currently holding book
        if(Input.GetKeyDown(_bookDropKey) && currentBook.activeSelf)
            DropBook();    
        
        if(Input.GetKeyDown(_bookPickUpKey) && _bookInRange)
            PickupBook();
        
    }

    private void DropBook()
    {
        
        //spawn book 
        Vector3 dropBookPos = new Vector3(transform.position.x, transform.position.y + bookYoffset, 
            transform.position.z + bookZoffset);
        GameObject newBook = Instantiate<GameObject>(dropBookPrefab,dropBookPos,Quaternion.identity);
        
        
        //copy info from book in hand to newly spawned book
        ParseIIIF newBookIIIF;
        if(newBook.TryGetComponent<ParseIIIF>(out newBookIIIF))
            newBookIIIF.Copy(currentBook.GetComponent<ParseIIIF>());
        else
            Debug.LogError("No ParseIIIF Script in spawned book");
        
        //hide book in hand (because we dropped it)
        currentBook.SetActive(false);
        
    }

    private void PickupBook()
    {
        //show book in hand
        currentBook.SetActive(true);
        
        //copy data from book on ground
        currentBook.GetComponent<ParseIIIF>().Copy(_bookInRange.GetComponent<ParseIIIF>());
        
        //destroy book on ground
        Destroy(_bookInRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Book"))
        {
            Debug.Log("book in range");
            _bookInRange = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Book"))
        {
            Debug.Log("book out of range");
            _bookInRange = null;
        }
    }
}
