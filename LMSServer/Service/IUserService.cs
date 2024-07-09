using System.ServiceModel;

namespace LMSServer
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        string RegisterUser(User user);
    }
}
