using System.Collections;
using UnityEngine;

public class Increaser : MonoBehaviour
{
    [SerializeField] private IncreaserMoveType _increaserMoveType;
    [SerializeField] private IncreaserData _increaserData;
    [SerializeField] private float _speed;
    [SerializeField] private Increaser _increaserDouble;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _blueMaterial;
    [SerializeField] private Material _redMaterial;

    private IncreaserZone _increaserZone;
    private IncreaserDisplay _display;
    private bool _isPassedZone;
 
    private void Awake()
    {
        _increaserZone = GetComponentInChildren<IncreaserZone>();
        _display = GetComponentInChildren<IncreaserDisplay>();
    }

    public void Initialize()
    {
        _increaserZone.CannonPassedIncreaserZone += OnCannonPassedIncreaserZone;
        _display.Initialize(_increaserData);
        SetMaterial(_increaserData.ArithmeticOperator);

        if (_increaserMoveType == IncreaserMoveType.Moving)
            StartCoroutine(Moving());
    }

    private void OnDisable()
    {
        _increaserZone.CannonPassedIncreaserZone -= OnCannonPassedIncreaserZone;
    }

    private void OnCannonPassedIncreaserZone(IIncreases increases)
    {
        if (_isPassedZone == true) return;
        _isPassedZone = true;

        increases.OnPassedIncreaserZone(_increaserData);
        _increaserDouble?.DisableUsage();
    }

    private void SetMaterial(ArithmeticOperator arithmeticOperator)
    {
        if (arithmeticOperator == ArithmeticOperator.Add ||
           arithmeticOperator == ArithmeticOperator.Mul)
        {
            _meshRenderer.material = _blueMaterial;
        }

        else
        {
            _meshRenderer.material = _redMaterial;
        }
    }

    public void DisableUsage()
    {
        _isPassedZone = true;
    }

    private IEnumerator Moving()
    {
        Vector3 target = transform.position;
        float[] positionX = new float[2] { 0, -4.35f };
        int currentPosition = 0;
        target.x = positionX[currentPosition];

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) <= 0.001)
            {
                currentPosition += 1;
                currentPosition = currentPosition % positionX.Length;
                target.x = positionX[currentPosition];
            }
            yield return null;
        }
    }

#if UNITY_EDITOR 
    [ContextMenu("UpdateUI")]
    public void UpdateUI()
    {
        _display = GetComponentInChildren<IncreaserDisplay>();
        _display.Initialize(_increaserData);
        SetMaterial(_increaserData.ArithmeticOperator);
    }
#endif


}

public enum IncreaserType
{
    Year,
    Cannon
}

public enum IncreaserMoveType
{
    Stand,
    Moving
}