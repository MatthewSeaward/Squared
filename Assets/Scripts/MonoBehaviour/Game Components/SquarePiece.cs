using Assets.Scripts;
using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;

public class SquarePiece : MonoBehaviour, ISquarePiece
{

    [SerializeField]
    public enum Colour
    {
        Red,
        Orange,
        Yellow,
        Pink,
        Purple,
        Grey,
        Green,
        DarkBlue,
        LightBlue
    }


    [SerializeField]
    public SpriteRenderer InnerSprite = null;

    private readonly MenuProvider _menuProvider;
    private Sprite _sprite;
    private Animator _animator;
    private static Vector3 _initialScale;
    private Vector2Int _position;
    public Vector2Int Position
    {
        get
        {
            return _position;
        }
        set
        {
            _position = value;
            gameObject.name = $"{_position.x},{_position.y}";
        }
    }

    public ISwapEffect SwapEffect { get; set; }
    public IPieceConnection PieceConnection { get; set; }
    public IPieceDestroy DestroyPieceHandler { get; set; }
    public IBehaviour PieceBehaviour { get; set; }
    public IScoreable Scoring { get; set; }

    private Animator Animator => _animator = _animator ?? GetComponent<Animator>();

    public SpriteRenderer SpriteRenderer => GetComponent<SpriteRenderer>();
    
    public Sprite Sprite
    {
        get
        {
            if (_sprite == null)
            {
                _sprite = SpriteRenderer.sprite;
            }
            return _sprite;
        }
        set
        {
            SpriteRenderer.sprite = value;
            _sprite = value;
        }
    }

    private void OnEnable()
    {
        _sprite = null;
        SetMouseDown(false);
        DestroyPieceHandler?.Reset();
        transform.localScale = _initialScale;
    }

    void Awake()
    {
        _initialScale = transform.localScale;
        GetComponent<Lerp>().LerpCompleted += SquarePiece_LerpCompleted;
    }

    private void SquarePiece_LerpCompleted()
    {
        GameResources.PlayEffect("Landing", transform.position);
    }

    void Update()
    {
        DestroyPieceHandler?.Update();
        PieceBehaviour?.Update(this, Time.deltaTime);
    }

    void OnMouseDown()
    {
        Pressed();
    }

    private void OnMouseEnter()
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }
       
        if (ConnectionHelper.AdjancentToLastPiece(this) && PieceSelectionManager.Instance.PieceCanBeRemoved(this))
        {
            PieceSelectionManager.Instance.RemovePiece();      
        }
        else
        {
            Pressed();
        }
    }

    public void Pressed()
    {
        if (MenuProvider.Instance.OnDisplay)
        {
            return;
        }

        if (PieceSelectionManager.Instance.AlreadySelected(this))
        {
            return;
        }

        if (!PieceConnection.ConnectionValid(this))
        {
            return;
        }

        PieceSelectionManager.Instance.Add(this);

        DestroyPieceHandler.OnPressed();
        SetMouseDown(true);
    }

     
    public void DestroyPiece()
    {
        Animator.SetTrigger("Destroy");
        DestroyPieceHandler.NotifyOfDestroy();
    }

    public void Destroy()
    {
        DestroyPieceHandler.OnDestroy();
    }

    public void Deselected()
    {
        DestroyPieceHandler.Reset();
        SetMouseDown(false);
    }

    private void SetMouseDown(bool value)
    {
        Animator.SetBool("MouseDown", value);
    }    
}
