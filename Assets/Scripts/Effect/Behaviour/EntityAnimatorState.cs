
namespace Effect.Behaviour
{
    public class EntityAnimatorState
    {

        public string value { get; private set; }

        private EntityAnimatorState(string name)
        {
            this.value = name;
        }
        
        
        public static EntityAnimatorState ATTACK
        {
            get { return new EntityAnimatorState("attack"); }
        }
        
        public static EntityAnimatorState WALK
        {
            get { return new EntityAnimatorState("walk"); }
        }

        public override string ToString()
        {
            return value;
        }
    }
}