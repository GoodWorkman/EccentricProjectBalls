using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Creator : MonoBehaviour
{
   [SerializeField] private Transform _tube;
   [SerializeField] private Transform _spawner;
   [SerializeField] private ActiveItem[] _activeItemsPrefab; //заменить на массив, чтобы спавнить не только шары
   [SerializeField] private ActiveItem _ballPrefab; 

   [SerializeField] private Transform _rayTransform;
   [SerializeField] private LayerMask _layerMask;
   [SerializeField] private TextMeshProUGUI _ballsCountText;

   private ActiveItem _itemInTube;
   private ActiveItem _itemInSpawner;
   private int _maxBallLevel; //это будет задаваться из настроек уровня далее (заменить отсюда)
   private int _ballsLeftCount;
   private Coroutine _waiterForLose;
   private Ray _ray;
   private RaycastHit _hit;

   private int _count = 0;

   private void Start()
   {
      _ballsLeftCount = Level.Instance.NumberOfBalls;
      _maxBallLevel = Level.Instance.MaxCreatedBallLevel;
      UpdateBallsLeftText();
      CreateItemInTube();
      StartCoroutine(MoveToSpawner());
      
      GameManager.Instance.OnWin += FinishCreateProcess;
      GameManager.Instance.OnLose += FinishCreateProcess;
   }

   private void Update()
   {
      if (!_itemInSpawner) return;

      _ray = new Ray(_spawner.position, Vector3.down);

      if (Physics.SphereCast(_ray, _itemInSpawner.Radius, out _hit, 50f, _layerMask, QueryTriggerInteraction.Ignore))
      {
         _rayTransform.localScale = new Vector3(_itemInSpawner.Radius * 2f, _hit.distance, 1f);
         _itemInSpawner.SetProjectionPosition(_spawner.position + Vector3.down * _hit.distance);
      }

      if (Input.GetMouseButtonUp(0))
      {
         Drop();
      }
   }

   private void UpdateBallsLeftText()
   {
      _ballsCountText.text = _ballsLeftCount.ToString();
   }

   private void Drop()
   {
      _itemInSpawner.Drop();
      _itemInSpawner.SetParent(transform);
      _itemInSpawner.HideProjection();
      _itemInSpawner = null;
      _rayTransform.gameObject.SetActive(false);

      if (_itemInTube)
      {
         StartCoroutine(MoveToSpawner());
      }
      else
      {
         _waiterForLose = StartCoroutine(WaitForLose());
         CollapseManager.Instance.OnCollapsed += ResetLoseTimer;
      }
   }

   private void ResetLoseTimer()
   {
      if (_waiterForLose != null)
      {
         StopCoroutine(_waiterForLose);
      }

      _waiterForLose = StartCoroutine(WaitForLose());
   }

   private void FinishCreateProcess()
   {
      if (_waiterForLose != null)
      {
         StopCoroutine(WaitForLose());
      }

      enabled = false;
   }

   private IEnumerator WaitForLose()
   {
      yield return new WaitForSecondsRealtime(5f);
      
      GameManager.Instance.Lose();
   }

   private void CreateItemInTube()
   {
      if (_ballsLeftCount == 0)
      {
         return;
      }
      _count++;
      
      int level = Random.Range(0, _maxBallLevel);
      
      int index = Random.Range(0, _activeItemsPrefab.Length);

      ActiveItem activeItem = (_count % 5 == 0) ? _activeItemsPrefab[index] : _ballPrefab;

      _itemInTube = Instantiate(activeItem, _tube.position, Quaternion.identity);

      if (_itemInTube.ItemType == ItemType.Ball)
      {
         _itemInTube.SetLevel(level);
      }
      _itemInTube.SetToTube();
      
      _ballsLeftCount--;
      UpdateBallsLeftText();
   }

   private IEnumerator MoveToSpawner()
   {
      _itemInTube.transform.parent = _spawner;

      for (float i = 0; i < 1f; i+= Time.deltaTime / 0.45f)
      {
         _itemInTube.transform.position = Vector3.Lerp(_tube.position, _spawner.position, i);
         yield return null;
      }
      
      _itemInTube.transform.localPosition = Vector3.zero;
      _itemInSpawner = _itemInTube;
      _rayTransform.gameObject.SetActive(true);
      _itemInSpawner.ShowProjection();
      _itemInTube = null;
      
      CreateItemInTube();
   }

   private void OnDisable()
   {
      CollapseManager.Instance.OnCollapsed -= ResetLoseTimer;
      GameManager.Instance.OnWin -= FinishCreateProcess;
      GameManager.Instance.OnLose -= FinishCreateProcess;
   }
}
