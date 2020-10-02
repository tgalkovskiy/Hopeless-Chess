using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PieceView : MonoBehaviour
{

    Camera camera;

    [SerializeField]
    GameObject frontView;
    [SerializeField]
    GameObject topView;

    Material frontViewMaterial;
    Material topViewMaterial;

    float deltaTopView;

    float topAngle;
    float delta;
    float CA;
    float color;


    // Start is called before the first frame update
    void Start()
    {
        camera = GameModule.instance.MainCamera;
        frontViewMaterial = frontView.GetComponent<MeshRenderer>().material;
        topViewMaterial = topView.GetComponent<MeshRenderer>().material;
        CA = camera.transform.eulerAngles.x;

        topAngle = camera.GetComponent<CameraController>().TransitionAngle;
        delta = camera.GetComponent<CameraController>().DeltatransitionAngle;
        deltaTopView = camera.GetComponent<CameraController>().AngleStartTopViewRotation;
    }

    // Update is called once per frame
    void Update()
    {
        /// Смотрим на камеру
        var cameraProjection = new Vector3(camera.transform.position.x, transform.position.y, camera.transform.position.z);
        var angle = Vector3.Angle(transform.position - cameraProjection, Vector3.back);

        if ((camera.transform.position - transform.position).x < 0) angle = -angle;

        frontView.transform.eulerAngles = new Vector3( frontView.transform.eulerAngles.x, angle, 0 );

        if (Math.Abs(camera.transform.eulerAngles.y) < deltaTopView || Math.Abs(camera.transform.eulerAngles.y) > 360 - deltaTopView)
            topView.transform.eulerAngles = new Vector3(0, 180, 0);
        else if (Math.Abs(camera.transform.eulerAngles.y) > 180 - deltaTopView && Math.Abs(camera.transform.eulerAngles.y) < 180 + deltaTopView)
            topView.transform.eulerAngles = new Vector3(0, 0, 0);

        /// Смотрим на камеру


        /// Смена ракурса

        CA = camera.transform.eulerAngles.x;
        color = Math.Abs(CA - topAngle) / (delta);

        if (CA >= topAngle) frontViewMaterial.color = new Color(0, 0, 0, 0);
        else if (CA < topAngle&& CA > topAngle - delta)
            frontViewMaterial.color = new Color(color,color,color,color);
        else frontViewMaterial.color = new Color(1, 1, 1, 1);

        if (CA <= topAngle - delta) topViewMaterial.color = new Color(0, 0, 0, 0);
        else if (CA < topAngle && CA > topAngle - delta)
            topViewMaterial.color = new Color(1-color,1- color,1- color,1- color);
        else topViewMaterial.color = new Color(1, 1, 1, 1);


        /// Смена ракурса

    }
}
