
namespace StardropTools.Tween
{
    public interface ITween : ITweenable
    {
        public int TweenID { get; set; }
        public TweenType TweenType { get; }
        public TweenState TweenState { get; }
        public bool IsInManagerList { get; set; }

        public Tween GetTween();
        void UpdateTweenState();
    }
}
