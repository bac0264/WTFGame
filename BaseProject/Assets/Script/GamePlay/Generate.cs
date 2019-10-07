using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    public ObjectPooling objPoolingMyCharacter;
    public ObjectPooling objPoolingEnemy;
    public Transform Mybuilding;
    public Transform EnemyBuilding;
    //private void Awake()
    //{
    //    if (objPoolingMyCharacter == null)
    //        objPoolingMyCharacter = GetComponent<ObjectPooling>();
    //}
    //private void OnValidate()
    //{
    //    if (objPoolingMyCharacter == null)
    //        objPoolingMyCharacter = GetComponent<ObjectPooling>();
    //}
    public void GenerateEnemyLoops ()
    {
        GameObject obj = objPoolingEnemy.getObjectPooling();
       // obj.transform.SetParent(EnemyBuilding);
        obj.transform.position = EnemyBuilding.position;
    }
    public void GenerateMyLoops()
    {
        GameObject obj = objPoolingMyCharacter.getObjectPooling();
        //obj.transform.SetParent(Mybuilding);
        obj.transform.position = Mybuilding.position;
        GenerateEnemyLoops();
    }
}
