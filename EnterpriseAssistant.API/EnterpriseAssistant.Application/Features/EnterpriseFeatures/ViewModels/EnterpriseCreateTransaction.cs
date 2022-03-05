using EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;
using EnterpriseAssistant.Application.Features.UserFeatures.ViewModels;

namespace EnterpriseAssistant.Application.Features.EnterpriseFeatures.ViewModels;

public class EnterpriseCreateTransaction :
    IEnterpriseCreateTransactionUser,
    IEnterpriseCreateTransactionEnterprise,
    IEnterpriseCreateTransactionDepartment
{
    public Guid Id { get; set; }

    public EnterpriseCreateTransactionStage Stage { get; set; }

    public DateTime ExpirationTime { get; set; }

    public UserViewModel User { get; set; }

    public EnterpriseViewModel Enterprise { get; set; }

    public DepartmentViewModel RootDepartment { get; set; }
}

public interface IEnterpriseCreateTransaction
{
    public Guid Id { get; set; }
}

public interface IEnterpriseCreateTransactionUser : IEnterpriseCreateTransaction
{
    public UserViewModel User { get; set; }
}

public interface IEnterpriseCreateTransactionEnterprise : IEnterpriseCreateTransaction
{
    public EnterpriseViewModel Enterprise { get; set; }
}

public interface IEnterpriseCreateTransactionDepartment : IEnterpriseCreateTransaction
{
    public DepartmentViewModel RootDepartment { get; set; }
}