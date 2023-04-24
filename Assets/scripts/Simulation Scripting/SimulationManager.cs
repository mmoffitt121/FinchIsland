using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    public GameObject setupEnvironmentStep;
    public GameObject runSimulationStep;
    public GameObject viewResultsStep;
    public GameObject viewResultsFinch;

    public Button nextButton;
    public TextMeshProUGUI stepLabel;

    public SimulationStep step;

    public void AdvanceStep(int stepTo = -1)
    {
        // If we don't specify a step
        if (stepTo == -1)
        {
            // Moves to next step
            step = (SimulationStep)(((int)step + 1) % 3);
        }
        // If we specify a step
        else if (0 <= stepTo && stepTo < 3)
        {
            step = (SimulationStep)stepTo;
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        DisableUIElements();
        switch (step)
        {
            case SimulationStep.SetupEnvironment:
                DisplaySetupEnvironmentUI();
                break;
            case SimulationStep.RunSimulation:
                DisplayRunSimulationUI();
                break;
            case SimulationStep.ViewResults:
                DisplayViewResultsUI();
                break;
            default:
                break;
        }
    }

    public void DisplaySetupEnvironmentUI()
    {
        stepLabel.text = "Set Up Environment";
        setupEnvironmentStep.SetActive(true);
        SetSimulationObjectGlowEffects(true);
    }

    public void DisplayRunSimulationUI()
    {
        stepLabel.text = "Simulation Running...";
        runSimulationStep.SetActive(true);
    }

    public void DisplayViewResultsUI()
    {
        stepLabel.text = "Results";
        viewResultsStep.SetActive(true);
        viewResultsFinch.SetActive(true);
    }

    public void DisableUIElements()
    {
        setupEnvironmentStep.SetActive(false);
        runSimulationStep.SetActive(false);
        viewResultsStep.SetActive(false);
        viewResultsFinch.SetActive(false);
        SetSimulationObjectGlowEffects(false);
    }

    public void SetSimulationObjectGlowEffects(bool glow)
    {
        foreach (SimulationResource sr in FindObjectsByType<SimulationResource>(FindObjectsSortMode.None))
        {
            sr.particleEmitter.SetActive(glow);
        }
    }

    public void Start()
    {
        UpdateUI();
    }

    public enum SimulationStep
    {
        SetupEnvironment = 0,
        RunSimulation = 1,
        ViewResults = 2
    }
}
