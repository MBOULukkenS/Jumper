namespace Jumper.Visual.Animation
{
    public class AnimatorFloatProperty : AnimatorValueProperty<float>
    {
        protected override void SetValue(float value)
        {
            Animator.SetFloat(PropertyName, value);
        }
    }
}