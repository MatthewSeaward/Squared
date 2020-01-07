using Assets.Scripts;
using Assets.Scripts.Managers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Collection;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;

public delegate void PieceDestroyed(SquarePiece piece);

public class SquarePiece : MonoBehaviour, ISquarePiece
{

    [SerializeField]
    public enum Colour
    {
        None = -1,
        Red = 0,
        Orange = 1,
        Yellow = 2,
        Pink = 3,
        Purple = 4,
        Grey = 5,
        Green = 6,
        DarkBlue = 7,
        LightBlue = 8
    }


    [SerializeField]
    public SpriteRenderer InnerSprite = null;

    private readonly MenuProvider _menuProvider;
    private Sprite _sprite;
    private Animator _animator;
    private static Vector3 _initialScale;
    private Vector2Int _position;
    private Canvas canvas;

    public PieceDestroyed PieceDestroyed { set; get; }

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

    public IPieceConnection PieceConnection { get; set; }
    public IPieceDestroy DestroyPieceHandler { get; set; }
    public IBehaviour PieceBehaviour { get; set; }
    public IScoreable Scoring { get; set; }
    public IOnCollection OnCollection { get; set; }
    public IOnDestroy OnDestroy { get; set; }

    public Colour PieceColour { get; set; }    
    public PieceTypes Type { get; set; }

    private Animator Animator => _animator = _animator ?? GetComponent<Animator>();

    public bool IsActive => gameObject.activeInHierarchy;

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
            if (value == null)
            {
                return;
            }

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
        canvas = GetComponentInChildren<Canvas>();
    }

    private void SquarePiece_LerpCompleted()
    {
        GameResources.PlayEffect("Landing", transform.position);
    }

    void Update()
    {
        if (GameManager.Instance.GamePaused)
        {
            return;
        }

        PieceBehaviour?.Update(this, Time.deltaTime);
    }

    void OnMouseUp()
    {   
        PieceSelectionManager.Instance.PieceSelection.Piece_MouseUp(this);
    }

    private void OnMouseEnter()
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        PieceSelectionManager.Instance.PieceSelection.Piece_MouseEnter(this);
    }

    void OnMouseDown()
    {
        Pressed(true);
    }

    public void Pressed(bool checkForAdditional)
    {
        PieceSelectionManager.Instance.PieceSelection.Piece_MouseDown(this, checkForAdditional);
    }    

    public void DestroyPiece()
    {
        Animator.SetTrigger("Destroy");
        DestroyPieceHandler.NotifyOfDestroy();
        OnDestroy?.OnDestroy();
    }

    public void Destroy()
    {
        DestroyPieceHandler.OnDestroy();
        PieceDestroyed?.Invoke(this);
    }

    public void Deselected()
    {
        DestroyPieceHandler.Reset();
        SetMouseDown(false);
    }

    public void SetMouseDown(bool value)
    {
        Animator.SetBool("MouseDown", value);
    }    

    public void Selected()
    {
        DestroyPieceHandler.OnPressed();
        SetMouseDown(true);
    }

    public void SetText(string text)
    {
        canvas.gameObject.SetActive(!string.IsNullOrWhiteSpace(text));

        if (canvas.gameObject.activeInHierarchy)
        {
            canvas.GetComponentInChildren<Text>().text = text;
        }
    }
}
