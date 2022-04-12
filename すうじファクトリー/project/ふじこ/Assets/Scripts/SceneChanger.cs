using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    Animation changers;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1024, 768, false, 60);
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Title");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TitleToFactory()
    {
        StartCoroutine(FactoryChange());
    }

    public void TitleToPizza()
    {
        StartCoroutine(PizzaChange());
    }

    public void FactoryToTitle()
    {
        StartCoroutine(FactoryChange2());
    }

    public void PizzaToTitle()
    {
        StartCoroutine(PizzaChange2());
    }

    IEnumerator FactoryChange()
    {
        changers.Play("SceneChange1");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SampleScene");
        changers.Play("SceneChange1_2");

    }

    IEnumerator PizzaChange()
    {
        BGMManager.d_BGM = true;
        changers.Play("SceneChange2");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("pizzadoreda");
        changers.Play("SceneChange2_2");
    }

    IEnumerator FactoryChange2()
    {
        changers.Play("SceneChange1");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Title");
        changers.Play("SceneChange1_2");

    }

    IEnumerator PizzaChange2()
    {
        BGMManager.f_BGM = true;
        changers.Play("SceneChange2");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Title");
        changers.Play("SceneChange2_2");
    }
}
