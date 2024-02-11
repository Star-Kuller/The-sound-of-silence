using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadController : MonoBehaviour
{
    [SerializeField] private GameObject _deadScreen;
    [SerializeField] private GameObject _ambientSound;

    void OnCollisionEnter2D(Collision2D col)
    {
        var objectTag = col.gameObject.tag;
        if (objectTag == "Enemy")
        {
            _ambientSound.SetActive(false);
            _deadScreen.SetActive(true);
            StartCoroutine(ExampleCoroutine());
            
        }
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
