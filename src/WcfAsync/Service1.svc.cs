using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Threading;
using System.Threading.Tasks;

namespace WcfAsync
{
    [ServiceContract(SessionMode = SessionMode.NotAllowed)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    //[ServiceBehavior(UseSynchronizationContext = true)] - TRIED THIS, IT DOESN'T WORKS
    public class Service1
    {
        [OperationContract, WebGet]
        public async Task<string> GetStr(bool configureAwait)
        {
            System.Web.HttpContext.Current.Items["threadid"] = Thread.CurrentThread.ManagedThreadId.ToString();
            if (configureAwait)
            {
                await Task.Delay(100);
            }
            else
            {
                await Task.Delay(100).ConfigureAwait(false);
            }
            return System.Web.HttpContext.Current == null
                ? "null httpcontext"
                : System.Web.HttpContext.Current.Items["threadid"] == null
                    ? $"new httpcontext, threadid =  {Thread.CurrentThread.ManagedThreadId}"
                    : $"original httpcontext, threadid = {System.Web.HttpContext.Current.Items["threadid"]}";
        }
    }
}
