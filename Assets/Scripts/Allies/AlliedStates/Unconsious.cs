public class Unconsious : AIState
{
        public Unconsious(AlliedAI ai)
        {
                Ai = ai; 
        }

        public override void Update()
        {
                //nothing happens if dead
        }
}