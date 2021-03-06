using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Timer))]
[RequireComponent(typeof(CustomerUI))]
[RequireComponent(typeof(AudioSource))]
public class Customer : MonoBehaviour
{
    [SerializeField] private string _name = "Bill";
    [SerializeField] private MealType _desiredMeal;
    [SerializeField] private Vector2 _desiredMealSelectionTime;
    [SerializeField] private CharacterScript _characterScript;
    [SerializeField] private AudioClip _correctMealAudio;
    [SerializeField] private AudioClip _incorrectMealAudio;

    public DateTime StoreArrivalTime { get; set; }

    private NavMeshAgent _agent;
    private Timer _timer;
    private CustomerUI _customerUI;
    private AudioSource _audioSource;

    private Chair _currentChair;
    private int _originalLayerMask;
    private bool _isMealSelectionDialogue;

    private bool ShouldMoveTowardsTarget =>
        this != null &&
        (_agent.pathPending ||
        _agent.remainingDistance > .1f);

    public bool IsWaitingInQueue =>
        _currentChair == null &&
        _timer.IsRunning;

    public bool IsWaitingForFood { get; private set; }

    public bool CanReceiveMeal => _desiredMeal != MealType.None && IsWaitingForFood;

    public string Name => _name;

    // Start is called before the first frame update
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _timer = GetComponent<Timer>();
        _customerUI = GetComponent<CustomerUI>();
        _audioSource = GetComponent<AudioSource>();

        if (_characterScript) _characterScript.characterName = _name;

        _timer.onTimerTickFinished += OnPatienceDepleted;
        _originalLayerMask = gameObject.layer;
    }

    private void OnEnable()
    {
        GameManager.Instance.onDialogueExit += ResetLayerMask;
        GameManager.Instance.onDialogueExit += UpdateDesiredMeal;
    }

    private void OnDisable()
    {
        GameManager.Instance.onDialogueExit -= ResetLayerMask;
        GameManager.Instance.onDialogueExit -= UpdateDesiredMeal;
    }

    public void CheckForFreeChairs()
    {
        if (_currentChair != null || !ChairManager.Instance.ExistsFreeChairs) return;

        _timer.StopTimer();
        var chair = ChairManager.Instance.GetFreeChair();

        if (chair != null)
        {
            StartCoroutine(MoveToChair(chair));
        }
    }

    private void OnPatienceDepleted()
    {
        MoveToExit();
    }

    public void MoveTowardsWaitingPoint(Vector3 point, Func<Vector3> waitingPointProvider)
    {
        StartCoroutine(MoveTowards(
            point,
            afterMoving: () =>
            {
                StartCoroutine(MoveTowards(
                    waitingPointProvider(),
                    afterMoving: () =>
                    {
                        CustomerManager.Instance.AddCustomerToWaitingQueue(this);
                        _timer.MaxTime *= 2f;
                        StoreArrivalTime = DateTime.Now;
                        _timer.StartTimer();
                    }));
            }));
    }

    IEnumerator MoveTowards(
        Vector3 point,
        Action beforeMoving = null,
        Action onMoving = null,
        Action afterMoving = null)
    {
        beforeMoving?.Invoke();

        _agent.SetDestination(point);
        _agent.isStopped = false;
        while (ShouldMoveTowardsTarget)
        {
            onMoving?.Invoke();
            yield return null;
        }

        afterMoving?.Invoke();
    }

    IEnumerator MoveToChair(Chair chair)
    {
        _timer.StopTimer();

        _currentChair = chair;
        var point = _currentChair.transform.position;

        _agent.SetDestination(point);
        _agent.isStopped = false;

        while (ShouldMoveTowardsTarget)
        {
            yield return null;
        }

        var secondsToWait = Random.Range(_desiredMealSelectionTime.x, _desiredMealSelectionTime.y);
        _customerUI.UpdateUI(_desiredMeal, false);
        yield return new WaitForSeconds(secondsToWait);

        /* AFTER REACHING CHAIR */

        _customerUI.UpdateUI(_desiredMeal, true);
        IsWaitingForFood = true;
        _timer.ResetMaxTime();
    }

    void MoveToExit()
    {
        StartCoroutine(MoveTowards(
            CustomerManager.Instance.RandomExitPoint.position,
            beforeMoving: () =>
            {
                if (_currentChair != null) _currentChair.IsOccupied = false;
                _timer.StopTimer();
                _customerUI.HideUI();
            },
            afterMoving: () =>
            {
                CustomerManager.Instance.RemoveFromAllLists(this);
                Destroy(gameObject);
            }));
    }

    private MealType GetRandomMeal()
    {
        var mealTypes = (MealType[])Enum.GetValues(typeof(MealType));

        // Start from 1 to avoid MealType.None
        var index = Random.Range(1, mealTypes.Length);
        var selectedMeal = mealTypes[index];
        Debug.Log($"Selected meal: {selectedMeal}");
        return selectedMeal;
    }

    private void UpdateDesiredMeal()
    {
        if (!_isMealSelectionDialogue) return;

        _desiredMeal = GetRandomMeal();
        _isMealSelectionDialogue = false;
        _customerUI.UpdateUI(_desiredMeal, true);
        _timer.StartTimer();
    }

    public bool ReceiveMeal(Meal meal)
    {
        var didReceiveMeal = false;
        if (meal.mealType == _desiredMeal)
        {
            Debug.Log($"{_name} is satisfied with your services");
            _currentChair.LocateInMealSpace(meal.gameObject);
            _audioSource.PlayOneShot(_correctMealAudio);
            CheckMealQuality();
            //ManageDialog();

            //MoveToExit();
            StartCoroutine(StartEatingMeal(meal));

            didReceiveMeal = true;
        }
        else
        {
            Debug.Log($"{_name} didn't asked for {meal.mealType}");
            _audioSource.PlayOneShot(_incorrectMealAudio);
            Destroy(meal.gameObject);
        }

        return didReceiveMeal;
    }

    private IEnumerator StartEatingMeal(Meal meal)
    {
        // TODO: trigger eat animation
        _timer.StopTimer();
        var secondsToWait = Random.Range(1.5f, 3f);
        yield return new WaitForSeconds(secondsToWait);

        // TODO: Show indicator depending on meal quality
        _currentChair.CleanPlace();

        ScoreManager.Instance.AddScore(meal.GetCalculatedScore());

        Debug.Log("Finished eating");
        MoveToExit();
    }

    public void StartMealSelectionDialogue()
    {
        ManageDialog();
        _isMealSelectionDialogue = true;
    }

    private void CheckMealQuality()
    {
        // TODO: implement
    }

    private void ManageDialog()
    {
        if (!_characterScript) return;

        // TODO: Check error when character script is set
        Debug.Log($"{nameof(Customer)}: Start Dialogue");

        DialogueManager.Instance.SetActiveCharacter(_characterScript);
        gameObject.layer = LayerMask.NameToLayer("DialogueFocus");
        GameManager.Instance.EnterDialogueMode();
    }

    private void ResetLayerMask()
    {
        if (!_characterScript) return;

        if (DialogueManager.Instance.activeCharacter == _characterScript)
            gameObject.layer = _originalLayerMask;
    }
}
