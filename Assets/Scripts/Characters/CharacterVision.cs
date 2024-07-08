using System;
using System.Collections;
using ScriptableObjects;
using UnityEngine;

namespace Characters
{
    public class CharacterVision : MonoBehaviour
    {
        [SerializeField] private string[] targetTags;
        [SerializeField] private int maxTargetAmountForCheck = 10;
        [SerializeField] private float secondsBeforeCheckTargets = 2;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private CharacterData characterData;

        private GameObject _target;
        private Collider2D[] _targets;
        private WaitForSeconds _intervalWait;

        [HideInInspector] public float sqrMagnitude;

        public LayerMask TargetLayerMask => layerMask;
        public GameObject Target => _target;
        
        private float _compareRangeSqr;

        private void Start()
        {
            _compareRangeSqr = characterData.SightRange * characterData.SightRange;
            _targets = new Collider2D[maxTargetAmountForCheck];
            _intervalWait = new WaitForSeconds(secondsBeforeCheckTargets);
            StartCoroutine(FindTarget());
        }

        private IEnumerator FindTarget()
        {
            while (true) 
            {
                yield return _intervalWait;
                
                if (_target is not null && (_target.transform.position - transform.position).sqrMagnitude > _compareRangeSqr)
                {
                    _target = null;
                }
                _targets = new Collider2D[maxTargetAmountForCheck];
                Physics2D.OverlapCircleNonAlloc(transform.position, characterData.SightRange, _targets, layerMask);
                
                float distance = Single.PositiveInfinity;
                int closestIndex = 0;
                for (int i = 0; i < maxTargetAmountForCheck; i++)
                {
                    if (_targets[i] is null)
                    {
                        continue;
                    }                    
                    bool skip = false;
                    foreach (string targetTag in targetTags)
                    {
                        if (!_targets[i].gameObject.CompareTag(targetTag))
                        {
                            skip = true;
                        }                        
                    }

                    if (skip)
                    {
                        continue;
                    }
                    
                    float distanceToTarget = (_targets[i].transform.position - transform.position).sqrMagnitude;
                    if (!(distanceToTarget < distance))
                    {
                        continue;
                    }
                    distance = distanceToTarget;
                    closestIndex = i;
                }
                if (_targets[closestIndex] is null)
                {
                    // _target = null;
                    continue;
                }
                _target = _targets[closestIndex].gameObject;
                sqrMagnitude = (_target.transform.position - transform.position).sqrMagnitude;
            }
        }

    }
}