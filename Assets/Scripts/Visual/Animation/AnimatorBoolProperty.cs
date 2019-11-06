namespace Jumper.Visual.Animation
{
    public class AnimatorBoolProperty : AnimatorValueProperty<bool>
    {
        protected override void SetValue(bool value)
        {
            Animator.SetBool(PropertyName, value);
        }
    }
}