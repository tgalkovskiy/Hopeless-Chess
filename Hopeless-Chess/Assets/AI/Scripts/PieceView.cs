using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class PieceView : MonoBehaviour
{

    Camera camera;

    [SerializeField]
    GameObject frontView;
    [SerializeField]
    GameObject topView;
    [Space]
    [SerializeField]
    GameObject frontMoralityBar;
    [SerializeField]
    GameObject topMoralityBar;
    [Space]
    [SerializeField]
    GameObject frontMoralityBarScale;
    [SerializeField]
    GameObject topMoralityBarScale;
    [Space]
    [SerializeField]
    GameObject text;



    List<Material> frontList;
    List<Material> topList;


    float deltaTopView;

    float topAngle;
    float delta;
    float CA;
    float color;


    // Start is called before the first frame update
    void Start()
    {
        camera = GameModule.instance.MainCamera;

        frontList = new List<Material>();
        topList = new List<Material>();

        frontList.Add(frontView.GetComponent<MeshRenderer>().material);
        frontList.Add(frontMoralityBar.GetComponent<MeshRenderer>().material);
        frontList.Add(frontMoralityBarScale.GetComponent<MeshRenderer>().material);

        topList.Add(topView.GetComponent<MeshRenderer>().material);
        topList.Add(topMoralityBar.GetComponent<MeshRenderer>().material);
        topList.Add(topMoralityBarScale.GetComponent<MeshRenderer>().material);

        CA = camera.transform.eulerAngles.x;

        topAngle = camera.GetComponent<CameraController>().TransitionAngle;
        delta = camera.GetComponent<CameraController>().DeltatransitionAngle;
        deltaTopView = camera.GetComponent<CameraController>().AngleStartTopViewRotation;

    }

    // Update is called once per frame
    void Update()
    {
        /// Смотрим на камеру фигуры
        var cameraProjection = new Vector3(camera.transform.position.x, transform.position.y, camera.transform.position.z);
        var angle = Vector3.Angle(transform.position - cameraProjection, Vector3.back);

        if ((camera.transform.position - transform.position).x < 0) angle = -angle;

        frontView.transform.eulerAngles = new Vector3(frontView.transform.eulerAngles.x, angle, 0);
        frontMoralityBar.transform.eulerAngles = new Vector3(frontView.transform.eulerAngles.x, angle, 0);
        text.transform.eulerAngles = new Vector3(0, 180 + angle, 0);

        if (Math.Abs(camera.transform.eulerAngles.y) < deltaTopView || Math.Abs(camera.transform.eulerAngles.y) > 360 - deltaTopView)
            topView.transform.eulerAngles = new Vector3(0, 180, 0);
        else if (Math.Abs(camera.transform.eulerAngles.y) > 180 - deltaTopView && Math.Abs(camera.transform.eulerAngles.y) < 180 + deltaTopView)
            topView.transform.eulerAngles = new Vector3(0, 0, 0);
        /// Смотрим на камеру фигуры

        /// Смена видимости при смене ракурса
        CA = camera.transform.eulerAngles.x;
        color = Math.Abs(CA - topAngle) / (delta);

        if (CA >= topAngle) foreach (var item in frontList) item.color = new Color(0, 0, 0, 0);
        else if (CA < topAngle && CA > topAngle - delta)
            foreach (var item in frontList) item.color = new Color(color, color, color, color);
        else foreach(var item in frontList) item.color = new Color(1, 1, 1, 1);

        if (CA <= topAngle - delta) foreach (var item in topList) item.color = new Color(0, 0, 0, 0); 
        else if (CA < topAngle && CA > topAngle - delta)
            foreach (var item in topList) item.color = new Color(1-color, 1 - color, 1 - color, 1 - color);
        else foreach(var item in topList) item.color = new Color(1, 1, 1, 1);
        /// Смена видимости при смене ракурса

        if (Input.GetKey(KeyCode.LeftControl))
		{
            frontMoralityBar.SetActive(true);
            topMoralityBar.SetActive(true);
        }
        else
		{
            frontMoralityBar.SetActive(false);
            topMoralityBar.SetActive(false);
        }
    }
    
    /// <summary>
    /// Всплывает цифра.
    /// </summary>
    /// <param name="delta"></param>
    public void ShowChangeMorality(float delta)
	{
        //Debug.Log(delta);
       
        text.SetActive(true);
        text.GetComponent<MeshRenderer>().enabled = true;
        //text.GetComponent<MeshRenderer>().material = GameModule.instance.Materials[1] ;
        text.GetComponent<TextMeshPro>().text = delta.ToString();
        text.GetComponent<Animation>().Play();

        //ChangeMoralityBar();
    }

    public void ChangeMoralityBar()
	{
        var morality = gameObject.GetComponent<CharacterController>().moralityCount;
        var moralityMax = gameObject.GetComponent<CharacterController>().character.MaxMorality;
        frontMoralityBarScale.transform.localScale =
            new Vector3(morality/ moralityMax * 0.9f,
            frontMoralityBarScale.transform.localScale.y,
            frontMoralityBarScale.transform.localScale.z);
        topMoralityBarScale.transform.localScale =
            new Vector3(morality / moralityMax * 0.9f,
            topMoralityBarScale.transform.localScale.y,
            topMoralityBarScale.transform.localScale.z);
    }
}
