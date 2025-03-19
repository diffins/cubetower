using System;
using _Source.Configs;
using DG.Tweening;
using UnityEngine;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Transform _viewParent;
    [SerializeField] private SpriteRenderer _renderer;

    private Tween _tween;

    public void Initialize(ItemConfig config)
    {
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.identity;
        _renderer.maskInteraction = SpriteMaskInteraction.None;
        _renderer.color = config.GetColor();
    }
    
    public void OnChangePosition(Vector3 position)
    {
        
    }
    
    public void OnEndDrag()
    {
        _renderer.sortingOrder = 1;
        StopDragAnimation();
    }
    
    public void OnStartDrag()
    {
        _renderer.sortingOrder = 10;
        StartDragAnimation();
    }

    private void StopDragAnimation()
    {
        if (_tween != null)
        {
            _tween.Kill();
            _tween = _viewParent.transform.DORotate(Vector3.zero, .2f).SetEase(Ease.OutQuad);
        }
    }

    private void StartDragAnimation()
    {
        _tween?.Kill();

        _tween = DOTween.Sequence()
            .Append(_viewParent.transform.DORotate(new Vector3(0, 0, 4), 0.5f * .5f).SetEase(Ease.InOutSine))
            .Append(DOTween.Sequence()
                .Append(_viewParent.transform.DORotate(new Vector3(0, 0, -4), 0.5f).SetEase(Ease.InOutSine))
                .Append(_viewParent.transform.DORotate(new Vector3(0, 0, 4), 0.5f).SetEase(Ease.InOutSine))
                .SetLoops(int.MaxValue, LoopType.Yoyo));
    }

    public void PlayDestroyImmediatelyAnimation(Action onComplete)
    {
        DOTween.Sequence()
            .Append(transform.DOScale(1.3f, .35f).SetEase(Ease.OutQuad))
            .Join(DOTween.Sequence()
                .Join(transform.DOMove(transform.position + new Vector3(0, -.5f, 0), .1f).SetEase(Ease.OutQuad))
                .Append(transform.DOMove(transform.position + new Vector3(0, 2f, 0), .15f).SetEase(Ease.OutQuad))
                .Join(transform.DOScale(0f, .35f).SetEase(Ease.OutQuad))
                .OnComplete(() =>
                {
                    onComplete();
                }));
        
    }

    public void PlayDestroyThrowTrashAnimation(Action onComplete, Vector3 position)
    {
        transform.DOMove(position, .2f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            _renderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            DOTween.Sequence()
                .Join(transform.DOMove(transform.position + new Vector3(0, .2f, 0), .4f).SetEase(Ease.OutQuad))
                .Append(transform.DORotate(new Vector3(0,0, 270), .5f).SetEase(Ease.OutQuad))
                .Join(transform.DOMove(transform.position + new Vector3(0, -5f, 0), .6f).SetEase(Ease.OutQuad))
                .OnComplete(() =>
                {
                    onComplete();
                });
        });
    }
}