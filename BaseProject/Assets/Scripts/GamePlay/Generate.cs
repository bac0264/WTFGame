using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    public ObjectPooling objPoolingMyCharacterTest;
    public ObjectPooling objPoolingEnemy;
    public Transform Mybuilding;
    public Transform EnemyBuilding;
    //private void Awake()
    //{
    //    if (objPoolingMyCharacterTest == null)
    //        objPoolingMyCharacterTest = GetComponent<ObjectPooling>();
    //}
    //private void OnValidate()
    //{
    //    if (objPoolingMyCharacterTest == null)
    //        objPoolingMyCharacterTest = GetComponent<ObjectPooling>();
    //}
    public void GenerateEnemyLoops ()
    {
        GameObject obj = objPoolingEnemy.getObjectPooling();
       // obj.transform.SetParent(EnemyBuilding);
        obj.transform.position = EnemyBuilding.position;
    }
    public void GenerateMyLoops()
    {
        GameObject obj = objPoolingMyCharacterTest.getObjectPooling();
        //obj.transform.SetParent(Mybuilding);
        obj.transform.position = Mybuilding.position;
        GenerateEnemyLoops();
    }
}
