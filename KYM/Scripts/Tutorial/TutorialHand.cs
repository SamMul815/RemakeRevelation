using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TutorialHand : MonoBehaviour {

    //SteamVR_TrackedObject trackedObject;
    SteamVR_RenderModel renderModel;

    [SerializeField]
    public Dictionary<string, MeshRenderer> modelRenderer;

    //[SerializeField]
    public List<string> materialNameList;

    public Material baseMaterial;
    public Material highlightMaterial;

    

private void Start()
    {
        modelRenderer = new Dictionary<string, MeshRenderer>();
        materialNameList = new List<string>();
    }

    private void OnEnable()
    {
        renderModel = GetComponent<SteamVR_RenderModel>();
        SteamVR_Events.RenderModelLoaded.Listen(OnControllerLoaded);
    }

    private void OnDisable()
    {
        renderModel = null;
        SteamVR_Events.RenderModelLoaded.Remove(OnControllerLoaded);
    }

    private void OnAttachedToHand(PlayerHand hand)
    {
        var system = OpenVR.System;
        uint leftIndex = 0;
        uint rightIndex = 0;

        if (system != null)
        {
            leftIndex = system.GetTrackedDeviceIndexForControllerRole(ETrackedControllerRole.LeftHand);
            rightIndex = system.GetTrackedDeviceIndexForControllerRole(ETrackedControllerRole.RightHand);
        }

        if (hand.GetHandType() == PlayerHand.HandType.Right)
        {

            //int rightIndex = system.GetTrackedDeviceIndexForControllerRole(ETrackedControllerRole.RightHand);
            renderModel.SetDeviceIndex((int)rightIndex);
        }
        else if (hand.GetHandType() == PlayerHand.HandType.Left)
        {
            renderModel.SetDeviceIndex((int)leftIndex);
        }
    }

    private void HandAttachedUpdate(PlayerHand hand)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            HighlightOnButton("body");
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            HighlightOffButton("body");
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            HighlightOnButton("lgrip");
            HighlightOnButton("rgrip");
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            HighlightOffButton("lgrip");
            HighlightOffButton("rgrip");
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            HighlightOnButton("trackpad");
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            HighlightOffButton("trackpad");
        }
    }

    void ModelLoaded(Transform modelTransform)
    {
        var meshRenderer = new ArrayList
        {
            modelTransform.Find("body").GetComponent<MeshRenderer>(),
            modelTransform.Find("button").GetComponent<MeshRenderer>(),
            modelTransform.Find("led").GetComponent<MeshRenderer>(),
            modelTransform.Find("lgrip").GetComponent<MeshRenderer>(),
            modelTransform.Find("rgrip").GetComponent<MeshRenderer>(),
            modelTransform.Find("scroll_wheel").GetComponent<MeshRenderer>(),
            modelTransform.Find("sys_button").GetComponent<MeshRenderer>(),
            modelTransform.Find("trackpad").GetComponent<MeshRenderer>(),
            modelTransform.Find("trackpad_scroll_cut").GetComponent<MeshRenderer>(),
            modelTransform.Find("trackpad_touch").GetComponent<MeshRenderer>(),
            modelTransform.Find("trigger").GetComponent<MeshRenderer>()
        };

        foreach (MeshRenderer m in meshRenderer)
        {
            modelRenderer.Add(m.name, m);
            materialNameList.Add(m.name);
        }
        baseMaterial = modelRenderer["body"].material;
    }

    void OnControllerLoaded(SteamVR_RenderModel loadModel, bool success)
    {
        if (!success) return;

        if (renderModel == null) return;

        if (renderModel.index == loadModel.index)
        {
            ModelLoaded(renderModel.gameObject.transform);
        }
    }

    void HighlightOnButton(string objectName)
    {
        modelRenderer[objectName].material = highlightMaterial;
    }

    void HighlightOffButton(string objectName)
    {
        modelRenderer[objectName].material = baseMaterial;
    }

}
