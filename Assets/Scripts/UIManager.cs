using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Clase principal que representa el gerente de la interfaz.
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Referencia a la instancia única UIManager.

    [Header("Temporizador Del Botón")]
    public float popupDuration;                            // Duración de ventana emergente.

    private Coroutine popupInCorutine = null;              // Corutine de aparición de ventana emergente.
    private Coroutine popupOutCorutine = null;             // Corutine de desaparición de ventana emergente.
    private bool panelActive = false;

    private List<UnityEvent> popUpEvents = new();

    //  MÉTODOS DE UNITY

    // Método que se llama al despertar el objeto.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);

            Debug.LogWarning("Había uno o varios objetos 'UIManager', se eliminó el duplicado.");
        }
    }

    // Método que se llama al destruir el objeto.
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    // MÉTODOS DEL GERENTE DE LA INTERFAZ

    // Método de abrir el panel.
    public void OpenPanel(GameObject panelGO)
    {
        var contentGO = panelGO.transform.Find("Content").gameObject;

        if (contentGO == null)
        {
            Debug.LogWarning("'Content' es nulo.");

            return;
        }

        StartPopupIn(panelGO, contentGO, popupDuration);
    }

    // Método de cerrar el panel.
    public void ClosePanel(GameObject panelGO)
    {
        var contentGO = panelGO.transform.Find("Content").gameObject;

        if (contentGO == null)
        {
            Debug.LogWarning("'Content' es nulo.");
        }

        StartPopupOut(panelGO, contentGO, popupDuration);
    }

    // Método de iniciar la aparición de la ventana emergente.
    public void StartPopupIn(GameObject parent, GameObject target, float duration)
    {
        if (popupInCorutine != null)
        {
            return;
        }

        popupInCorutine = StartCoroutine(PopupIn(parent, target, duration));
    }

    // Método de iniciar la desaparición de la ventana emergente.
    public void StartPopupOut(GameObject parent, GameObject target, float duration)
    {
        if (popupOutCorutine != null)
        {
            return;
        }

        popupOutCorutine = StartCoroutine(PopupOut(parent, target, duration));
    }

    // Corrutina de la aparición de la ventana emergente.
    private IEnumerator PopupIn(GameObject parent, GameObject target, float duration)
    {
        parent.SetActive(true);
        panelActive = true;
        target.transform.localScale = Vector3.zero;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float elapsed = timer / duration;
            float scale = Mathf.Lerp(0f, 1.1f, elapsed);

            target.transform.localScale = Vector3.one * scale;

            yield return null;
        }

        target.transform.localScale = Vector3.one * 1.1f;

        timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float elapsed = timer / (duration / 2f);
            float scale = Mathf.Lerp(1.1f, 1f, elapsed);

            target.transform.localScale = Vector3.one * scale;

            yield return null;
        }

        target.transform.localScale = Vector3.one;

        popupInCorutine = null;
    }

    // Corrutina de la desaparición de la ventana emergente.
    private IEnumerator PopupOut(GameObject parent, GameObject target, float duration)
    {
        target.transform.localScale = Vector3.one;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float elapsed = timer / duration;
            float scale = Mathf.Lerp(1f, 0f, elapsed);

            target.transform.localScale = Vector3.one * scale;

            yield return null;
        }

        target.transform.localScale = Vector3.zero;
        parent.SetActive(false);

        popupOutCorutine = null;
        panelActive = false;

        if (popUpEvents.Count > 0)
        {
            popUpEvents[0].Invoke();
            popUpEvents.RemoveAt(0);
        } 
    }

    // MÉTODOS DE LOS BOTONES

    public void OpenPanelButton(GameObject panelGO)
    {
        OpenPanel(panelGO);
    }

    public void ClosePanelButton(GameObject panelGO)
    {
        ClosePanel(panelGO);
    }

    public void AddPopUpToRunList(UnityAction setInfoEvent, GameObject panelGO)
    {
        if (popupInCorutine != null || panelActive)
        {
            UnityEvent popUpEvent = new();
            popUpEvent.AddListener(setInfoEvent);
            popUpEvent.AddListener(() => OpenPanel(panelGO));

            popUpEvents.Add(popUpEvent);
        }
        else
        {
            UnityEvent popUpEvent = new();
            popUpEvent.AddListener(setInfoEvent);
            popUpEvent.Invoke();
            OpenPanel(panelGO);
        }
    }
}