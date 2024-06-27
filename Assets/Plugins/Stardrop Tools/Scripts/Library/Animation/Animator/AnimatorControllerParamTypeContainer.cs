using UnityEngine;

namespace StardropTools.Animation
{
    [System.Serializable]
    public struct AnimatorControllerParamTypeContainer
    {
        [SerializeField] AnimatorControllerParameterType parameterType;
        [SerializeField] private string parameter;

        public AnimatorControllerParameterType ParameterType { get => parameterType; set => parameterType = value; }
        public string Parameter { get => parameter; set => parameter = value; }


        public AnimatorControllerParamTypeContainer(AnimatorControllerParameterType parameterType = AnimatorControllerParameterType.Trigger, string parameter = "Trigger Param")
        {
            this.parameterType = parameterType;
            this.parameter = parameter;
        }


        public override bool Equals(object obj)
        {
            return obj is AnimatorControllerParamTypeContainer container &&
                   parameterType == container.parameterType &&
                   parameter == container.parameter;
        }
    }
}
