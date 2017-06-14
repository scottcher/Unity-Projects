using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Breaker : MonoBehaviour
{

    private bool status;
    private string label;
    private GameObject breakerLabel;
    private GameObject breakerNominal;
    private GameObject breakerFault;
    private Renderer breakerNominalRenderer;
    private Renderer breakerFaultRenderer;

    public enum Status
    {
        Nominal = 0,
        Fault = 1
    }

    [Tooltip("The breaker's position number in the panel.")]
    public int Slot = -1;

    [Tooltip("The breaker's amperage rating in Amps")]
    public int AmpRating = 10;
    
    [Tooltip("The breaker's initial status")]
    public Status StatusType = Status.Nominal;

    // Use this for initialization
    void Start()
    {
        // get the relevant game objects for the breaker
        breakerLabel = transform.Find("Breaker Label").gameObject;
        breakerNominal = transform.Find("Nominal").gameObject;
        breakerFault = transform.Find("Fault").gameObject;
        breakerNominalRenderer = breakerNominal.GetComponent<Renderer>();
        breakerFaultRenderer = breakerFault.GetComponent<Renderer>();

        // determine the initial fault state based on the StatusType
        SetStatus();

        // set the initial state of the breaker label
        if (breakerLabel != null)
        {
            label = "Breaker ";
            label += Slot.ToString();
            label += System.Environment.NewLine;
            label += AmpRating.ToString() + " Amp";
            breakerLabel.GetComponent<TextMesh>().text = label;
        }

        // set the initial state of the fault indicator
        if (breakerNominal != null &&
            breakerFault != null)
        {
            updateFaultStatus(status);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((StatusType == Status.Nominal && !status) || 
            (StatusType == Status.Fault && status))
        {
            status = !status;
            updateFaultStatus(status);
        }
    }

    void SetStatus()
    {
        switch (StatusType)
        {
            case Status.Nominal:
                status = true;
                break;
            case Status.Fault:
            default:
                status = false;
                break;
        }
    }

    private void updateFaultStatus(bool status)
    {
        breakerFaultRenderer.enabled = !status;
        breakerNominalRenderer.enabled = status;
    }
}
