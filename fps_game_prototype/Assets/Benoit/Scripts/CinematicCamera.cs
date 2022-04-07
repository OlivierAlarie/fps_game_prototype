using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicCamera : MonoBehaviour
{
    [Header("La Camera")]
    [SerializeField]
    private GameObject _laCamera;
    [Header("Movement Variables")]
    [SerializeField]
    private float _speed = 5.0f;
    private int _thePositionsIndex = 0;
    private Transform _theObjective = null;
    [Header("*********ALLEZ LIRE LE SCRIP*******")]
    /*Saluse ! Donc le prefab est assez versatile, la camera va aller de la première position du array vers la prochaine et ainsi de suite jusqu'à la dernière et rendu à la derniere elle va changer de scene. 
     * vous pouvez rajouter des positions et en enlever à votre guise. Pour savoir vers ou la position pointe il faut changer le transform dans l'editor (dans la fenetre "Scene") pour voir les LOCALS positions,
     * puis la petite flèche bleue va pointer vers ou elle regarde. j'en parlerai au meeting.
     * 
     */
    [Header("Les positions")]
    [SerializeField]
    private Transform[] _thePositions;

    
    private void Start()
    {
           
            _theObjective = _thePositions[_thePositionsIndex];
        
    }

    
    private void Update()
    {
        Move();
        Rotate();

        if (_thePositionsIndex == _thePositions.Length-1)
        {
            if (Vector3.Distance(_laCamera.transform.position, _theObjective.position) <= 0.1)
            {
                //SceneManager.LoadScene(scenename);
            }

        }
    }

    
    private void Move()
    {
        _laCamera.transform.position = Vector3.MoveTowards(_laCamera.transform.position, _theObjective.position, _speed * Time.deltaTime);

        if (Vector3.Distance(_laCamera.transform.position, _theObjective.position) <= 0.1)
        {
            _thePositionsIndex++;
            _theObjective = _thePositions[_thePositionsIndex];
        }
    }
    private void Rotate()
    {
        _laCamera.transform.rotation = Quaternion.Lerp(_laCamera.transform.rotation, _theObjective.transform.rotation, 0.5f * Time.deltaTime);
    }
}
