using DG.Tweening;
using LibraryGame;
using MyUtils.Colors;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Librarian : MonoBehaviour
{
	private const int MAX_CARRY_CAPACITY = 20; // The maximum number of book the bot can carry
	private const float SEARCH_THRESHOLD = 0.25f; // Search threshold interval 0-1
	private const float BOOK_DROP_DISTANCE = 1.75f; // The distance of the bot to put the book on the shelf
	private const float BOOK_TAKE_DISTANCE = 2f; // The distance the bot can take the book
	private const float STUN_TIME = 0.5f; // Stun duration after collision

	public bool Stunned { get; private set; } = false;
	private bool _goingForSearch = false;
	private StateMachine _stateMachine;

	[SerializeField] private StackManager _stackManager;

	public StackManager MyStackManager { get => _stackManager; }
	public Book TargetBook { get; set; }
	public Bookshelf TargetBookshelf { get; set; }
	public ColorEnum MyColor { get; private set; }


	private void Awake()
	{
		var navMeshAgent = GetComponent<NavMeshAgent>();
		var animator = GetComponent<Animator>();
		var grayBookDetector = GetComponentInChildren<GrayBookDetecter>();
		var hitDetection = GetComponentInChildren<HitDetection>();

		_stateMachine = new StateMachine();

		#region Default States
		var search = new SearchForBooks(this);
		var moveToSelected = new MoveToSelected(this, navMeshAgent, animator);
		var pickUpBook = new PickUpBook(this, animator);
		var returnToBookshelf = new ReturnToBookshelf(this, navMeshAgent, animator);
		var sendBookToBookshelf = new SendBookToBookshelf(this, animator);
		#endregion

		#region High Priority States
		var goToGrayBook = new GoToGrayBook(this, navMeshAgent, grayBookDetector, animator);
		var hitSomething = new HitSomething(this, animator);
		var endingMode = new EndingMode(this, navMeshAgent, animator);
		#endregion

		#region Default State Transiitons
		At(search, moveToSelected, HasTarget());
		At(moveToSelected, search, StuckForOverASecond());
		At(moveToSelected, pickUpBook, ReachedBook());
		At(pickUpBook, search, ShoulISearchRandomDecide());
		At(pickUpBook, returnToBookshelf, ReturnToBookshelf());
		At(returnToBookshelf, sendBookToBookshelf, ReachedBookshelf());
		At(sendBookToBookshelf, search, () => _stackManager.IsStackEmpty);
		#endregion

		#region High Priority Transitions
		_stateMachine.AddAnyTransition(endingMode, Ending());
		_stateMachine.AddAnyTransition(hitSomething, HitDetected());
		_stateMachine.AddAnyTransition(goToGrayBook, GrayBookIsAvailable());
		At(hitSomething, search, () => !Stunned);
		At(goToGrayBook, returnToBookshelf, ReturnToBookshelf());
		At(goToGrayBook, search, GrayBookIsNotInRange());
		At(goToGrayBook, pickUpBook, ReachedBook());
		#endregion

		_stateMachine.SetState(search);

		void At(IState to, IState from, Func<bool> condition) =>
														_stateMachine.AddTransition(to, from, condition);
		#region Transition Conditions
		Func<bool> HasTarget() => () => TargetBook != false;
		Func<bool> StuckForOverASecond() => () => moveToSelected.TimeStuck > 1f;
		Func<bool> ReachedBook() => () => TargetBook != null &&
										Vector3.Distance(transform.position, TargetBook.transform.position)
										< BOOK_TAKE_DISTANCE;

		Func<bool> GrayBookIsNotInRange() => () => grayBookDetector.GrayBookInRange == false;
		Func<bool> ShoulISearchRandomDecide() => () =>
										(_goingForSearch = UnityEngine.Random.value > SEARCH_THRESHOLD) &&
										!_stackManager.IsStackFull;

		Func<bool> ReturnToBookshelf() => () => !_goingForSearch || _stackManager.IsStackFull && !_stackManager.IsStackEmpty;
		Func<bool> ReachedBookshelf() => () =>
				Vector3.Distance(transform.position, TargetBookshelf.transform.position) < BOOK_DROP_DISTANCE;
		Func<bool> GrayBookIsAvailable() => () => TargetBook?.MyColor != ColorEnum.Gray &&
															grayBookDetector.GrayBookInRange;

		Func<bool> HitDetected() => () => hitDetection.HitSomething;
		Func<bool> Ending() => () => GameManager.Instance.Ending;
		#endregion
	}

	private void Start()
	{
		MyColor = ColorEnum.Red;
		SetMyColor(StartDistributor.Instance.GiveMeColor());
		_stackManager.StackCapacity = MAX_CARRY_CAPACITY;
		PlayersData.Instance.SendYourData(gameObject, MyColor);
	}

	private void Update() => _stateMachine.Tick();

	public void SetMyColor(ColorEnum color)
	{
		MyColor = color;
		switch (color)
		{
			case ColorEnum.Gray:
				GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.gray;
				break;
			case ColorEnum.Red:
				GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
				break;
			case ColorEnum.Green:
				GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.green;
				break;
			case ColorEnum.Blue:
				GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.blue;
				break;
			case ColorEnum.Yellow:
				GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.yellow;
				break;

		}
	}

	public void TakeBook() => _stackManager.PushBook(TargetBook, MyColor);

	public void SendBookToBookshelf() => _stackManager.SendBookToBookshelf(TargetBookshelf);

	public void StunAndClearStack()
	{
		Stunned = true;
		MyStackManager.Clear();
		transform.GetComponentInChildren<ModelHolder>().transform
					.DOShakePosition(1f, 1f, 10, 42f, false, true);
		StartCoroutine(StunForATime());
	}

	private IEnumerator StunForATime()
	{
		yield return new WaitForSeconds(STUN_TIME);
		Stunned = false;
	}
}
