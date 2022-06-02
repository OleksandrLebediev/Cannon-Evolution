using System.Collections.Generic;
using UnityEngine;

public class MoneyHendler : MonoBehaviour
{
    [SerializeField] private Money _moneyTemplate;

    private readonly float _radius = 1f;
    private readonly float _offsetY = 1;
    private IAcceptingMoney _acceptingMoney;

    public void Initialize(IAcceptingMoney acceptingMoney)
    {
        _acceptingMoney = acceptingMoney;
    }


    public void GetMoney(int amountOfMoney, Vector3 startPosition)
    {
        List<Money> monies = new List<Money>();

        for (int i = 0; i < amountOfMoney; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * _radius + startPosition;
            randomPosition.y = _offsetY;
            Money money = Instantiate(_moneyTemplate, randomPosition, Quaternion.identity);
            Destroy(money.gameObject, 5);
        }

        _acceptingMoney.AddMoney(10);
    }
}
