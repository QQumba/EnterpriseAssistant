using EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;
using EnterpriseAssistant.Application.Features.UserFeatures.ViewModels;

namespace EnterpriseAssistant.Application.Features.EnterpriseFeatures.ViewModels;

public class EnterpriseCreateTransaction : IEnterpriseCreateTransaction
{
    public Guid Id { get; set; }

    public EnterpriseCreateTransactionStep Step { get; set; }

    public DateTime ExpirationTime { get; set; }
    
    // public string UserLogin { get; set; }
    
    public UserViewModel User { get; set; }
    
    // public Guid EnterpriseId { get; set; }

    public EnterpriseViewModel Enterprise { get; set; }
    
    // public long RootDepartmentId { get; set; }

    public DepartmentViewModel RootDepartment { get; set; }

}

public interface IEnterpriseCreateTransaction
{
    public Guid Id { get; set; }
}