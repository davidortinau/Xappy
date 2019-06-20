using Continuous;

namespace Xappy.iOS
{
    public class XappyVM : Continuous.Server.VM
    {
        protected override bool ShouldInstantiate(EvalRequest code)
        {
            return base.ShouldInstantiate(code);
        }
    }
}