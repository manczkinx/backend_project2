using LMSApi.Model;
using System.ServiceModel;

namespace LMSApi.Service
{
    [ServiceContract]
    public interface IMemberService
    {
        [OperationContract]
        Task<List<Member>> GetMembersAsync();

        [OperationContract]
        Task<Member> GetMemberByIdAsync(int id);

        [OperationContract]
        Task CreateMemberAsync(Member member);

        [OperationContract]
        Task UpdateMemberAsync(Member member);

        [OperationContract]
        Task DeleteMemberAsync(int id);
    }
}
