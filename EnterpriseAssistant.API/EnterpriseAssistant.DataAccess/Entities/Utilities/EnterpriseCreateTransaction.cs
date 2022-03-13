using EnterpriseAssistant.DataAccess.Entities.Enums;

namespace EnterpriseAssistant.DataAccess.Entities.Utilities;

public class EnterpriseCreateTransaction
{
    public Guid Id { get; set; }

    public EnterpriseCreateTransactionStage Stage { get; set; }

    public DateTime ExpirationTime { get; set; }

    public string UserLogin { get; set; }

    public Guid EnterpriseId { get; set; }

    public long RootDepartmentId { get; set; }
}