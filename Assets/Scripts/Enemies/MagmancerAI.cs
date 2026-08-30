using UnityEngine;

public class MagmancerAI : EnemyAI
{
    public GameObject magaBall;

    public GameObject SummonProjectile()
    {
        return Instantiate(magaBall);
    }
    
    public override void RequestState(States state)
    {
        _currentState.Exit();
        switch (state)
        {
            case States.EnemyAttacking:
                _currentState = new MagmancerAttack(this);
                break;
            case States.EnemyFollowing:
                _currentState = new MagmancerFollow(this);
                break;
            case States.EnemySearching:
                _currentState = new EnemySearch(this);
                if (isCultist)
                    _currentState = new CultistSearch(this);
                break;
            case States.EnemyStunned:
                _currentState = new EnemyStun(this);
                break;
        }
        _currentState.Enter();
    }
}