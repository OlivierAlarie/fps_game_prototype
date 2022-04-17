using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Script_de_lamie : MonoBehaviour
{
    private GameObject _thePlayer;
    [SerializeField] private string _laPremiereChoseQuElleVaDire;
    [SerializeField] private TextMeshProUGUI _leTexteDuDialogue;
    [SerializeField] private string _laDeuxiemeChoseQuElleVaDire;
    private GameObject _laBulleDeDialogue;
    [SerializeField] private Transform _leSpawnDuPtitJus;
    [SerializeField] private Transform _leSpawnDuPtitlait;
    private bool _jyAiParler;
    [SerializeField] private GameObject _lesPtitsJusCestPourLesLunchs;
    [SerializeField] private GameObject _lesPtitsLaitCestCorrect;
    private bool _jeSuisEnTrainDyParler;
    [SerializeField] private bool _onVeutQuelleDropDuLait;

    // Start is called before the first frame update
    void Start()
    {
        _thePlayer = GameObject.FindGameObjectWithTag("Player");
        _laBulleDeDialogue = GameObject.Find("TextBubble");
        _laBulleDeDialogue.SetActive(false);
       
    }

    /*1. mesurer la distance entre l'amie et le player
     *2. Si distance <= X, le reste du code embarque sinon rien se passe
     *3. Un String public qu'on doit accéder pour le speech de la fille.
     *4. Un IENUMERATOR Coroutine qui fait en sorte que le text soit lisible assez longtemps.
     *5. après la coroutine, le text disparait.
     *6. Après que le text disparaisse, le jus spawn au spawn de jus.
     *
     *1 coroutine
     *if else premier et deuxieme text
     *bool pendant que le text apparait.
     * */

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, _thePlayer.transform.position) <= 5f) 
        {
            if (_jeSuisEnTrainDyParler == false) 
            { 
                _laBulleDeDialogue.SetActive(true);
                 if (_jyAiParler == false)
                 {
                   _leTexteDuDialogue.text = _laPremiereChoseQuElleVaDire;
                 StartCoroutine("LeTexteApparaitPourLaPremiereFois");
                 } else
                 {
                   _leTexteDuDialogue.text = _laDeuxiemeChoseQuElleVaDire;
                  StartCoroutine("LeTexteApparaitPourLaDeuxiemeFois");
                 }
                _jeSuisEnTrainDyParler = true;
            }

        }
    }
    
    IEnumerator LeTexteApparaitPourLaPremiereFois()
    {

        yield return new WaitForSeconds(5.0f);
        if (_jyAiParler == false)
        {
            Instantiate(_lesPtitsJusCestPourLesLunchs, _leSpawnDuPtitJus.transform.position, _leSpawnDuPtitJus.transform.rotation);
            if (_onVeutQuelleDropDuLait == true)
            {
                Instantiate(_lesPtitsLaitCestCorrect, _leSpawnDuPtitlait.transform.position, _leSpawnDuPtitlait.transform.rotation);
            }
        }
        _jyAiParler = true;
        _laBulleDeDialogue.SetActive(false);
        _jeSuisEnTrainDyParler = false;
        
    }

    IEnumerator LeTexteApparaitPourLaDeuxiemeFois()
    {

        yield return new WaitForSeconds(5.0f);
        _laBulleDeDialogue.SetActive(false);
        _jeSuisEnTrainDyParler = false;
    }
}
