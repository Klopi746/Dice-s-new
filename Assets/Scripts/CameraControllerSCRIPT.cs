using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class CameraControllerSCRIPT : MonoBehaviour
{
    [Header("Camera Positions")]
    public Vector3 position1 = new Vector3(0, 0, 0);
    public Vector3 rotation1 = new Vector3(18, 0, 0);
    public Vector3 position2 = new Vector3(0, -5, 15);
    public Vector3 rotation2 = new Vector3(60, 0, 0);

    public float transitionSpeed = 0.5f;

    [Header("UI Elements")]
    public Button targetButton;


    [Header("Misc")]
    public bool actionOverride = false;
    private bool isAtPos2 = false;

    public static CameraControllerSCRIPT Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SetCamera(position1, rotation1);
        targetButton.interactable = false;
    }

    void Update()
    {
        if (actionOverride) return;
        if (Input.GetKeyDown(KeyCode.W) && !isAtPos2)
        {
            SetCamera(position2, rotation2);
            WaitforMovementEnd();
            isAtPos2 = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && isAtPos2)
        {
            SetCamera(position1, rotation1);
            targetButton.interactable = false;
            isAtPos2 = false;
        }
    }

    void SetCamera(Vector3 position, Vector3 rotation)
    {
        transform.DOMove(position, transitionSpeed);
        transform.DORotate(rotation, transitionSpeed);
    }


    private IEnumerator WaitforMovementEnd()
    {
        yield return new WaitForSeconds(transitionSpeed);
        targetButton.interactable = true;
    }

    public void SetCloseCamView()
    {
        SetCamera(position2, rotation2);
    }

    public void SetFarCamView()
    {
        SetCamera(position1, rotation1);
    }

    public void ChangeLockOverride()
    {
        if (GameHandlerSCRIPT.Instance.IsPlayerTurn) actionOverride = false;
        else actionOverride = true;
    }
}