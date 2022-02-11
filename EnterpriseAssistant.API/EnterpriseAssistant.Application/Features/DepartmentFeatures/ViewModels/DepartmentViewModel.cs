namespace EnterpriseAssistant.Application.Features.DepartmentFeatures.ViewModels;

public class DepartmentViewModel
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public long? ParentDepartmentId { get; set; }

    public IList<DepartmentViewModel> ChildDepartments { get; set; } = new List<DepartmentViewModel>();
}